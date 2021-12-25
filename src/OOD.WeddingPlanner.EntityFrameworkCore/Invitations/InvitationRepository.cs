using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
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
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new InvitationWithNavigationProperties()
      {
        Invitation = p,
        Wedding = p.Wedding
      }).SingleAsync();
    }
  }
}