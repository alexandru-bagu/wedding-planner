using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Invitees
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditInviteeViewModel ViewModel { get; set; }

        private readonly IInviteeAppService _service;
        private readonly IInvitationAppService _invitationAppService;
        private readonly IWeddingAppService _weddingAppService;

        public EditModalModel(IInviteeAppService service, IInvitationAppService invitationAppService, IWeddingAppService weddingAppService)
        {
            _service = service;
            _invitationAppService = invitationAppService;
            _weddingAppService = weddingAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetWithNavigationByIdAsync(Id);
            ViewModel = ObjectMapper.Map<InviteeDto, CreateEditInviteeViewModel>(dto.Invitee);
            ViewModel.BooleanItems.AddRange(new[] {
                new SelectListItem("", ""),
                new SelectListItem(L["No"].Value, "False"),
                new SelectListItem(L["Yes"].Value, "True"),
            });
            ViewModel.WeddingItems.AddRange(new[] {
                new SelectListItem("", "")
            });
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString()) { Selected = dto.Invitation?.WeddingId == p.Id }));
            ViewModel.InvitationItems.AddRange(new[] {
                new SelectListItem("", "")
            });
            if (dto.Wedding != null)
                ViewModel.InvitationItems.AddRange(
                  (await _invitationAppService.GetLookupListAsync(new LookupInvitationsInputDto() { WeddingId = dto.Wedding.Id }))
                    .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.MenuTypes.AddRange(new[] {
                new SelectListItem("", ""),
                new SelectListItem(L["MenuNone"].Value, "None"),
                new SelectListItem(L["MenuChild"].Value, "Child"),
                new SelectListItem(L["MenuAdult"].Value, "Adult")
            });
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            if(!ViewModel.Child.Value && string.IsNullOrWhiteSpace(ViewModel.Menu)) ViewModel.Menu = "Adult";
            var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}