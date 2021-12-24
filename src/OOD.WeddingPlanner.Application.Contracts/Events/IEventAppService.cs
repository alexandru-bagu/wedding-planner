using System;
using OOD.WeddingPlanner.Events.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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

    }
}