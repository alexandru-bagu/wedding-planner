using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Tables.Dtos
{
  [Serializable]
  public class TableDto : FullAuditedEntityDto<Guid>
  {
    public Guid? EventId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Size { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }
  }
}