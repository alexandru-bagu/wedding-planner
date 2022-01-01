using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public class InvitationDesignRepository : EfCoreRepository<WeddingPlannerDbContext, InvitationDesign, Guid>, IInvitationDesignRepository
    {
        public InvitationDesignRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}