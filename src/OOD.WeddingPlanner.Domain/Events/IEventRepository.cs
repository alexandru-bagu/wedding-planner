using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Events
{
  public interface IEventRepository : IRepository<Event, Guid>
  {
    Task<EventWithNavigationProperties> GetWithNavigationById(Guid id);
  }
}