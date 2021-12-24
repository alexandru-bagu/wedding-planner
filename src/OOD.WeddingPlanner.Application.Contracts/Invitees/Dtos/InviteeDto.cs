using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
  [Serializable]
  public class InviteeDto : FullAuditedEntityDto<Guid>
  {
    public string Surname { get; set; }

    public string GivenName { get; set; }

    public Guid? InvitationId { get; set; }

    public DateTime? RSVP { get; set; }

    public bool? Confirmed { get; set; }
  }
}