using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public static class LanguageTextEfCoreQueryableExtensions
    {
        public static IQueryable<LanguageText> IncludeDetails(this IQueryable<LanguageText> queryable, bool include = true)
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