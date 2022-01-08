using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public interface IInvitationDesignAppService :
        ICrudAppService<
            InvitationDesignDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateInvitationDesignDto,
            CreateUpdateInvitationDesignDto>
    {
        Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input);
    }
}