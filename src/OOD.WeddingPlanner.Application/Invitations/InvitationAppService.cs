using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Invitations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

    public InvitationAppService(IInvitationRepository repository) : base(repository)
    {
      _repository = repository;
    }

    public async Task<InvitationWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id)
    {
      return ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(
        await _repository.GetWithNavigationByIdAsync(id));
    }

    [AllowAnonymous]
    public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input)
    {
      await AuthorizationService.AnyPolicy(
        WeddingPlannerPermissions.Invitation.Default,
        WeddingPlannerPermissions.Wedding.Create,
        WeddingPlannerPermissions.Wedding.Update,
        WeddingPlannerPermissions.Invitee.Create,
        WeddingPlannerPermissions.Invitee.Update);
      var count = await _repository.GetCountAsync();
      var list = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, null);
      return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Invitation>, List<LookupDto<Guid>>>(list));
    }
  }
}
