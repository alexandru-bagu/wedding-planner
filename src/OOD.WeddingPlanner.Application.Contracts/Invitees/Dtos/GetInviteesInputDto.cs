using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
    public class GetInviteesInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool? Confirmed { get; set; }

        public Guid? InvitationId { get; set; }

        public Guid? WeddingId { get; set; }

        public GetInviteesInputDto()
        {
        }
    }
}