using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Invitations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationAppService : CrudAppService<Invitation, InvitationDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateInvitationDto, CreateUpdateInvitationDto>,
        IInvitationAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Delete;

        private readonly IInvitationRepository _repository;
        
        public InvitationAppService(IInvitationRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
