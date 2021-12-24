using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Locations.Dtos
{
  [Serializable]
  public class CreateUpdateLocationDto
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }
  }
}