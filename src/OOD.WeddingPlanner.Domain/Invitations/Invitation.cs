using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitations
{
    public class Invitation : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid? WeddingId { get; set; }
    }
}
