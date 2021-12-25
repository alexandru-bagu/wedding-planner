using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Locations
{
  public interface ILocationRepository : IRepository<Location, Guid>
  {
  }
}