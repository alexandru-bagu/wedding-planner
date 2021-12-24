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
          // .Include(x => x.xxx) // TODO: AbpHelper generated
          ;
    }
  }
}