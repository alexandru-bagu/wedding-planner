using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Web
{
    [Dependency(ReplaceServices = true)]
    public class WeddingPlannerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WeddingPlanner";
    }
}
