using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Weddings;

namespace OOD.WeddingPlanner.Events
{
  public class EventWithNavigationProperties
  {
    public Event Event { get; set; }

    public Wedding Wedding { get; set; }

    public Location Location { get; set; }
  }
}
