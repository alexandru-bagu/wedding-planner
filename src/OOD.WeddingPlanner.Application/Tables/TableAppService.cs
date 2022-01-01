using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Tables.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Tables
{
  public class TableAppService : CrudAppService<Table, TableDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTableDto, CreateUpdateTableDto>,
      ITableAppService
  {
    protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Table.Default;
    protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Table.Default;
    protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Table.Create;
    protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Table.Update;
    protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Table.Delete;

    private readonly ITableRepository _repository;

    public TableAppService(ITableRepository repository) : base(repository)
    {
      _repository = repository;
    }

    public async Task<TableWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id)
    {
      return ObjectMapper.Map<TableWithNavigationProperties, TableWithNavigationPropertiesDto>(
        await _repository.GetWithNavigationByIdAsync(id));
    }

    public async Task<PagedResultDto<TableWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetTablesInputDto input)
    {
      var count = await _repository.GetCountAsync(input.EventId);
      var list = await _repository.GetListWithNavigationAsync(input.EventId, input.SkipCount, input.MaxResultCount, input.Sorting);
      return new PagedResultDto<TableWithNavigationPropertiesDto>(count, ObjectMapper.Map<List<TableWithNavigationProperties>, List<TableWithNavigationPropertiesDto>>(list));
    }

    public async Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupTablesInputDto input)
    {
      await AuthorizationService.AnyPolicy(
        WeddingPlannerPermissions.Table.Default,
        WeddingPlannerPermissions.Invitee.Create,
        WeddingPlannerPermissions.Invitee.Update);
      var count = await _repository.GetCountAsync(input.EventId);
      var list = await _repository.GetPagedListAsync(input.EventId, input.SkipCount, input.MaxResultCount, nameof(Table.CreationTime));
      return new PagedResultDto<LookupDto<Guid>>(count, ObjectMapper.Map<List<Table>, List<LookupDto<Guid>>>(list));
    }
  }
}
