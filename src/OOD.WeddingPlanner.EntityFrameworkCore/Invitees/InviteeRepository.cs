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
            });
        }

        public async Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
        {
            var query = await GetQueryableWithNavigation();
            query = query.Where(p => p.Invitee.Id == id);
            return await query.SingleAsync();
        }

        public async Task<Invitee> GetPlusOneByIdAsync(Guid id)
        {
            var invitee = await (await GetQueryableAsync()).Where(p => p.Id == id).FirstOrDefaultAsync();
            var evt = await (await GetQueryableAsync()).Where(p => p.InvitationId == invitee.InvitationId).ToListAsync();
            return await ((await GetQueryableAsync()).Where(p => p.InvitationId == invitee.InvitationId && p.PlusOne != invitee.PlusOne)).FirstOrDefaultAsync();
        }

        public async Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation, string sorting, int skipCount, int maxResultCount)
        {
            var query = await GetQueryableWithNavigation();

            return await applyFilters(query, filter, invitationId, weddingId, name, surname, confirmed, child, hasInvitation)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InviteeWithNavigationProperties.Invitee)}.{nameof(Invitee.Surname)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        public async Task<long> GetCountAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation)
        {
            var query = await GetQueryableWithNavigation();
            return await applyFilters(query, filter, invitationId, weddingId, name, surname, confirmed, child, hasInvitation)
            .LongCountAsync();
        }

        public async Task<List<Invitee>> GetPagedListAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation, int skipCount, int maxResultCount, string sorting)
        {
            var query = await GetQueryableWithNavigation();
            return await applyFilters(query, filter, invitationId, weddingId, name, surname, confirmed, child, hasInvitation)
            .Select(p => p.Invitee)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(Invitee.Surname)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        protected IQueryable<InviteeWithNavigationProperties> applyFilters(IQueryable<InviteeWithNavigationProperties> query, string filter = null, Guid? invitationId = null, Guid? weddingId = null, string name = null, string surname = null, bool? confirmed = null, bool? child = null, bool? hasInvitation = null)
        {
            query = query.WhereIf(invitationId.HasValue, p => p.Invitee.InvitationId == invitationId);
            query = query.WhereIf(weddingId.HasValue, p => p.Invitation.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(filter),
                p => p.Invitee.Name.Contains(filter) || p.Invitee.Surname.Contains(filter) ||
                p.Wedding.Name.Contains(filter) || p.Invitation.Destination.Contains(filter));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(name),
                p => p.Invitee.Name.Contains(name));
            query = query.WhereIf(!string.IsNullOrWhiteSpace(surname),
                p => p.Invitee.Surname.Contains(surname));
            query = query.WhereIf(confirmed.HasValue,
                p => p.Invitee.Confirmed == confirmed);
            query = query.WhereIf(child.HasValue,
                p => p.Invitee.Child == child);
            query = query.WhereIf(hasInvitation.HasValue,
                p => (hasInvitation.Value && p.Invitee.InvitationId != null) || 
                    (!hasInvitation.Value && p.Invitee.InvitationId == null));
            return query;
        }
    }
}