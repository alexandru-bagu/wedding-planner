using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
  public class MonacoEditorJsBundleContributor : IBundleContributor
  {
    public MonacoEditorJsBundleContributor()
    {

    }

    public Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
      var serviceProvider = context.ServiceProvider;
      var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

      var loaderFilePath = Path.Combine("libs", "monaco-editor", "loader.js");
      var absoluteLoaderFilePath = Path.Combine(environment.WebRootPath, loaderFilePath);
      if (!File.Exists(absoluteLoaderFilePath))
      {
        File.WriteAllText(absoluteLoaderFilePath, @$"(function(){{
  var monacoLoader = document.createElement('script');
  monacoLoader.setAttribute('src','/libs/monaco-editor/min/vs/loader.js');
  document.body.appendChild(monacoLoader);

  var interval = setInterval(function(){{
    if(window.require && window.require.config) {{
      window.require.config({{ paths: {{ vs: '/libs/monaco-editor/min/vs' }} }});
      clearInterval(interval);
    }}
  }}, 100);
}})();");
      }
      context.Files.Add('/' + loaderFilePath.TrimStart('\\', '/').Replace('\\', '/'));
      return Task.CompletedTask;
    }

    public Task ConfigureDynamicResourcesAsync(BundleConfigurationContext context)
    {
      return Task.CompletedTask;
    }

    public Task PostConfigureBundleAsync(BundleConfigurationContext context)
    {
      return Task.CompletedTask;
    }

    public Task PreConfigureBundleAsync(BundleConfigurationContext context)
    {
      return Task.CompletedTask;
    }
  }
}
