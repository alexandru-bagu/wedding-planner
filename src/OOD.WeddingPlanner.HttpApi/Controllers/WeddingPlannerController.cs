using OOD.WeddingPlanner.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OOD.WeddingPlanner.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class WeddingPlannerController : AbpControllerBase
    {
        protected WeddingPlannerController()
        {
            LocalizationResource = typeof(WeddingPlannerResource);
        }
    }
}