using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Invitations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationAppService :
        ICrudAppService< 
            InvitationDto, 
            Guid, 
            GetInvitationsInputDto,
            CreateUpdateInvitationDto,
            CreateUpdateInvitationDto>
    {
        Task<InvitationWithNavigationPropertiesDto> GetWithNavigationById(Guid id);
    }
}