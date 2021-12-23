using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Weddings
{
    public class Wedding : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual List<Event> Events { get; set; }
    }
}
