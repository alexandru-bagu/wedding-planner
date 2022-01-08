using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace OOD.WeddingPlanner.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(WeddingPlannerEntityFrameworkCoreModule),
        typeof(WeddingPlannerApplicationContractsModule)
        )]
    public class WeddingPlannerDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
