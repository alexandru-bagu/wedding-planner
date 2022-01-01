using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInvitationDesignViewModel ViewModel { get; set; }

        private readonly IInvitationDesignAppService _service;

        public CreateModalModel(IInvitationDesignAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}