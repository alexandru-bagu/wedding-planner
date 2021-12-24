using System;
using OOD.WeddingPlanner.Invitations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationAppService :
        ICrudAppService< 
            InvitationDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateInvitationDto,
            CreateUpdateInvitationDto>
    {

    }
}