using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OOD.WeddingPlanner.Locations
{
    public static class LocationEfCoreQueryableExtensions
    {
        public static IQueryable<Location> IncludeDetails(this IQueryable<Location> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(p => p.Events);
        }
    }
}