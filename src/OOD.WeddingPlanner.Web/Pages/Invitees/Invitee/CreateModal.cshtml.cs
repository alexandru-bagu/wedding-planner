using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using OOD.WeddingPlanner.Invitations;
using System.Linq;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.Invitee
{
  public class CreateModalModel : WeddingPlannerPageModel
  {
    [BindProperty]
    public CreateEditInviteeViewModel ViewModel { get; set; }

    private readonly IInviteeAppService _service;
    private readonly IInvitationAppService _invitationAppService;

    public CreateModalModel(IInviteeAppService service, IInvitationAppService invitationAppService)
    {
      _service = service;
      _invitationAppService = invitationAppService;
    }

    public virtual async Task OnGetAsync()
    {
      ViewModel = new CreateEditInviteeViewModel();
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
      await _service.CreateAsync(dto);
      return NoContent();
    }
  }
}