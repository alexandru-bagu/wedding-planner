using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Web.Pages.Weddings.ViewModels;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using System;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Weddings
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditWeddingViewModel ViewModel { get; set; }

        private readonly IWeddingAppService _service;

        public EditModalModel(IWeddingAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<WeddingDto, CreateEditWeddingViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditWeddingViewModel, CreateUpdateWeddingDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}