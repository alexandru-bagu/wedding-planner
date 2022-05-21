using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OOD.WeddingPlanner.TableMenus
{
    public static class TableMenuEfCoreQueryableExtensions
    {
        public static IQueryable<TableMenu> IncludeDetails(this IQueryable<TableMenu> queryable, bool include = true)
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