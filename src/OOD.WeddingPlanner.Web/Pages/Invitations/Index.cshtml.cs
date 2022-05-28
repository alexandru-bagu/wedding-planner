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
        [SelectItems(nameof(BooleanFilterItems))]
        public bool? GroomSideFilter { get; set; }
        [SelectItems(nameof(BooleanFilterItems))]
        public bool? BrideSideFilter { get; set; }
        public string DestinationFilter { get; set; }
        public string SurnameFilter { get; set; }
        [SelectItems(nameof(WeddingIdFilterItems))]
        public Guid? WeddingIdFilter { get; set; }

        public List<SelectListItem> WeddingIdFilterItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BooleanFilterItems { get; set; } = new List<SelectListItem>();

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

            BooleanFilterItems.AddRange(new[] {
                new SelectListItem(L["Select a value"], ""),
                new SelectListItem(L["No"].Value, "False"),
                new SelectListItem(L["Yes"].Value, "True"),
            });
            await Task.CompletedTask;
        }
    }
}
