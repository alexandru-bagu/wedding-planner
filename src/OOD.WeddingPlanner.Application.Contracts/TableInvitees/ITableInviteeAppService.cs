using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.TableInvitees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.TableInvitees
{
    public interface ITableInviteeAppService :
        ICrudAppService< 
            TableInviteeDto, 
            Guid,
            GetTableInviteesDto,
            CreateUpdateTableInviteeDto,
            CreateUpdateTableInviteeDto>
    {
        Task<PagedResultDto<TableInviteeWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetTableInviteesDto input);
    }
}