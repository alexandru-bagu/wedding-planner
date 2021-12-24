using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using OOD.WeddingPlanner.Web.Pages.Weddings.Wedding.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Weddings.Wedding
{
  public class CreateModalModel : WeddingPlannerPageModel
  {
    [BindProperty]
    public CreateEditWeddingViewModel ViewModel { get; set; }

    private readonly IWeddingAppService _service;

    public CreateModalModel(IWeddingAppService service)
    {
      _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditWeddingViewModel, CreateUpdateWeddingDto>(ViewModel);
      await _service.CreateAsync(dto);
      return NoContent();
    }
  }
}