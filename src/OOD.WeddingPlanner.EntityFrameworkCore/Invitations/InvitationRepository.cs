using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationRepository : EfCoreRepository<WeddingPlannerDbContext, Invitation, Guid>, IInvitationRepository
    {
        public InvitationRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider, IServiceProvider serviceProvider) : base(dbContextProvider)
        {
            ServiceProvider = serviceProvider;
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

        public async Task<InvitationWithNavigationProperties> GetWithFullNavigationByIdAsync(Guid id)
        {
            var query = await GetQueryableAsync();
            query = query.Include(p => p.Invitees)
                .Include(p => p.Wedding).ThenInclude(p => p.Events).ThenInclude(p => p.Location)
                .Include(p => p.Design);
            query = query.Where(p => p.Id == id);
            var result = await query.Select(p => new InvitationWithNavigationProperties()
            {
                Invitation = p,
                Wedding = p.Wedding,
                Design = p.Design
            }).SingleAsync();
            result.Wedding.Events = result.Wedding.Events.OrderBy(p => p.Time).ToList();
            result.Invitation.Invitees = result.Invitation.Invitees.OrderBy(p => p.Order).ToList();
            return result;
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

        public async Task<List<Invitation>> GetListAsync(Guid? weddingId, string destination, string sorting, int skipCount, int maxResultCount)
        {
            var query = await GetQueryableAsync();
            query = query.WhereIf(weddingId.HasValue, p => p.WeddingId == weddingId);
            query = query.WhereIf(!string.IsNullOrWhiteSpace(destination), p => p.Destination.Contains(destination));
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(Invitation.Destination)}" : sorting);
            query = query.Skip(skipCount).Take(maxResultCount);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<bool> UniqueCodeUsedAsync(string unique)
        {
            using (DataFilter.Disable<IMultiTenant>())
                return (await (await GetQueryableAsync()).Where(p => p.UniqueCode == unique).CountAsync()) > 0;
        }

        public async Task<InvitationWithNavigationProperties> GetWithFullNavigationByCodeAsync(string code)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                var query = await GetQueryableAsync();
                query = query.Include(p => p.Invitees)
                    .Include(p => p.Wedding).ThenInclude(p => p.Events).ThenInclude(p => p.Location)
                    .Include(p => p.Design);
                query = query.Where(p => p.UniqueCode == code);
                var result = await query.Select(p => new InvitationWithNavigationProperties()
                {
                    Invitation = p,
                    Wedding = p.Wedding,
                    Design = p.Design
                }).SingleAsync();
                result.Wedding.Events = result.Wedding.Events.OrderBy(p => p.Time).ToList();
                result.Invitation.Invitees = result.Invitation.Invitees.OrderBy(p => p.Order).ToList();
                return result;
            }
        }
    }
}