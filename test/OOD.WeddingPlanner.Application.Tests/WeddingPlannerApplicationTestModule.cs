using Volo.Abp.Modularity;

namespace OOD.WeddingPlanner
{
    [DependsOn(
        typeof(WeddingPlannerApplicationModule),
        typeof(WeddingPlannerDomainTestModule)
        )]
    public class WeddingPlannerApplicationTestModule : AbpModule
    {

    }
}