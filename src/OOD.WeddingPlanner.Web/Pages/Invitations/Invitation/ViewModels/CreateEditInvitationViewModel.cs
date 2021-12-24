using System;

using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels
{
  public class CreateEditInvitationViewModel
  {
    [Display(Name = "InvitationWeddingId")]
    public Guid? WeddingId { get; set; }
  }
}