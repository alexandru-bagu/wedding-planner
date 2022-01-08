using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Events.Dtos
{
    public class GetEventsInputDto : PagedAndSortedResultRequestDto
    {
        public Guid? WeddingId { get; set; }
        public GetEventsInputDto()
        {
        }
    }
}