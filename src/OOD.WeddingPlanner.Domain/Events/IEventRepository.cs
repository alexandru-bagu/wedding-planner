using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Events
{
  public interface IEventRepository : IRepository<Event, Guid>
  {
    Task<EventWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
    Task<List<EventWithNavigationProperties>> GetListWithNavigationAsync(string sorting, int skipCount, int maxResultCount);
  }
}