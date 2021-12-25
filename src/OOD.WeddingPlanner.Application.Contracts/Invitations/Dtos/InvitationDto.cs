using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
  [Serializable]
  public class InvitationDto : FullAuditedEntityDto<Guid>
  {
    public Guid? WeddingId { get; set; }
    
    public string Destination { get; set; }
  }
}