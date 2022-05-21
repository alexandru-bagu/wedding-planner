using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public class LanguageTextRepository : EfCoreRepository<WeddingPlannerDbContext, LanguageText, Guid>, ILanguageTextRepository
    {
        public LanguageTextRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}