using OOD.WeddingPlanner.Localization;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner
{
    /* Inherit your application services from this class.
     */
    public abstract class WeddingPlannerAppService : ApplicationService
    {
        protected WeddingPlannerAppService()
        {
            LocalizationResource = typeof(WeddingPlannerResource);
        }
    }
}
