using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitees
{
  public interface IInviteeRepository : IRepository<Invitee, Guid>
  {
    Task<InviteeWithNavigationProperties> GetWithNavigationById(Guid id);
  }
}