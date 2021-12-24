using System;
using OOD.WeddingPlanner.Invitees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitees
{
    public interface IInviteeAppService :
        ICrudAppService< 
            InviteeDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateInviteeDto,
            CreateUpdateInviteeDto>
    {

    }
}