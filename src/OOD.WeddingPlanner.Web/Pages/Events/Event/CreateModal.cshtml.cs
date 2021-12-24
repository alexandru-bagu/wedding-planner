using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Events.Event
{
  public class CreateModalModel : WeddingPlannerPageModel
  {
    [BindProperty]
    public CreateEditEventViewModel ViewModel { get; set; }

    private readonly IEventAppService _service;

    public CreateModalModel(IEventAppService service)
    {
      _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditEventViewModel, CreateUpdateEventDto>(ViewModel);
      await _service.CreateAsync(dto);
      return NoContent();
    }
  }
}