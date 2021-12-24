using System;
using OOD.WeddingPlanner.Invitations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitees
{
    public class Invitee : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Surname { get; set; }

        public virtual string GivenName { get; set; }

        public virtual Guid? InvitationId { get; set; }

        public virtual DateTime? RSVP { get; set; }

        public virtual bool? Confirmed { get; set; }
    }
}
