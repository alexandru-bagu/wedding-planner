using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Invitees
{
    public class InviteeRepository : EfCoreRepository<WeddingPlannerDbContext, Invitee, Guid>, IInviteeRepository
    {
        public InviteeRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}