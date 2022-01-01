using System;
using OOD.WeddingPlanner.Events.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
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
    Task<PagedResultDto<EventWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetEventsInputDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetLookupListAsync(LookupEventsInputDto input);
  }
}