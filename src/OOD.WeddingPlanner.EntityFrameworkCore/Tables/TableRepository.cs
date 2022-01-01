using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace OOD.WeddingPlanner.Tables
{
  public class TableRepository : EfCoreRepository<WeddingPlannerDbContext, Table, Guid>, ITableRepository
  {
    public TableRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<long> GetCountAsync(Guid? eventId)
    {
      var query = await GetQueryableAsync();
      query = query.WhereIf(eventId.HasValue, p => p.EventId == eventId);
      return await query
      .LongCountAsync();
    }

    public async Task<List<TableWithNavigationProperties>> GetListWithNavigationAsync(Guid? eventId, int skipCount, int maxResultCount, string sorting)
    {
      var query = await GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.WhereIf(eventId.HasValue, p => p.EventId == eventId);
      return await query.Select(p => new TableWithNavigationProperties()
      {
        Table = p,
        Event = p.Event,
        Invitees = p.Invitees
      })
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(TableWithNavigationProperties.Table)}.{nameof(Table.Name)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }

    public async Task<List<Table>> GetPagedListAsync(Guid? eventId, int skipCount, int maxResultCount, string sorting)
    {
      var query = await GetQueryableAsync();
      query = query.WhereIf(eventId.HasValue, p => p.EventId == eventId);
      return await query
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(TableWithNavigationProperties.Table)}.{nameof(Table.Name)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }

    public async Task<TableWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
    {
      var query = await GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new TableWithNavigationProperties()
      {
        Table = p,
        Event = p.Event,
        Invitees = p.Invitees
      })
      .SingleAsync();
    }
  }
}