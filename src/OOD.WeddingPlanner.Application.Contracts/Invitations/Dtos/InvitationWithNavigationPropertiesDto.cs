using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
  public class InvitationWithNavigationPropertiesDto
  {
    public InvitationDto Invitation { get; set; }

    public WeddingDto Wedding { get; set; }

    public List<InviteeDto> Invitees { get; set; }
  }
}
