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
        [SelectItems(nameof(ConfirmFilterItems))]
        public bool? ConfirmedFilter { get; set; }

        public List<SelectListItem> ConfirmFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
