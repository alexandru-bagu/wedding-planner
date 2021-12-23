using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OOD.WeddingPlanner
{
    [DependsOn(
        typeof(WeddingPlannerEntityFrameworkCoreTestModule)
        )]
    public class WeddingPlannerDomainTestModule : AbpModule
    {

    }
}