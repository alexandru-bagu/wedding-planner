using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OOD.WeddingPlanner.Tables
{
    public static class TableEfCoreQueryableExtensions
    {
        public static IQueryable<Table> IncludeDetails(this IQueryable<Table> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(p => p.Event)
                .Include(p => p.TableAssignments);
        }
    }
}