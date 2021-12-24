using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.Invitee
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInviteeViewModel ViewModel { get; set; }

        private readonly IInviteeAppService _service;

        public CreateModalModel(IInviteeAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}