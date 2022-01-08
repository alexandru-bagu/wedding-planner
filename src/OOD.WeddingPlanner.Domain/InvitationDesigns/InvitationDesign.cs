using OOD.WeddingPlanner.Invitations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public class InvitationDesign : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Body { get; set; }

        public virtual List<Invitation> Invitations { get; set; } = new List<Invitation>();

        protected InvitationDesign()
        {
        }

        public InvitationDesign(
            Guid id,
            Guid? tenantId,
            string name,
            string body
        ) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            Body = body;
        }
    }
}
