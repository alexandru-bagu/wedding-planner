using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Locations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Locations
{
  public interface ILocationAppService :
      ICrudAppService<
          LocationDto,
          Guid,
          GetLocationsInputDto,
          CreateUpdateLocationDto,
          CreateUpdateLocationDto>
  {
    Task<LocationWithNavigationPropertiesDto> GetWithNavigationById(Guid id);
  }
}