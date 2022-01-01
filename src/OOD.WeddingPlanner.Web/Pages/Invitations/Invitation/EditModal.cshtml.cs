using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels;
using OOD.WeddingPlanner.Weddings;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation
{
  public class EditModalModel : WeddingPlannerPageModel
  {
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditInvitationViewModel ViewModel { get; set; }

    private readonly IInvitationAppService _service;
    private readonly IWeddingAppService _weddingAppService;
    private readonly IInviteeAppService _inviteeAppService;
    private readonly IInvitationDesignAppService _invitationDesignAppService;

    public EditModalModel(IInvitationAppService service, IWeddingAppService weddingAppService, IInviteeAppService inviteeAppService, IInvitationDesignAppService invitationDesignAppService)
    {
      _service = service;
      _weddingAppService = weddingAppService;
      _inviteeAppService = inviteeAppService;
      _invitationDesignAppService = invitationDesignAppService;
    }

    public virtual async Task OnGetAsync()
    {
      var dto = await _service.GetAsync(Id);
      var invitees = await _inviteeAppService.GetListAsync(new GetInviteesInputDto() { InvitationId = Id, MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount });
      ViewModel = ObjectMapper.Map<InvitationDto, CreateEditInvitationViewModel>(dto);
      ViewModel.InviteeIds = invitees.Items.Select(p => p.Id).ToList();
      ViewModel.WeddingItems.AddRange(
        (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
          .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
      ViewModel.InviteeItems.AddRange(
        (await _inviteeAppService.GetLookupListAsync(new LookupRequestDto()))
          .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
      ViewModel.DesignItems.AddRange(
        (await _invitationDesignAppService.GetLookupListAsync(new LookupRequestDto()))
          .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditInvitationViewModel, CreateUpdateInvitationDto>(ViewModel);
      await _service.UpdateAsync(Id, dto);
      return NoContent();
    }
  }
}