using System.Linq;
using Microsoft.EntityFrameworkCore;

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
          .Include(p => p.Wedding);
    }
  }
}