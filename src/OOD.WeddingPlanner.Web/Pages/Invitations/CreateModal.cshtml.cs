using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitations.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Invitations
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInvitationViewModel ViewModel { get; set; }

        private readonly IInvitationAppService _service;
        private readonly IWeddingAppService _weddingAppService;
        private readonly IInviteeAppService _inviteeAppService;
        private readonly IInvitationDesignAppService _invitationDesignAppService;

        public CreateModalModel(IInvitationAppService service, IWeddingAppService weddingAppService, IInviteeAppService inviteeAppService, IInvitationDesignAppService invitationDesignAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
            _inviteeAppService = inviteeAppService;
            _invitationDesignAppService = invitationDesignAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditInvitationViewModel();
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.InviteeItems.AddRange(
              (await _inviteeAppService.GetLookupListAsync(new LookupInviteeInputDto()))
                .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.DesignItems.AddRange(
              (await _invitationDesignAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationViewModel, CreateUpdateInvitationDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}