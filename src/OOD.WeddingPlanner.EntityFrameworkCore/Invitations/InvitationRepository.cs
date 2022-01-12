using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationRepository : EfCoreRepository<WeddingPlannerDbContext, Invitation, Guid>, IInvitationRepository
    {
        public InvitationRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
        {
            var query = await GetQueryableAsync();
            query = query.IncludeDetails();
            query = query.Where(p => p.Id == id);
            return await query.Select(p => new InvitationWithNavigationProperties()
            {
                Invitation = p,
                Wedding = p.Wedding,
                Design = p.Design
            }).SingleAsync();
        }

        public async Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId, string destination, string sorting, int skipCount, int maxResultCount)
        {
            var query = await GetQueryableAsync();
            query = query.IncludeDetails();
            query = query.WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(destination), p => p.Destination.Contains(destination));
            return await query.Select(p => new InvitationWithNavigationProperties()
            {
                Invitation = p,
                Wedding = p.Wedding,
                Design = p.Design
            })
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InvitationWithNavigationProperties.Invitation)}.{nameof(Invitation.Destination)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        public async Task<long> GetCountAsync(Guid? weddingId, string destination)
        {
            var query = await GetQueryableAsync();
            query = query.WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(destination), p => p.Destination.Contains(destination));
            return await query.LongCountAsync();
        }

        public async Task<List<Invitation>> GetPagedListAsync(Guid? weddingId, string destination, int skipCount, int maxResultCount, string sorting)
        {
            var query = await GetQueryableAsync();
            query = query.WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(destination), p => p.Destination.Contains(destination));
            return await query
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InvitationWithNavigationProperties.Invitation)}.{nameof(Invitation.Destination)}" : sorting)
            .Skip(skipCount).Take(maxResultCount).ToListAsync();
        }
    }
}