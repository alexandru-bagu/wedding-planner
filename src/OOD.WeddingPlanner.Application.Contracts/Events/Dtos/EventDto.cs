using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Events.Dtos
{
  [Serializable]
  public class EventDto : FullAuditedEntityDto<Guid>
  {
    public Guid? LocationId { get; set; }

    public Guid? WeddingId { get; set; }

    public string Name { get; set; }

    public DateTime Time { get; set; }
  }
}