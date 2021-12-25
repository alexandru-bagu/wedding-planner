using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
  public interface IInvitationRepository : IRepository<Invitation, Guid>
  {
    Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
    Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(string sorting, int skipCount, int maxResultCount);
  }
}