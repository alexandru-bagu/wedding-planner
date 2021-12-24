using System;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Events.Dtos
{
    public class EventWithNavigationPropertiesDto
    {
        public EventDto Event { get; set; }

        public WeddingDto Wedding { get; set; }

        public LocationDto Location { get; set; }
    }
}
