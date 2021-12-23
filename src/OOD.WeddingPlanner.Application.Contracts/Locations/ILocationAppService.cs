using System;
using OOD.WeddingPlanner.Locations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Locations
{
    public interface ILocationAppService :
        ICrudAppService< 
            LocationDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateLocationDto,
            CreateUpdateLocationDto>
    {

    }
}