using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

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
                Assignments = p.TableAssignments
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
                Assignments = p.TableAssignments
            })
            .SingleAsync();
        }
    }
}