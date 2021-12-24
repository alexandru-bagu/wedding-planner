using System;

using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels
{
  public class CreateEditInviteeViewModel
  {
    [Display(Name = "InviteeSurname")]
    public string Surname { get; set; }

    [Display(Name = "InviteeGivenName")]
    public string GivenName { get; set; }

    [Display(Name = "InviteeInvitationId")]
    public Guid? InvitationId { get; set; }

    [Display(Name = "InviteeRSVP")]
    public DateTime? RSVP { get; set; }

    [Display(Name = "InviteeConfirmed")]
    public bool? Confirmed { get; set; }
  }
}