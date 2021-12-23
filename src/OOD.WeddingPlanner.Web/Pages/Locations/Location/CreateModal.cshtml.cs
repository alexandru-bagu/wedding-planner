using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Locations.Location.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Locations.Location
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditLocationViewModel ViewModel { get; set; }

        private readonly ILocationAppService _service;

        public CreateModalModel(ILocationAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditLocationViewModel, CreateUpdateLocationDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}