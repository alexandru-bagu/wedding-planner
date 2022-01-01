using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Tables
{
  public interface ITableRepository : IRepository<Table, Guid>
  {
    Task<long> GetCountAsync(Guid? eventId);
    Task<List<Table>> GetPagedListAsync(Guid? eventId, int skipCount, int maxResultCount, string sorting);
    Task<TableWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
    Task<List<TableWithNavigationProperties>> GetListWithNavigationAsync(Guid? eventId, int skipCount, int maxResultCount, string sorting);
  }
}