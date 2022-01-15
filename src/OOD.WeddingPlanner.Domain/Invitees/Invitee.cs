using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Tables;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitees
{
    public class Invitee : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Name { get; set; }

        public virtual Guid? InvitationId { get; set; }

        public virtual Guid? TableId { get; set; }

        public virtual DateTime? RSVP { get; set; }

        public virtual bool? Confirmed { get; set; }

        public virtual bool Child { get; set; }

        public virtual bool Male { get; set; }

        public virtual int Order { get; set; }

        public virtual Invitation Invitation { get; set; }

        public virtual Table Table { get; set; }

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
            bool? confirmed,
            bool child
        ) : base(id)
        {
            TenantId = tenantId;
            Surname = surname;
            Name = givenName;
            InvitationId = invitationId;
            RSVP = rsvp;
            Confirmed = confirmed;
            Child = child;
        }
    }
}
