using System;
using System.Threading.Tasks;
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
        Task<InviteeWithNavigationPropertiesDto> GetWithNavigationById(Guid id);
    }
}