using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public EditModalModel(IInviteeAppService service)
    {
      _service = service;
    }

    public virtual async Task OnGetAsync()
    {
      var dto = await _service.GetAsync(Id);
      ViewModel = ObjectMapper.Map<InviteeDto, CreateEditInviteeViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
      await _service.UpdateAsync(Id, dto);
      return NoContent();
    }
  }
}