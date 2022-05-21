using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Tables
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
        public IWeddingAppService WeddingAppService { get; }
        public long WeddingCount { get; private set; }

        public IndexModel(IWeddingAppService weddingAppService)
        {
            WeddingAppService = weddingAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var result = await WeddingAppService.GetListAsync(new GetWeddingsInputDto() { MaxResultCount = 1 });
            WeddingCount = result.TotalCount;
        }
    }
}
