using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public interface IInvitationDesignRepository : IRepository<InvitationDesign, Guid>
    {
    }
}