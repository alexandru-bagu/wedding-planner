using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.TableMenus
{
    public class TableMenuRepository : EfCoreRepository<WeddingPlannerDbContext, TableMenu, Guid>, ITableMenuRepository
    {
        public TableMenuRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}