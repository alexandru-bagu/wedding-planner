using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events.Dtos;

namespace OOD.WeddingPlanner.Locations.Dtos
{
  public class LocationWithNavigationPropertiesDto
  {
    public LocationDto Location { get; set; }
    public List<EventDto> Events { get; set; }
  }
}
