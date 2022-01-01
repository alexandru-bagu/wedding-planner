using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Invitees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace OOD.WeddingPlanner.Invitees
{
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
      var query = await base.CreateFilteredQueryAsync(input);
      query = query.WhereIf(input.InvitationId.HasValue, p => p.InvitationId == input.InvitationId);
      return query.WhereIf(input.TableId.HasValue, p => p.TableId == input.TableId);
    }

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
      var count = await _repository.GetCountAsync(input.InvitationId, input.TableId, input.WeddingId);
      var list = await _repository.GetPagedListAsync(input.InvitationId, input.TableId, input.WeddingId, input.SkipCount, input.MaxResultCount, nameof(Invitee.CreationTime));
      return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Invitee>, List<LookupDto<Guid>>>(list));
    }

    public async Task<PagedResultDto<InviteeWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetInviteesInputDto input)
    {
      var count = await _repository.GetCountAsync(input.InvitationId, input.TableId, input.WeddingId);
      var list = await _repository.GetListWithNavigationAsync(input.InvitationId, input.TableId, input.WeddingId, input.Sorting, input.SkipCount, input.MaxResultCount);
      return new PagedResultDto<InviteeWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<InviteeWithNavigationProperties>, List<InviteeWithNavigationPropertiesDto>>(list));
    }
  }
}
