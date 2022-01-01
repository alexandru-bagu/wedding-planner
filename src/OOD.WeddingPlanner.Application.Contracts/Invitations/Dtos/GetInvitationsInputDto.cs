using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
  public class GetInvitationsInputDto : PagedAndSortedResultRequestDto
  {
    public Guid? WeddingId { get; set; }

    public GetInvitationsInputDto()
    {
    }
  }
}