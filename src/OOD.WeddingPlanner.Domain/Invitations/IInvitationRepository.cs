using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
  public interface IInvitationRepository : IRepository<Invitation, Guid>
  {
    Task<InvitationWithNavigationProperties> GetWithNavigationById(Guid id);
  }
}