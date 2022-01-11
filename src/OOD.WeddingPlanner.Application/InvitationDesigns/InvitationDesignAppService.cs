using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    [Authorize]
    public class InvitationDesignAppService : CrudAppService<InvitationDesign, InvitationDesignDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateInvitationDesignDto, CreateUpdateInvitationDesignDto>,
        IInvitationDesignAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.InvitationDesign.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.InvitationDesign.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.InvitationDesign.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.InvitationDesign.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.InvitationDesign.Delete;

        private readonly IInvitationDesignRepository _repository;

        public InvitationDesignAppService(IInvitationDesignRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.InvitationDesign.Default,
              WeddingPlannerPermissions.Invitation.Create,
              WeddingPlannerPermissions.Invitation.Update);
            var count = await _repository.GetCountAsync();
            var list = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, nameof(Invitation.CreationTime));
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<InvitationDesign>, List<LookupDto<Guid>>>(list));
        }
    }
}
