using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Invitations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OOD.WeddingPlanner.Invitees;
using Volo.Abp.Uow;
using System.Linq;

namespace OOD.WeddingPlanner.Invitations
{
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
      var count = await _repository.GetCountAsync(input.WeddingId);
      var list = await _repository.GetPagedListAsync(input.WeddingId, input.SkipCount, input.MaxResultCount, nameof(Invitation.CreationTime));
      return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Invitation>, List<LookupDto<Guid>>>(list));
    }

    protected override async Task<IQueryable<Invitation>> CreateFilteredQueryAsync(GetInvitationsInputDto input)
    {
      var query = await base.CreateFilteredQueryAsync(input);
      query = query.WhereIf(input.WeddingId.HasValue, p => p.WeddingId == input.WeddingId);
      return query;
    }

    public async Task<PagedResultDto<InvitationWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetInvitationsInputDto input)
    {
      var count = await _repository.GetCountAsync(input.WeddingId);
      var list = await _repository.GetListWithNavigationAsync(input.WeddingId, input.Sorting, input.SkipCount, input.MaxResultCount);
      return new PagedResultDto<InvitationWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<InvitationWithNavigationProperties>, List<InvitationWithNavigationPropertiesDto>>(list));
    }

    [UnitOfWork]
    public override async Task<InvitationDto> CreateAsync(CreateUpdateInvitationDto input)
    {
      var invitation = await base.CreateAsync(input);
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
