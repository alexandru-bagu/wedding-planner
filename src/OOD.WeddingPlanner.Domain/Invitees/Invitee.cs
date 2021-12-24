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

        protected Invitee()
        {
        }

        public Invitee(
            Guid id,
            Guid? tenantId,
            string surname,
            string givenName,
            Guid? invitationId,
            DateTime? rsvp,
            bool? confirmed
        ) : base(id)
        {
            TenantId = tenantId;
            Surname = surname;
            GivenName = givenName;
            InvitationId = invitationId;
            RSVP = rsvp;
            Confirmed = confirmed;
        }
    }
}
