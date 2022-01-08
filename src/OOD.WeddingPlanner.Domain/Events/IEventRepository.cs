using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Events
{
    public interface IEventRepository : IRepository<Event, Guid>
    {
        Task<long> GetCountAsync(Guid? weddingId);
        Task<List<Event>> GetPagedListAsync(Guid? weddingId, int skipCount, int maxResultCount, string sorting);
        Task<EventWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<List<EventWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId, int skipCount, int maxResultCount, string sorting);
    }
}