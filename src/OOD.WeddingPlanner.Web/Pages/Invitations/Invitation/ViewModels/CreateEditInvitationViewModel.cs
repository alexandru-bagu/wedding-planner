using System;

using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels
{
  public class CreateEditInvitationViewModel
  {
    [Required] [Display(Name = "InvitationWeddingId")]
    public Guid? WeddingId { get; set; }

    [Required] [Display(Name = "InvitationDestination")]
    public string Destination { get; set; }
  }
}