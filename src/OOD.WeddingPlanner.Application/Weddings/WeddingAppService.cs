using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Weddings.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Weddings
{
    [Authorize]
    public class WeddingAppService : CrudAppService<Wedding, WeddingDto, Guid, GetWeddingsInputDto, CreateUpdateWeddingDto, CreateUpdateWeddingDto>,
        IWeddingAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Delete;

        private readonly IWeddingRepository _repository;

        public WeddingAppService(IWeddingRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.Wedding.Default,
              WeddingPlannerPermissions.Invitation.Create,
              WeddingPlannerPermissions.Invitation.Update,
              WeddingPlannerPermissions.Invitee.Create,
              WeddingPlannerPermissions.Invitee.Update,
              WeddingPlannerPermissions.Table.Create,
              WeddingPlannerPermissions.Table.Update,
              WeddingPlannerPermissions.Event.Create,
              WeddingPlannerPermissions.Event.Update);
            var count = await _repository.GetCountAsync();
            var list = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, nameof(Wedding.CreationTime));
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Wedding>, List<LookupDto<Guid>>>(list));
        }
    }
}
