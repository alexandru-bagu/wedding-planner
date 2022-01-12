using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Web.Download
{
    public class InvitationDownloadBuilderService : IHostedService
    {
        private static ConcurrentBag<InvitationDownloadBuilder> _queue = new ConcurrentBag<InvitationDownloadBuilder>();
        public IServiceProvider ServiceProvider { get; }

        public InvitationDownloadBuilderService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public static void Enqueue(InvitationDownloadBuilder builder)
        {
            _queue.Add(builder);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ThreadPool.QueueUserWorkItem(async (_) =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_queue.TryTake(out InvitationDownloadBuilder builder))
                        await builder.Process(ServiceProvider);
                    await Task.Delay(1000);
                }
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
