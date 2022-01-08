using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Events
{
    public class EventAppService : CrudAppService<Event, EventDto, Guid, GetEventsInputDto, CreateUpdateEventDto, CreateUpdateEventDto>,
        IEventAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Event.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Event.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Event.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Event.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Event.Delete;

        private readonly IEventRepository _repository;
        public EventAppService(IEventRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<EventWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id)
        {
            return ObjectMapper.Map<EventWithNavigationProperties, EventWithNavigationPropertiesDto>(
              await _repository.GetWithNavigationByIdAsync(id));
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupEventsInputDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.Event.Default,
              WeddingPlannerPermissions.Wedding.Create,
              WeddingPlannerPermissions.Wedding.Update,
              WeddingPlannerPermissions.Table.Create,
              WeddingPlannerPermissions.Table.Update,
              WeddingPlannerPermissions.Location.Create,
              WeddingPlannerPermissions.Location.Update);
            var count = await _repository.GetCountAsync(input.WeddingId);
            var list = await _repository.GetPagedListAsync(input.WeddingId, input.SkipCount, input.MaxResultCount, nameof(Event.CreationTime));
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Event>, List<LookupDto<Guid>>>(list));
        }

        public async Task<PagedResultDto<EventWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetEventsInputDto input)
        {
            var count = await _repository.GetCountAsync(input.WeddingId);
            var list = await _repository.GetListWithNavigationAsync(input.WeddingId, input.SkipCount, input.MaxResultCount, input.Sorting);
            return new PagedResultDto<EventWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<EventWithNavigationProperties>, List<EventWithNavigationPropertiesDto>>(list));
        }
    }
}
