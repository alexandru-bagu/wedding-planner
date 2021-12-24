using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Weddings.Dtos
{
  [Serializable]
  public class WeddingDto : FullAuditedEntityDto<Guid>
  {
    public string GroomName { get; set; }

    public string BrideName { get; set; }

    public string Name { get; set; }

    public string ContactPhoneNumber { get; set; }
  }
}