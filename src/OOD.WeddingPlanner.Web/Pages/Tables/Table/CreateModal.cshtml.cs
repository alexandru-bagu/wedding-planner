using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Web.Pages.Tables.Table.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Tables.Table
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditTableViewModel ViewModel { get; set; }

        private readonly ITableAppService _service;
        private readonly IWeddingAppService _weddingAppService;

        public CreateModalModel(ITableAppService service, IWeddingAppService weddingAppService, IEventAppService eventAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditTableViewModel();
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.EventItems.Add(new SelectListItem("", ""));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTableViewModel, CreateUpdateTableDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}