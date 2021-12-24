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

        public virtual string GroomName { get; set; }

        public virtual string BrideName { get; set; }

        public virtual string Name { get; set; }

        public virtual string ContactPhoneNumber { get; set; }
    }
}
