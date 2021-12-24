using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOD.WeddingPlanner.EntityFrameworkCore;
using OOD.WeddingPlanner.Weddings.Dtos;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Weddings
{
  public class WeddingRepository : EfCoreRepository<WeddingPlannerDbContext, Wedding, Guid>, IWeddingRepository
  {
    public WeddingRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<WeddingWithNavigationProperties> GetWithNavigationById(Guid id)
    {
      var query = await this.GetQueryableAsync();
      query = query.IncludeDetails();
      query = query.Where(p => p.Id == id);
      return await query.Select(p => new WeddingWithNavigationProperties()
      {
        Wedding = p,
        Events = p.Events,
        Invitations = p.Invitations
      }).SingleAsync();
    }
  }
}