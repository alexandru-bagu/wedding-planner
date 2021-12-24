using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Events.Dtos
{
  [Serializable]
  public class CreateUpdateEventDto
  {
    public Guid? LocationId { get; set; }

    public Guid? WeddingId { get; set; }

    public string Name { get; set; }

    public DateTime Time { get; set; }
  }
}