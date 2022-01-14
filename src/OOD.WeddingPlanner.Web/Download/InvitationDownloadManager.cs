using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Web.Services;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Web.Download
{
    public class InvitationDownloadManager : IInvitationDownloadManager, ISingletonDependency
    {
        public IServiceProvider ServiceProvider { get; }

        private bool _initialized;
        private SemaphoreSlim _lock;
        private string _rootDir;
        private ConcurrentDictionary<Guid, InvitationDownloadBuilder> _builders;

        public InvitationDownloadManager(IServiceProvider serviceProvider)
        {
            _initialized = false;
            _builders = new ConcurrentDictionary<Guid, InvitationDownloadBuilder>();
            _lock = new SemaphoreSlim(1);
            ServiceProvider = serviceProvider;
        }

        protected async Task Initialize()
        {
            await _lock.WaitAsync();
            try
            {
                if (_initialized) return;
                _initialized = true;
                _rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "wedding-planner", "invitations");
                try { if (Directory.Exists(_rootDir)) Directory.Delete(_rootDir, true); } catch { }
                Directory.CreateDirectory(_rootDir);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<Guid> Begin(GetInvitationsInputDto input, Guid? tenantId = null)
        {
            await Initialize();
            Guid id = Guid.NewGuid();
            var builder = new InvitationDownloadBuilder(id, 
                ServiceProvider.GetService<ILogger<InvitationDownloadBuilder>>(), 
                ServiceProvider.GetService<IHttpContextAccessor>().HttpContext, 
                input, Path.Combine(_rootDir, id.ToString()), tenantId);
            _builders.TryAdd(builder.Id, builder);
            InvitationDownloadBuilderService.Enqueue(builder);
            return builder.Id;
        }

        public async Task Cancel(Guid id)
        {
            await Initialize();
            if (_builders.TryGetValue(id, out InvitationDownloadBuilder builder))
            {
                builder.Stop();
            }
        }

        public async Task<Stream> Download(Guid id)
        {
            await Initialize();
            if (_builders.TryGetValue(id, out InvitationDownloadBuilder builder) && builder.Finished)
                return builder.GetStream();
            return null;
        }

        public async Task<DownloadStatus> Status(Guid id)
        {
            await Initialize();
            if (_builders.TryGetValue(id, out InvitationDownloadBuilder builder))
                return new DownloadStatus() { Total = builder.Total, Complete = builder.Complete };
            return null;
        }
    }
}
