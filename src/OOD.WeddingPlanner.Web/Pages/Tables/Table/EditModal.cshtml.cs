using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Web.Pages.Tables.Table.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Tables.Table
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditTableViewModel ViewModel { get; set; }

        private readonly ITableAppService _service;
        private readonly IWeddingAppService _weddingAppService;
        private readonly IEventAppService _eventAppService;

        public EditModalModel(ITableAppService service, IWeddingAppService weddingAppService, IEventAppService eventAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
            _eventAppService = eventAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetWithNavigationByIdAsync(Id);
            ViewModel = ObjectMapper.Map<TableDto, CreateEditTableViewModel>(dto.Table);
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.EventItems.Add(new SelectListItem("", ""));
            if (dto.Event != null)
                ViewModel.EventItems.AddRange(
                  (await _eventAppService.GetLookupListAsync(new LookupEventsInputDto() { WeddingId = dto.Event.WeddingId }))
                    .Items
                    .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTableViewModel, CreateUpdateTableDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}