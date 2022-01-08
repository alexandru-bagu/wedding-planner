using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OOD.WeddingPlanner.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.EntityFrameworkCore
{
    public class EntityFrameworkCoreWeddingPlannerDbSchemaMigrator
        : IWeddingPlannerDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreWeddingPlannerDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the WeddingPlannerDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WeddingPlannerDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
