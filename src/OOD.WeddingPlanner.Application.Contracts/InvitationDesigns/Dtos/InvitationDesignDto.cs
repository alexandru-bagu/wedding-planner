using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.InvitationDesigns.Dtos
{
    [Serializable]
    public class InvitationDesignDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Body { get; set; }
    }
}