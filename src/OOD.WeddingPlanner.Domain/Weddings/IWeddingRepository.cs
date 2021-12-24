using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Weddings.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Weddings
{
  public interface IWeddingRepository : IRepository<Wedding, Guid>
  {
    Task<WeddingWithNavigationProperties> GetWithNavigationById(Guid id);
  }
}