using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitees
{
    [Authorize]
    public class InviteeAppService : CrudAppService<Invitee, InviteeDto, Guid, GetInviteesInputDto, CreateUpdateInviteeDto, CreateUpdateInviteeDto>,
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

        protected override async Task<IQueryable<Invitee>> CreateFilteredQueryAsync(GetInviteesInputDto input)
        {
            var query = await _repository.GetQueryableWithNavigation();
            query = query.WhereIf(input.InvitationId.HasValue, p => p.Invitee.InvitationId == input.InvitationId);
            query = query.WhereIf(input.WeddingId.HasValue, p => p.Invitation.WeddingId == input.WeddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                p => p.Invitee.Name.Contains(input.Filter) || p.Invitee.Surname.Contains(input.Filter) ||
                p.Wedding.Name.Contains(input.Filter) || p.Invitation.Destination.Contains(input.Filter));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name),
                p => p.Invitee.Name.Contains(input.Name));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Surname),
                p => p.Invitee.Surname.Contains(input.Surname));
            query = query.WhereIf(input.Confirmed.HasValue,
                p => p.Invitee.Confirmed == input.Confirmed);
            query = query.WhereIf(input.Child.HasValue,
                p => p.Invitee.Child == input.Child);
            return query.Select(p => p.Invitee);
        }

        [Authorize(WeddingPlannerPermissions.Invitee.Default)]
        public async Task<InviteeWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id)
        {
            return ObjectMapper.Map<InviteeWithNavigationProperties, InviteeWithNavigationPropertiesDto>(
              await _repository.GetWithNavigationByIdAsync(id));
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupInviteeInputDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.Invitee.Default,
              WeddingPlannerPermissions.Invitation.Create,
              WeddingPlannerPermissions.Invitation.Update);
            var count = await _repository.GetCountAsync(input.Filter, input.InvitationId, input.WeddingId, null, null, null);
            var list = await _repository.GetPagedListAsync(input.Filter, input.InvitationId, input.WeddingId, null, null, null, input.SkipCount, input.MaxResultCount, nameof(Invitee.CreationTime));
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Invitee>, List<LookupDto<Guid>>>(list));
        }

        [Authorize(WeddingPlannerPermissions.Invitee.Default)]
        public async Task<PagedResultDto<InviteeWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetInviteesInputDto input)
        {
            var count = await _repository.GetCountAsync(input.Filter, input.InvitationId, input.WeddingId, input.Name, input.Surname, input.Confirmed);
            var list = await _repository.GetListWithNavigationAsync(input.Filter, input.InvitationId, input.WeddingId, input.Name, input.Surname, input.Confirmed, input.Sorting, input.SkipCount, input.MaxResultCount);
            return new PagedResultDto<InviteeWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<InviteeWithNavigationProperties>, List<InviteeWithNavigationPropertiesDto>>(list));
        }
    }
}
