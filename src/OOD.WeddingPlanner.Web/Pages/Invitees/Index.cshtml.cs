using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitees
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public string NameFilter { get; set; }
        public string SurnameFilter { get; set; }
        [SelectItems(nameof(BooleanFilterItems))]
        public bool? ConfirmedFilter { get; set; }
        [SelectItems(nameof(BooleanFilterItems))]
        public bool? ChildFilter { get; set; }

        public List<SelectListItem> BooleanFilterItems { get; set; }

        public virtual async Task OnGetAsync()
        {
            BooleanFilterItems =
               new List<SelectListItem>
               {
                new SelectListItem("", ""),
                new SelectListItem(L["No"].Value, "False"),
                new SelectListItem(L["Yes"].Value, "True"),
               };
            await Task.CompletedTask;
        }
    }
}
