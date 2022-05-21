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
    }
}
