using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using static System.Environment;

namespace OOD.WeddingPlanner.Providers
{
  public class IPAddressRequestCultureProvider : RequestCultureProvider
  {
    private SemaphoreSlim _semaphore;
    private string _dir, _hash_file, _db_file;
    private string _sha256;
    private DateTime _lastCheck;

    public IPAddressRequestCultureProvider()
    {
      _semaphore = new SemaphoreSlim(1);
      _dir = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData), "maxmind");
      if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
      _hash_file = Path.Combine(_dir, "hash.txt");
      _db_file = null;
      if (File.Exists(_hash_file))
      {
        _sha256 = File.ReadAllText(_hash_file);
        _lastCheck = File.GetLastWriteTime(_hash_file);
        _db_file = get_db_path();
      }
    }

    private async Task<string> updateDatabase()
    {
      if (_db_file == null || DateTime.Now > _lastCheck.AddHours(24))
      {
        _lastCheck = DateTime.Now;
        const string url = "https://download.maxmind.com/app/geoip_download?edition_id=GeoLite2-Country&license_key=eOrJHq5hnAQ3qNCK&suffix=tar.gz";
        var sha256url = url + ".sha256";
        using (var client = new HttpClient())
        {
          var sha256 = await client.GetStringAsync(new Uri(sha256url));
          sha256 = sha256.Substring(0, sha256.IndexOf(' '));
          if (_db_file == null || _sha256 != sha256)
          {
            File.WriteAllText(_hash_file, sha256);
            var dirs = Directory.GetDirectories(_dir);
            foreach (var dir in dirs) Directory.Delete(dir, true);
            var tmp = Path.Combine(_dir, "archive.tar.gz");
            using (var iStream = await client.GetStreamAsync(new Uri(url)))
            using (var oStream = new FileStream(tmp, FileMode.Create))
              await iStream.CopyToAsync(oStream);
            ExtractTGZ(tmp, _dir);
            _db_file = get_db_path();
          }
        }
      }
      return _db_file;
    }

    public void ExtractTGZ(String gzArchiveName, String destFolder)
    {
      using (Stream inStream = File.OpenRead(gzArchiveName))
      using (Stream gzipStream = new GZipInputStream(inStream))
      using (TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.UTF8))
        tarArchive.ExtractContents(destFolder);
    }

    private string get_db_path()
    {
      var files = Directory.GetFiles(_dir, "*.mmdb", SearchOption.AllDirectories);
      if (files.Length > 0) return files[0];
      return null;
    }

    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
      await _semaphore.WaitAsync();
      try
      {
        await updateDatabase();

        using (var reader = new DatabaseReader(_db_file))
        {
          var response = reader.Country(httpContext.Connection.RemoteIpAddress);
          if ("ro".Equals(response.Country.IsoCode))
            return new ProviderCultureResult("ro-RO");
        }
      }
      catch (Exception) { }
      finally
      {
        _semaphore.Release();
      }
      return null;
    }
  }
}