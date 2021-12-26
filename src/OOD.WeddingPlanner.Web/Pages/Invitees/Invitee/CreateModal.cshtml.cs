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

    public virtual Task OnGetAsync()
    {
      ViewModel = new CreateEditInviteeViewModel();
      ViewModel.BooleanItems = new List<SelectListItem>()
      {
        new SelectListItem("", ""),
        new SelectListItem("No", "False"),
        new SelectListItem("Yes", "True"),
      };
      return Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
      var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
      await _service.CreateAsync(dto);
      return NoContent();
    }
  }
}