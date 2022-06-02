using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Web.Controllers;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using WkHtmlToPdfDotNet.Contracts;
using Newtonsoft.Json;

namespace OOD.WeddingPlanner.Web.Download
{
    public class InvitationDownloadBuilder
    {
        public GetInvitationsInputDto Input { get; }
        public Guid Id { get; }
        public int Total { get; private set; }
        public int Complete { get; private set; }
        public bool Finished { get => Total == Complete; }
        public bool Working { get; private set; }
        public bool Canceled { get; private set; }
        protected CancellationTokenSource CancellationTokenSource { get; }

        private string _resultPath;
        private ILogger<InvitationDownloadBuilder> _logger;
        private string _path;
        private readonly Guid? _tenantId;
        private GetInvitationsInputDto _input;
        private string _baseUrl;

        public InvitationDownloadBuilder(Guid id, ILogger<InvitationDownloadBuilder> logger, HttpContext httpContext, GetInvitationsInputDto input, string path, Guid? tenantId)
        {
            Id = id;
            CancellationTokenSource = new CancellationTokenSource();
            Working = false;
            Canceled = false;
            _logger = logger;
            _path = path;
            _tenantId = tenantId;
            _input = input;
            _baseUrl = httpContext.Request.Scheme + "://" + httpContext.Request.Host;
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        }

        public async Task Process(IServiceProvider serviceProvider)
        {
            if (Working) return;
            Working = true;
            try
            {
                var invitationRepository = serviceProvider.GetService<IInvitationRepository>();
                var currentTenant = serviceProvider.GetService<ICurrentTenant>();
                var tenantStore = serviceProvider.GetService<ITenantStore>();
                using (currentTenant.Change(_tenantId))
                {
                    TenantConfiguration tenant = null;
                    if (_tenantId != null) tenant = await tenantStore.FindAsync(_tenantId.Value);
                    var invitationDesignRepository = serviceProvider.GetService<IInvitationDesignRepository>();
                    var converter = serviceProvider.GetService<IConverter>();
                    var logger = serviceProvider.GetService<ILogger<InvitationDownloadBuilder>>();
                    var invitations = await invitationRepository.GetListAsync(_input.WeddingId, _input.Destination);
                    _logger.LogInformation($"Processing {JsonConvert.SerializeObject(_input)} / {invitations.Count}");
                    if (invitations.Count > 0)
                    {
                        Total = invitations.Count * 2 - 1;
                        foreach (var invitation in invitations)
                        {
                            if (CancellationTokenSource.Token.IsCancellationRequested) break;
                            var design = await invitationDesignRepository.GetAsync(invitation.DesignId.Value);
                            var response = PrintController.PrintInvitationPDF(invitation.Id, tenant.Name, design, _baseUrl, converter, logger);
                            using (var oStream = new FileStream(Path.Combine(_path, $"{invitation.Id}.pdf"), FileMode.Create))
                                await oStream.WriteAsync(response, 0, response.Length);
                            Complete++;
                        }
                        if (CancellationTokenSource.Token.IsCancellationRequested) return;
                        var fi = invitations.First();
                        var target = Path.Combine(_path, $"{Id}.pdf");
                        var tmp_target = Path.Combine(_path, $"{Id}.tmp.pdf");
                        File.Move(Path.Combine(_path, $"{fi.Id}.pdf"), target);
                        foreach (var invitation in invitations.Skip(1))
                        {
                            if (CancellationTokenSource.Token.IsCancellationRequested) return;
                            using (var src1 = PdfReader.Open(target, PdfDocumentOpenMode.Import))
                            using (var src2 = PdfReader.Open(Path.Combine(_path, $"{invitation.Id}.pdf"), PdfDocumentOpenMode.Import))
                            using (var @out = new PdfDocument())
                            {
                                for (int i = 0; i < src1.PageCount; i++)
                                    @out.AddPage(src1.Pages[i]);
                                for (int i = 0; i < src2.PageCount; i++)
                                    @out.AddPage(src2.Pages[i]);

                                @out.Save(tmp_target);
                                File.Delete(target);
                                File.Move(tmp_target, target);
                                Complete++;
                            }
                        }
                        _resultPath = target;
                    }
                }
            }
            catch (Exception ex)
            {
                Canceled = true;
                CancellationTokenSource.Cancel();
                _logger.LogError(ex, "buildInvitation");
            }
            finally
            {
                Working = false;
                if (CancellationTokenSource.Token.IsCancellationRequested)
                    Canceled = true;
            }
        }

        public Stream GetStream()
        {
            if (!Finished) return null;
            var dispose = (() => () =>
            {
                var parent = Path.GetDirectoryName(_resultPath);
                Directory.Delete(parent, true);
            });
            return new DisposableStream(new FileStream(_resultPath, FileMode.Open), dispose, (() => Task.Run(() =>
            {
                (dispose())();
                return Task.CompletedTask;
            })));
        }

        public void Stop()
        {
            CancellationTokenSource.Cancel();
        }

        private class DisposableStream : Stream
        {
            public Stream Stream { get; }
            public Func<Action> DisposeAction { get; }
            public Func<Task> DisposeTask { get; }

            public DisposableStream(Stream stream, Func<Action> disposeAction, Func<Task> disposeTask)
            {
                Stream = stream;
                DisposeAction = disposeAction ?? (() => (Action)(() => { }));
                DisposeTask = disposeTask ?? (() => Task.CompletedTask);
            }

            public override bool CanRead => Stream.CanRead;
            public override bool CanSeek => Stream.CanSeek;
            public override bool CanWrite => Stream.CanWrite;
            public override long Length => Stream.Length;
            public override long Position { get => Stream.Position; set => Stream.Position = value; }
            public override bool CanTimeout => Stream.CanTimeout;
            public override int ReadTimeout { get => Stream.ReadTimeout; set => Stream.ReadTimeout = value; }
            public override int WriteTimeout { get => Stream.WriteTimeout; set => Stream.WriteTimeout = value; }
            public override void Flush() => Stream.Flush();
            public override int Read(byte[] buffer, int offset, int count) => Stream.Read(buffer, offset, count);
            public override long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);
            public override void SetLength(long value) => Stream.SetLength(value);
            public override void Write(byte[] buffer, int offset, int count) => Stream.Write(buffer, offset, count);
            public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => Stream.BeginRead(buffer, offset, count, callback, state);
            public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => Stream.BeginWrite(buffer, offset, count, callback, state);
            public override int GetHashCode() => Stream.GetHashCode();
            public override void Close() => Stream.Close();
            public override void CopyTo(Stream destination, int bufferSize) => Stream.CopyTo(destination, bufferSize);
            public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => Stream.CopyToAsync(destination, bufferSize, cancellationToken);
            public override int EndRead(IAsyncResult asyncResult) => Stream.EndRead(asyncResult);
            public override void EndWrite(IAsyncResult asyncResult) => Stream.EndWrite(asyncResult);
            public override Task FlushAsync(CancellationToken cancellationToken) => Stream.FlushAsync(cancellationToken);
            public override int Read(Span<byte> buffer) => Stream.Read(buffer);
            public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => Stream.ReadAsync(buffer, offset, count, cancellationToken);
            public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => Stream.ReadAsync(buffer, cancellationToken);
            public override int ReadByte() => Stream.ReadByte();
            public override void Write(ReadOnlySpan<byte> buffer) => Stream.Write(buffer);
            public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => Stream.WriteAsync(buffer, offset, count, cancellationToken);
            public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => Stream.WriteAsync(buffer, cancellationToken);
            public override void WriteByte(byte value) => Stream.WriteByte(value);

            protected override void Dispose(bool disposing)
            {
                Stream.Dispose();
                DisposeAction()?.Invoke();
            }
            public override async ValueTask DisposeAsync()
            {
                await Stream.DisposeAsync();
                await DisposeTask();
            }
        }
    }
}
