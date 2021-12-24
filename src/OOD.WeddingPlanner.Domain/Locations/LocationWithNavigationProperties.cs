using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events;

namespace OOD.WeddingPlanner.Locations
{
  public class LocationWithNavigationProperties
  {
    public Location Location { get; set; }
    public List<Event> Events { get; set; }
  }
}
