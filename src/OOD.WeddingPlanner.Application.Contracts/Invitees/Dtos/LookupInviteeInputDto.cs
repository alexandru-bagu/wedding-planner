using System;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
  public class LookupInviteeInputDto : LookupRequestDto
  {
    public Guid? InvitationId { get; set; }

    public Guid? WeddingId { get; set; }

    public Guid? TableId { get; set; }
  }
}
