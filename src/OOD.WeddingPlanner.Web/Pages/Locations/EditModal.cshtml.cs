using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Locations.ViewModels;
using System;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Locations
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditLocationViewModel ViewModel { get; set; }

        private readonly ILocationAppService _service;

        public EditModalModel(ILocationAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<LocationDto, CreateEditLocationViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditLocationViewModel, CreateUpdateLocationDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}