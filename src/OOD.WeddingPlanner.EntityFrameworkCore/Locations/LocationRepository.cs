using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Locations
{
    public class LocationRepository : EfCoreRepository<WeddingPlannerDbContext, Location, Guid>, ILocationRepository
    {
        public LocationRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}