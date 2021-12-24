using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationRepository : IRepository<Invitation, Guid>
    {
    }
}