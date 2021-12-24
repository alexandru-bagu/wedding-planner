using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels;

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

    public EditModalModel(IInvitationAppService service)
    {
      _service = service;
    }

    public virtual async Task OnGetAsync()
    {
      var dto = await _service.GetAsync(Id);
      ViewModel = ObjectMapper.Map<InvitationDto, CreateEditInvitationViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditInvitationViewModel, CreateUpdateInvitationDto>(ViewModel);
      await _service.UpdateAsync(Id, dto);
      return NoContent();
    }
  }
}