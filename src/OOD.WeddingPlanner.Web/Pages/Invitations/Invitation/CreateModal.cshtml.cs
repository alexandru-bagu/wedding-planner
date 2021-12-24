using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInvitationViewModel ViewModel { get; set; }

        private readonly IInvitationAppService _service;

        public CreateModalModel(IInvitationAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationViewModel, CreateUpdateInvitationDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}