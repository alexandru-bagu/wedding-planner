using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Web.Pages.Weddings.ViewModels;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Alerts;

namespace OOD.WeddingPlanner.Web.Pages.MyWedding
{
    public class IndexModel : WeddingPlannerPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public CreateEditWeddingViewModel Wedding { get; set; }
        public IWeddingAppService AppService { get; }

        public IndexModel(IWeddingAppService appService)
        {
            AppService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            var weddings = await AppService.GetListAsync(new GetWeddingsInputDto() { MaxResultCount = 1 });
            var wedding = weddings.Items.FirstOrDefault() ?? new WeddingDto();
            Wedding = ObjectMapper.Map<WeddingDto, CreateEditWeddingViewModel>(wedding);
            Id = wedding.Id;
        }
        public virtual async Task OnPostAsync()
        {
            var weddings = await AppService.GetListAsync(new GetWeddingsInputDto() { MaxResultCount = 1 });
            var input = ObjectMapper.Map<CreateEditWeddingViewModel, CreateUpdateWeddingDto>(Wedding);
            WeddingDto result;
            if (weddings.TotalCount == 0 && Id == Guid.Empty)
                result = await AppService.CreateAsync(input);
            else
                result = await AppService.UpdateAsync(Id, input);
            Wedding = ObjectMapper.Map<WeddingDto, CreateEditWeddingViewModel>(result);

            Alerts.Add(AlertType.Success, L["SuccessfullyUpdated"]);
        }
    }
}
