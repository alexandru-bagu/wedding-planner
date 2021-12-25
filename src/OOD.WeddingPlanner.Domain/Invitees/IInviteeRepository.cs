using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitees
{
  public interface IInviteeRepository : IRepository<Invitee, Guid>
  {
    Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
    Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(string sorting, int skipCount, int maxResultCount);
  }
}