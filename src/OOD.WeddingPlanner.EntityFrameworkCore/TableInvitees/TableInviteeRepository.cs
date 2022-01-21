using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.TableInvitees
{
    public class TableInviteeRepository : EfCoreRepository<WeddingPlannerDbContext, TableInvitee, Guid>, ITableInviteeRepository
    {
        public TableInviteeRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}