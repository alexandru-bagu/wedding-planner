using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.Invitee
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

    public EditModalModel(IInviteeAppService service, IInvitationAppService invitationAppService)
    {
      _service = service;
      _invitationAppService = invitationAppService;
    }

    public virtual async Task OnGetAsync()
    {
      var dto = await _service.GetAsync(Id);
      ViewModel = ObjectMapper.Map<InviteeDto, CreateEditInviteeViewModel>(dto);
      ViewModel.BooleanItems.AddRange(new[] {
        new SelectListItem("", ""),
        new SelectListItem("No", "False"),
        new SelectListItem("Yes", "True"),
      });
      ViewModel.InvitationItems.AddRange(new[] {
        new SelectListItem("", "")
      });
      ViewModel.InvitationItems.AddRange(
        (await _invitationAppService.GetLookupListAsync(new LookupRequestDto()))
          .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
      await _service.UpdateAsync(Id, dto);
      return NoContent();
    }
  }
}