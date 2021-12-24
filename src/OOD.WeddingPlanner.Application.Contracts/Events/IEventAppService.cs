using System;
using OOD.WeddingPlanner.Events.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Events
{
    public interface IEventAppService :
        ICrudAppService< 
            EventDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateEventDto,
            CreateUpdateEventDto>
    {
        Task<EventWithNavigationPropertiesDto> GetWithNavigationById(Guid id);
    }
}