using System;
using OOD.WeddingPlanner.Events.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Events
{
  public interface IEventAppService :
      ICrudAppService<
          EventDto,
          Guid,
          GetEventsInputDto,
          CreateUpdateEventDto,
          CreateUpdateEventDto>
  {
    Task<EventWithNavigationPropertiesDto> GetWithNavigationByIdAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupRequestDto input);
  }
}