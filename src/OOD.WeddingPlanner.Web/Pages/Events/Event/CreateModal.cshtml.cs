using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels;
using OOD.WeddingPlanner.Weddings;

namespace OOD.WeddingPlanner.Web.Pages.Events.Event
{
  public class CreateModalModel : WeddingPlannerPageModel
  {
    [BindProperty]
    public CreateEditEventViewModel ViewModel { get; set; }

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
          .Select(p=>new SelectListItem(p.DisplayName, p.Id.ToString())));
      ViewModel.LocationItems.AddRange(
        (await _locationAppService.GetLookupListAsync(new LookupRequestDto()))
          .Items
          .Select(p=>new SelectListItem(p.DisplayName, p.Id.ToString())));
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditEventViewModel, CreateUpdateEventDto>(ViewModel);
      await _service.CreateAsync(dto);
      return NoContent();
    }
  }
}