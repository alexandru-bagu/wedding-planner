using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Locations.Dtos
{
    [Serializable]
    public class CreateUpdateLocationDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Hall { get; set; }

        public string Address { get; set; }

        public string ParkingGoogleLink { get; set; }
        
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}