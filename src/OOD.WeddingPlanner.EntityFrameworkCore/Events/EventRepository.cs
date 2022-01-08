using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Events
{
    public class EventRepository : EfCoreRepository<WeddingPlannerDbContext, Event, Guid>, IEventRepository
    {
        public EventRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<EventWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
        {
            var query = await this.GetQueryableAsync();
            query = query.IncludeDetails();
            query = query.Where(p => p.Id == id);
            return await query.Select(p => new EventWithNavigationProperties()
            {
                Event = p,
                Wedding = p.Wedding,
                Location = p.Location
            }).SingleAsync();
        }

        public async Task<List<EventWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId, int skipCount, int maxResultCount, string sorting)
        {
            var query = await this.GetQueryableAsync();
            query = query.IncludeDetails();
            return await query.Select(p => new EventWithNavigationProperties()
            {
                Event = p,
                Wedding = p.Wedding,
                Location = p.Location
            })
            .WhereIf(weddingId.HasValue, p => p.Event.WeddingId == weddingId)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(EventWithNavigationProperties.Event)}.{nameof(Event.Name)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        public async Task<long> GetCountAsync(Guid? weddingId)
        {
            var query = await this.GetQueryableAsync();
            query = query.IncludeDetails();
            return await query
            .WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId)
            .LongCountAsync();
        }

        public async Task<List<Event>> GetPagedListAsync(Guid? weddingId, int skipCount, int maxResultCount, string sorting)
        {
            var query = await this.GetQueryableAsync();
            query = query.IncludeDetails();
            return await query
            .WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(EventWithNavigationProperties.Event)}.{nameof(Event.Name)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }
    }
}