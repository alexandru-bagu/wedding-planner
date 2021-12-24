using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Events.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Events
{
    public class EventAppService : CrudAppService<Event, EventDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateEventDto, CreateUpdateEventDto>,
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
    }
}
