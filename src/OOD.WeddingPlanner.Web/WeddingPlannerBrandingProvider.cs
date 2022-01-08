using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace OOD.WeddingPlanner.Web
{
    [Dependency(ReplaceServices = true)]
    public class WeddingPlannerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Wedding Planner";
    }
}
