using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Locations.Dtos
{
    [Serializable]
    public class LocationDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}