using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
  public class GetInviteesInputDto : PagedAndSortedResultRequestDto
  {
    public Guid? InvitationId { get; set; }
    public GetInviteesInputDto()
    {
    }
  }
}