using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Weddings
{
    public class WeddingRepository : EfCoreRepository<WeddingPlannerDbContext, Wedding, Guid>, IWeddingRepository
    {
        public WeddingRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}