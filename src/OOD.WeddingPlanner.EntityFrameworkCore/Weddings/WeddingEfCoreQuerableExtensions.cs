using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OOD.WeddingPlanner.Weddings
{
    public static class WeddingEfCoreQueryableExtensions
    {
        public static IQueryable<Wedding> IncludeDetails(this IQueryable<Wedding> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                 .Include(p => p.Events)
                 .Include(p => p.Invitations);
        }
    }
}