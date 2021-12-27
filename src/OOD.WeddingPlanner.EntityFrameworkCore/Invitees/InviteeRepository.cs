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
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new InviteeWithNavigationProperties()
      {
        Invitee = p,
        Invitation = p.Invitation,
        Wedding = p.Invitation == null ? null : p.Invitation.Wedding
      }).SingleAsync();
    }

    public async Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(Guid? invitationId, string sorting, int skipCount, int maxResultCount)
    {
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      return await query.Select(p => new InviteeWithNavigationProperties()
      {
        Invitee = p,
        Invitation = p.Invitation,
        Wedding = p.Invitation == null ? null : p.Invitation.Wedding
      })
      .WhereIf(invitationId.HasValue, p => p.Invitee.InvitationId == invitationId)
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InviteeWithNavigationProperties.Invitee)}.{nameof(Invitee.Surname)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }
  }
}