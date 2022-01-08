using OOD.WeddingPlanner.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace OOD.WeddingPlanner.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class WeddingPlannerPageModel : AbpPageModel
    {
        protected WeddingPlannerPageModel()
        {
            LocalizationResourceType = typeof(WeddingPlannerResource);
        }
    }
}