using System.Linq;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public static class InvitationDesignEfCoreQueryableExtensions
    {
        public static IQueryable<InvitationDesign> IncludeDetails(this IQueryable<InvitationDesign> queryable, bool include = true)
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