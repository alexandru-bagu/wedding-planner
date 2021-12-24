using System.Linq;
using Microsoft.EntityFrameworkCore;

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
          // .Include(x => x.xxx) // TODO: AbpHelper generated
          ;
    }
  }
}