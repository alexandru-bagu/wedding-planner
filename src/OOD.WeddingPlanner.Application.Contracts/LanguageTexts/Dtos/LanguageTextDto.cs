using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.LanguageTexts.Dtos
{
    [Serializable]
    public class LanguageTextDto : FullAuditedEntityDto<Guid>
    {
        public string ResourceName { get; set; }

        public string CultureName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}