using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Locations
{
    [Authorize]
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

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.Location.Default,
              WeddingPlannerPermissions.Event.Create,
              WeddingPlannerPermissions.Event.Update);
            var count = await _repository.GetCountAsync();
            var list = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, nameof(Location.CreationTime));
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Location>, List<LookupDto<Guid>>>(list));
        }
    }
}
