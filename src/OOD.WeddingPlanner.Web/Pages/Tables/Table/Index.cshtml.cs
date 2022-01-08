using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Tables.Table
{
    public class IndexModel : WeddingPlannerPageModel
    {
        [SelectItems(nameof(EmptySelect))]
        public string Wedding { get; set; }

        [SelectItems(nameof(EmptySelect))]
        public string Event { get; set; }

        [SelectItems(nameof(EmptySelect))]
        public string Invitee { get; set; }
        public List<SelectListItem> EmptySelect { get; set; } = new List<SelectListItem>();

        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
