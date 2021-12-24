using System;
using OOD.WeddingPlanner.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OOD.WeddingPlanner.Events
{
  public class EventRepository : EfCoreRepository<WeddingPlannerDbContext, Event, Guid>, IEventRepository
  {
    public EventRepository(IDbContextProvider<WeddingPlannerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
  }
}