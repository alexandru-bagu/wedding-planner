using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.InvitationDesigns.Dtos
{
    [Serializable]
    public class InvitationDesignDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public string DefaultCulture { get; set; }

        public string MeasurementUnit { get; set; }

        public double? PaperWidth { get; set; }

        public double? PaperHeight { get; set; }

        public double? PaperDpi { get; set; }
    }
}