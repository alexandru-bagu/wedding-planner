using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitations;
using System;
using System.Collections.Generic;
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

        public virtual List<Invitation> Invitations { get; set; } = new List<Invitation>();

        public virtual List<Event> Events { get; set; } = new List<Event>();

        protected Wedding()
        {
        }

        public Wedding(
            Guid id,
            Guid? tenantId,
            string groomName,
            string brideName,
            string name,
            string contactPhoneNumber
        ) : base(id)
        {
            TenantId = tenantId;
            GroomName = groomName;
            BrideName = brideName;
            Name = name;
            ContactPhoneNumber = contactPhoneNumber;
        }
    }
}
