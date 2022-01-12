using OOD.WeddingPlanner.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public class InvitationDesignRepository : EfCoreRepository<WeddingPlannerDbContext, InvitationDesign, Guid>, IInvitationDesignRepository
    {
        public InvitationDesignRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider, IServiceProvider serviceProvider) : base(dbContextProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}