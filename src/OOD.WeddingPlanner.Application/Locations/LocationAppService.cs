using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Locations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Locations
{
  public class LocationAppService : CrudAppService<Location, LocationDto, Guid, GetLocationsInputDto, CreateUpdateLocationDto, CreateUpdateLocationDto>,
      ILocationAppService
  {
    protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Location.Default;
    protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Location.Default;
    protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Location.Create;
    protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Location.Update;
    protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Location.Delete;

    private readonly ILocationRepository _repository;

    public LocationAppService(ILocationRepository repository) : base(repository)
    {
      _repository = repository;
    }

    public async Task<LocationWithNavigationPropertiesDto> GetWithNavigationById(Guid id)
    {
      return ObjectMapper.Map<LocationWithNavigationProperties, LocationWithNavigationPropertiesDto>(
        await _repository.GetWithNavigationById(id));
    }
  }
}
