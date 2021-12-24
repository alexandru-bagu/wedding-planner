using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Invitations.Dtos;

namespace OOD.WeddingPlanner.Weddings.Dtos
{
  public class WeddingWithNavigationPropertiesDto
  {
    public WeddingDto Wedding { get; set; }
    public List<InvitationDto> Invitations { get; set; }
    public List<EventDto> Events { get; set; }
  }
}
