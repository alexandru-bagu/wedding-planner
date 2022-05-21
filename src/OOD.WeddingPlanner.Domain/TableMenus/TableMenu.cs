using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.TableMenus
{
    public class TableMenu : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual bool Adult { get; set; }

        public virtual int Order { get; set; }

        protected TableMenu()
        {
        }

        public TableMenu(
            Guid id,
            Guid? tenantId,
            string name,
            bool adult,
            int order
        ) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            Adult = adult;
            Order = order;
        }
    }
}
