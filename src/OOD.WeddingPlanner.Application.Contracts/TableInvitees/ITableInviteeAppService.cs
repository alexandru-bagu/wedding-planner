using System;
using OOD.WeddingPlanner.TableInvitees.Dtos;
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

    }
}