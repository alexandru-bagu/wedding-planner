using System.Collections.Generic;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;

namespace OOD.WeddingPlanner.Invitations
{
  public class InvitationWithNavigationProperties
  {
    public Invitation Invitation { get; set; }

    public Wedding Wedding { get; set; }
  }
}
