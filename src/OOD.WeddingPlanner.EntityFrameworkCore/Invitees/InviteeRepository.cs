using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Invitees
{
    public class InviteeRepository : EfCoreRepository<WeddingPlannerDbContext, Invitee, Guid>, IInviteeRepository
    {
        public InviteeRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IQueryable<InviteeWithNavigationProperties>> GetQueryableWithNavigation()
        {
            var query = await GetQueryableAsync();
            query = query.IncludeDetails();
            return query.Select(p => new InviteeWithNavigationProperties()
            {
                Invitee = p,
                Invitation = p.Invitation,
                Wedding = p.Invitation == null ? null : p.Invitation.Wedding,
                Table = p.Table
            });
        }

        public async Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
        {
            var query = await GetQueryableWithNavigation();
            query = query.Where(p => p.Invitee.Id == id);
            return await query.SingleAsync();
        }

        public async Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(string filter, Guid? invitationId, Guid? tableId, Guid? weddingId, string name, string surname, bool? confirmed, string sorting, int skipCount, int maxResultCount)
        {
            var query = await GetQueryableWithNavigation();

            return await applyFilters(query, filter, invitationId, tableId, weddingId, name, surname, confirmed)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InviteeWithNavigationProperties.Invitee)}.{nameof(Invitee.Surname)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        public async Task<long> GetCountAsync(string filter, Guid? invitationId, Guid? tableId, Guid? weddingId, string name, string surname, bool? confirmed)
        {
            var query = await GetQueryableWithNavigation();
            return await applyFilters(query, filter, invitationId, tableId, weddingId, name, surname, confirmed)
            .LongCountAsync();
        }

        public async Task<List<Invitee>> GetPagedListAsync(string filter, Guid? invitationId, Guid? tableId, Guid? weddingId, string name, string surname, bool? confirmed, int skipCount, int maxResultCount, string sorting)
        {
            var query = await GetQueryableWithNavigation();
            return await applyFilters(query, filter, invitationId, tableId, weddingId, name, surname, confirmed)
            .Select(p => p.Invitee)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(Invitee.Surname)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        protected IQueryable<InviteeWithNavigationProperties> applyFilters(IQueryable<InviteeWithNavigationProperties> query, string filter = null, Guid? invitationId = null, Guid? tableId = null, Guid? weddingId = null, string name = null, string surname = null, bool? confirmed = null)
        {
            query = query.WhereIf(invitationId.HasValue, p => p.Invitee.InvitationId == invitationId);
            query = query.WhereIf(tableId.HasValue, p => p.Invitee.TableId == tableId);
            query = query.WhereIf(weddingId.HasValue, p => p.Invitation.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(filter),
                p => p.Invitee.Name.Contains(filter) || p.Invitee.Surname.Contains(filter) ||
                p.Table.Name.Contains(filter) || p.Wedding.Name.Contains(filter) ||
                p.Invitation.Destination.Contains(filter));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(name),
                p => p.Invitee.Name.Contains(name));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(surname),
                p => p.Invitee.Surname.Contains(surname));
            query = query.WhereIf(confirmed.HasValue,
                p => p.Invitee.Confirmed == confirmed);
            return query;
        }
    }
}