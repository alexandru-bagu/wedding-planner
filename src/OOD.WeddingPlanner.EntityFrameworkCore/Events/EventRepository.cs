using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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

    public async Task<List<EventWithNavigationProperties>> GetListWithNavigationAsync(string sorting, int skipCount, int maxResultCount)
    {
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      return await query.OrderBy(sorting).Skip(skipCount).Take(maxResultCount).Select(p => new EventWithNavigationProperties()
      {
        Event = p,
        Wedding = p.Wedding,
        Location = p.Location
      }).ToListAsync();
    }
  }
}