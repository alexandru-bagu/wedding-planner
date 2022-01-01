using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.Invitees
{
  public class InviteeRepository : EfCoreRepository<WeddingPlannerDbContext, Invitee, Guid>, IInviteeRepository
  {
    public InviteeRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
    {
      var query = await GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new InviteeWithNavigationProperties()
      {
        Invitee = p,
        Invitation = p.Invitation,
        Wedding = p.Invitation == null ? null : p.Invitation.Wedding,
        Table = p.Table
      }).SingleAsync();
    }

    public async Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(Guid? invitationId, Guid? tableId, Guid? weddingId, string sorting, int skipCount, int maxResultCount)
    {
      var query = await GetQueryableAsync();
      query = query.IncludeDetails();
      return await query.Select(p => new InviteeWithNavigationProperties()
      {
        Invitee = p,
        Invitation = p.Invitation,
        Wedding = p.Invitation == null ? null : p.Invitation.Wedding,
        Table = p.Table
      })
      .WhereIf(invitationId.HasValue, p => p.Invitee.InvitationId == invitationId)
      .WhereIf(tableId.HasValue, p => p.Invitee.TableId == tableId)
      .WhereIf(tableId.HasValue, p => p.Invitation != null && p.Invitation.WeddingId == weddingId)
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InviteeWithNavigationProperties.Invitee)}.{nameof(Invitee.Surname)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }

    public async Task<long> GetCountAsync(Guid? invitationId, Guid? tableId, Guid? weddingId)
    {
      var query = await GetQueryableAsync();
      return await query.IncludeDetails()
      .WhereIf(invitationId.HasValue, p => p.InvitationId == invitationId)
      .WhereIf(tableId.HasValue, p => p.TableId == tableId)
      .WhereIf(weddingId.HasValue, p => p.Invitation != null && p.Invitation.WeddingId == weddingId)
      .LongCountAsync();
    }

    public async Task<List<Invitee>> GetPagedListAsync(Guid? invitationId, Guid? tableId, Guid? weddingId, int skipCount, int maxResultCount, string sorting)
    {
      var query = await GetQueryableAsync();
      return await query.IncludeDetails()
      .WhereIf(invitationId.HasValue, p => p.InvitationId == invitationId)
      .WhereIf(tableId.HasValue, p => p.TableId == tableId)
      .WhereIf(weddingId.HasValue, p => p.Invitation != null && p.Invitation.WeddingId == weddingId)
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(Invitee.Surname)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }
  }
}