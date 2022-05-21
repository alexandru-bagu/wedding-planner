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
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditEventViewModel ViewModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TimeZone { get; set; }

        private readonly IEventAppService _service;
        private readonly IWeddingAppService _weddingAppService;
        private readonly ILocationAppService _locationAppService;

        public CreateModalModel(IEventAppService service, IWeddingAppService weddingAppService, ILocationAppService locationAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
            _locationAppService = locationAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditEventViewModel();
            ViewModel.Time = DateTime.Now;
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.LocationItems.AddRange(
              (await _locationAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));

            var wding = ViewModel.WeddingItems.FirstOrDefault();
            if (wding != null) wding.Selected = true;

            var tz = TimeZoneInfo.GetSystemTimeZones();
            var userTz = TimeZoneInfo.Local;
            if (!string.IsNullOrEmpty(TimeZone))
                try { userTz = TimeZoneInfo.FindSystemTimeZoneById(TimeZone); } catch { }
            ViewModel.TimeZones.AddRange(tz.Select(p => new SelectListItem(p.DisplayName, p.Id, p.DisplayName == userTz.DisplayName)));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditEventViewModel, CreateUpdateEventDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}