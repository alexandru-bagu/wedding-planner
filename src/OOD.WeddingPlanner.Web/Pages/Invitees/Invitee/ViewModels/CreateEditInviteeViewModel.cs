using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels
{
  public class CreateEditInviteeViewModel
  {
    [Required] [Display(Name = "InviteeSurname")]
    public string Surname { get; set; }

    [Required] [Display(Name = "InviteeGivenName")]
    public string GivenName { get; set; }

    [Display(Name = "InviteeRSVP")]
    public DateTime? RSVP { get; set; }

    [Required] [Display(Name = "InviteeConfirmed")] [SelectItems(nameof(BooleanItems))]
    public bool Confirmed { get; set; }
    public List<SelectListItem> BooleanItems { get; set; } = new List<SelectListItem>()
    {
      new SelectListItem("No", "False"),
      new SelectListItem("Yes", "True"),
    };
  }
}