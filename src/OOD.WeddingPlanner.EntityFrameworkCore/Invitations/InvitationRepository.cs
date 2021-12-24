using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Invitations
{
  public class InvitationRepository : EfCoreRepository<WeddingPlannerDbContext, Invitation, Guid>, IInvitationRepository
  {
    public InvitationRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
  }
}