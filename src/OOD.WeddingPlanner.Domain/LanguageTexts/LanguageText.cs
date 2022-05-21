using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public class LanguageText : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string CultureName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        protected LanguageText()
        {
        }

        public LanguageText(
            Guid id,
            Guid? tenantId,
            string cultureName,
            string name,
            string value
        ) : base(id)
        {
            TenantId = tenantId;
            CultureName = cultureName;
            Name = name;
            Value = value;
        }
    }
}
