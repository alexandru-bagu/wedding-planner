using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

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
        Invitation = p.Invitation
      }).SingleAsync();
    }
  }
}