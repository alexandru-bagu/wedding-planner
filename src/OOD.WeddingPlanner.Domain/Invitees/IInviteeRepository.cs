using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitees
{
  public interface IInviteeRepository : IRepository<Invitee, Guid>
  {
  }
}