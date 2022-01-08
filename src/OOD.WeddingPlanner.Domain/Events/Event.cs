using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Events
{
    public class Event : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid? LocationId { get; set; }

        public virtual Guid? WeddingId { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual Location Location { get; set; }

        public virtual Wedding Wedding { get; set; }

        public virtual List<Table> Tables { get; set; } = new List<Table>();

        protected Event()
        {
        }

        public Event(
            Guid id,
            Guid? tenantId,
            Guid? locationId,
            Guid? weddingId,
            string name,
            DateTime time
        ) : base(id)
        {
            TenantId = tenantId;
            LocationId = locationId;
            WeddingId = weddingId;
            Name = name;
            Time = time;
        }
    }
}
