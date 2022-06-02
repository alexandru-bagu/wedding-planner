using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
    public class GetInvitationsInputDto : PagedAndSortedResultRequestDto
    {
        public Guid? TenantId { get; set; }
        public Guid? WeddingId { get; set; }
        public string? Destination { get; set; }
        public bool? GroomSide { get; set; }
        public bool? BrideSide { get; set; }

        public GetInvitationsInputDto()
        {
        }
    }
}