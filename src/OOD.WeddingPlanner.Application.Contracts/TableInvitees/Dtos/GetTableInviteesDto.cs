using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.TableInvitees.Dtos
{
    public class GetTableInviteesDto : PagedAndSortedResultRequestDto
    {
        public Guid? TableId { get; set; }
        public Guid? InviteeId { get; set; }
    }
}
