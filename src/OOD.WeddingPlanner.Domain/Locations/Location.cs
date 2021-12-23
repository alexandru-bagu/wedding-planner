using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Locations
{
    public class Location : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual double Longitude { get; set; }

        public virtual double Latitude { get; set; }
    }
}
