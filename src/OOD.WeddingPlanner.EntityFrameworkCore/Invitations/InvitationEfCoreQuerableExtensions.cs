using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OOD.WeddingPlanner.Invitations
{
    public static class InvitationEfCoreQueryableExtensions
    {
        public static IQueryable<Invitation> IncludeDetails(this IQueryable<Invitation> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(p => p.Invitees)
                .Include(p => p.Wedding).ThenInclude(p => p.Events)
                .Include(p => p.Design);
        }
    }
}