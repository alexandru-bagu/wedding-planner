using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.ViewModels;
using System;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditInvitationDesignViewModel ViewModel { get; set; }

        private readonly IInvitationDesignAppService _service;

        public EditModalModel(IInvitationDesignAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<InvitationDesignDto, CreateEditInvitationDesignViewModel>(dto);

            ViewModel.MeasurementUnits.AddRange(new[] {
                new SelectListItem("Millimeter", "mm"),
                new SelectListItem("Centimeter", "cm"),
                new SelectListItem("Inch", "in"),
            });
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}