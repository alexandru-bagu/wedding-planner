using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitations
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public string DestinationFilter { get; set; }
        public string SurnameFilter { get; set; }
        [SelectItems(nameof(WeddingIdFilterItems))]
        public Guid? WeddingIdFilter { get; set; }

        public List<SelectListItem> WeddingIdFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", "")
            };
        public IWeddingAppService AppService { get; }

        public IndexModel(IWeddingAppService appService)
        {
            AppService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            WeddingIdFilterItems.AddRange(
                 (await AppService.GetLookupListAsync(new LookupRequestDto()))
                   .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            await Task.CompletedTask;
        }
    }
}
