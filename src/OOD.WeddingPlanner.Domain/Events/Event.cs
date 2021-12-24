﻿using System;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Weddings;
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
    }
}
