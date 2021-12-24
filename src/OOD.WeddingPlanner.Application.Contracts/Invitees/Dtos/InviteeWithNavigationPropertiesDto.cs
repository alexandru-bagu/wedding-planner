using System;
using OOD.WeddingPlanner.Invitations.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
  public class InviteeWithNavigationPropertiesDto
  {
    public InviteeDto Invitee { get; set; }
    public InvitationDto Invitation { get; set; }
  }
}
