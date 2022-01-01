using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Tables.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Tables
{
    public interface ITableAppService :
        ICrudAppService< 
            TableDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateTableDto,
            CreateUpdateTableDto>
  {
    Task<TableWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id);
    Task<PagedResultDto<TableWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetTablesInputDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupTablesInputDto input);
  }
}