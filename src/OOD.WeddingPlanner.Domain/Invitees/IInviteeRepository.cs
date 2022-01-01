using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitees
{
  public interface IInviteeRepository : IRepository<Invitee, Guid>
  {
    Task<long> GetCountAsync(Guid? invitationId, Guid? tableId, Guid? weddingId);
    Task<List<Invitee>> GetPagedListAsync(Guid? invitationId, Guid? tableId, Guid? weddingId, int skipCount, int maxResultCount, string sorting);
    Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
    Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(Guid? invitationId, Guid? tableId, Guid? weddingId, string sorting, int skipCount, int maxResultCount);
  }
}