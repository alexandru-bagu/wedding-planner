using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OOD.WeddingPlanner.TableInvitees
{
    public static class TableInviteeEfCoreQueryableExtensions
    {
        public static IQueryable<TableInvitee> IncludeDetails(this IQueryable<TableInvitee> queryable, bool include = true)
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