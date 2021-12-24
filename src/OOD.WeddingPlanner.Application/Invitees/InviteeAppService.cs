using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Invitees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitees
{
    public class InviteeAppService : CrudAppService<Invitee, InviteeDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateInviteeDto, CreateUpdateInviteeDto>,
        IInviteeAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Invitee.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Invitee.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Invitee.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Invitee.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Invitee.Delete;

        private readonly IInviteeRepository _repository;
        
        public InviteeAppService(IInviteeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
