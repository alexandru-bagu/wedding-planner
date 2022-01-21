using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Tables;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.TableInvitees
{
    public class TableInvitee : Entity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public virtual Guid? TableId { get; set; }
        public virtual Guid? InviteeId { get; set; }

        public virtual Table Table { get; set; }
        public virtual Invitee Invitee { get; set; }

        protected TableInvitee()
        {
        }

        public TableInvitee(
            Guid id,
            Guid? tenantId,
            Guid? tableId,
            Guid? inviteeId
        ) : base(id)
        {
            TenantId = tenantId;
            TableId = tableId;
            InviteeId = inviteeId;
        }
    }
}
