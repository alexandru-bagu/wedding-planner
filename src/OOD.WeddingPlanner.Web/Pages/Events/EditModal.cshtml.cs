using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Web.Pages.Events.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Events
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditEventViewModel ViewModel { get; set; }

        private readonly IEventAppService _service;
        private readonly IWeddingAppService _weddingAppService;
        private readonly ILocationAppService _locationAppService;

        public EditModalModel(IEventAppService service, IWeddingAppService weddingAppService, ILocationAppService locationAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
            _locationAppService = locationAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<EventDto, CreateEditEventViewModel>(dto);

            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.LocationItems.AddRange(
              (await _locationAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));

            var tz = TimeZoneInfo.GetSystemTimeZones();
            ViewModel.TimeZones.AddRange(tz.Select(p => new SelectListItem(p.DisplayName, p.Id)));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditEventViewModel, CreateUpdateEventDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}