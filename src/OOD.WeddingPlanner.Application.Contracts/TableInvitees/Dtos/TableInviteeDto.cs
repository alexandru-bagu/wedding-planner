using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.TableInvitees.Dtos
{
    [Serializable]
    public class TableInviteeDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TableId { get; set; }

        public Guid? InviteeId { get; set; }
    }
}