using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Tables.Dtos
{
  public class GetTablesInputDto : PagedAndSortedResultRequestDto
  {
    public Guid? EventId { get; set; }
  }
}
