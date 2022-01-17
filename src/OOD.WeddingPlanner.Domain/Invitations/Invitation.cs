using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitations
{
    public class Invitation : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid? WeddingId { get; set; }

        public virtual Guid? DesignId { get; set; }

        public virtual string Destination { get; set; }

        public virtual bool PlusOne { get; set; }

        public virtual List<Invitee> Invitees { get; set; } = new List<Invitee>();

        public virtual Wedding Wedding { get; set; }

        public virtual InvitationDesign Design { get; set; }

        protected Invitation()
        {
        }

        public Invitation(
            Guid id,
            Guid? tenantId,
            Guid? weddingId,
            string destination
        ) : base(id)
        {
            TenantId = tenantId;
            WeddingId = weddingId;
            Destination = destination;
        }
    }
}
