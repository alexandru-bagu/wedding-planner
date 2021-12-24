using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitations;

namespace OOD.WeddingPlanner.Weddings.Dtos
{
  public class WeddingWithNavigationProperties
  {
    public Wedding Wedding { get; set; }
    public List<Invitation> Invitations { get; set; }
    public List<Event> Events { get; set; }
  }
}
