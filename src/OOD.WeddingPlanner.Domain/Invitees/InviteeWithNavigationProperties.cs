using System;
using OOD.WeddingPlanner.Invitations;

namespace OOD.WeddingPlanner.Invitees
{
  public class InviteeWithNavigationProperties
  {
    public Invitee Invitee { get; set; }
    public Invitation Invitation { get; set; }
  }
}
