using System;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
  public class InvitationWithNavigationPropertiesDto
  {
    public InvitationDto Invitation { get; set; }

    public WeddingDto Wedding { get; set; }

    public InvitationDesignDto Design { get; set; }
  }
}
