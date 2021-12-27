using System;
using System.Collections.Generic;
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
          GetInviteesInputDto,
          CreateUpdateInviteeDto,
          CreateUpdateInviteeDto>
  {
    Task<InviteeWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id);
    Task<PagedResultDto<InviteeWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetInviteesInputDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input);
  }
}