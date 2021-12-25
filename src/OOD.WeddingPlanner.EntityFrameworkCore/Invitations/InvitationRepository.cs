using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace OOD.WeddingPlanner.Invitations
{
  public class InvitationRepository : EfCoreRepository<WeddingPlannerDbContext, Invitation, Guid>, IInvitationRepository
  {
    public InvitationRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id)
    {
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new InvitationWithNavigationProperties()
      {
        Invitation = p,
        Wedding = p.Wedding
      }).SingleAsync();
    }
    
    public async Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(string sorting, int skipCount, int maxResultCount)
    {
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      return await query.Select(p => new InvitationWithNavigationProperties()
      {
        Invitation = p,
        Wedding = p.Wedding
      })
      .OrderBy(string.IsNullOrWhiteSpace(sorting) ? $"{nameof(InvitationWithNavigationProperties.Invitation)}.{nameof(Invitation.Destination)}" : sorting)
      .Skip(skipCount).Take(maxResultCount).ToListAsync();
    }
  }
}