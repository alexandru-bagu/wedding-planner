using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Locations
{
  public interface ILocationRepository : IRepository<Location, Guid>
  {
  }
}