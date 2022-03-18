using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace OOD.WeddingPlanner.Invitations
{
  [Authorize]
    public class InvitationAppService : CrudAppService<Invitation, InvitationDto, Guid, GetInvitationsInputDto, CreateUpdateInvitationDto, CreateUpdateInvitationDto>,
        IInvitationAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Invitation.Delete;

        private readonly IInvitationRepository _repository;
        private readonly IInviteeRepository _inviteeRepository;

        public InvitationAppService(IInvitationRepository repository, IInviteeRepository inviteeRepository) : base(repository)
        {
            _repository = repository;
            _inviteeRepository = inviteeRepository;
        }

        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<InvitationWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id)
        {
            return ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(
              await _repository.GetWithNavigationByIdAsync(id));
        }

        [AllowAnonymous]
        public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupInvitationsInputDto input)
        {
            await AuthorizationService.AnyPolicy(
              WeddingPlannerPermissions.Invitation.Default,
              WeddingPlannerPermissions.Wedding.Create,
              WeddingPlannerPermissions.Wedding.Update,
              WeddingPlannerPermissions.Invitee.Create,
              WeddingPlannerPermissions.Invitee.Update);
            var count = await _repository.GetCountAsync(input.WeddingId, input.Destination);
            var list = await _repository.GetListAsync(input.WeddingId, input.Destination, nameof(Invitation.CreationTime), input.SkipCount, input.MaxResultCount);
            return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Invitation>, List<LookupDto<Guid>>>(list));
        }

        protected override async Task<IQueryable<Invitation>> CreateFilteredQueryAsync(GetInvitationsInputDto input)
        {
            var query = await base.CreateFilteredQueryAsync(input);
            query = query.WhereIf(input.WeddingId.HasValue, p => p.WeddingId == input.WeddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Destination), p => p.Destination.Contains(input.Destination));
            return query;
        }

        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<PagedResultDto<InvitationWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetInvitationsInputDto input)
        {
            var count = await _repository.GetCountAsync(input.WeddingId, input.Destination);
            var list = await _repository.GetListWithNavigationAsync(input.WeddingId, input.Destination, input.Sorting, input.SkipCount, input.MaxResultCount);
            return new PagedResultDto<InvitationWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<InvitationWithNavigationProperties>, List<InvitationWithNavigationPropertiesDto>>(list));
        }

        [UnitOfWork]
        [Authorize(WeddingPlannerPermissions.Invitation.Create)]
        public override async Task<InvitationDto> CreateAsync(CreateUpdateInvitationDto input)
        {
            var unique = await Invitation.GenerateUniqueCode(_repository);
            (input as IUniqueCoded).UniqueCode = unique;
            var invitation = await base.CreateAsync(input);
            invitation.UniqueCode = unique;

            List<Invitee> inviteeList = new List<Invitee>();
            foreach (var inviteeId in input.InviteeIds)
            {
                var invitee = await _inviteeRepository.GetAsync(inviteeId);
                invitee.InvitationId = invitation.Id;
                inviteeList.Add(invitee);
            }
            await _inviteeRepository.UpdateManyAsync(inviteeList, true);
            return invitation;
        }

        [UnitOfWork]
        [Authorize(WeddingPlannerPermissions.Invitation.Update)]
        public override async Task<InvitationDto> UpdateAsync(Guid id, CreateUpdateInvitationDto input)
        {
            var invitation = await base.UpdateAsync(id, input);

            HashSet<Invitee> inviteeList = new HashSet<Invitee>(await _inviteeRepository.GetListAsync(p => p.InvitationId == id));
            foreach (var invitee in inviteeList)
            {
                invitee.InvitationId = null;
            }
            foreach (var inviteeId in input.InviteeIds)
            {
                var invitee = await _inviteeRepository.GetAsync(inviteeId);
                invitee.InvitationId = invitation.Id;
                inviteeList.Add(invitee);
            }
            await _inviteeRepository.UpdateManyAsync(inviteeList, true);
            return invitation;
        }
    }
}
