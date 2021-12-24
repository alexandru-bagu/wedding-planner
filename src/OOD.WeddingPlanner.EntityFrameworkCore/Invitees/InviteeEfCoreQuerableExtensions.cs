using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Invitees
{
    public static class InviteeEfCoreQueryableExtensions
    {
        public static IQueryable<Invitee> IncludeDetails(this IQueryable<Invitee> queryable, bool include = true)
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