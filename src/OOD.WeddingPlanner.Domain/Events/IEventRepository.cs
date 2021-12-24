using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Events
{
  public interface IEventRepository : IRepository<Event, Guid>
  {
  }
}