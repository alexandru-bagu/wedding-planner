using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationRepository : IRepository<Invitation, Guid>
    {
        Task<long> GetCountAsync(Guid? weddingId);
        Task<List<Invitation>> GetPagedListAsync(Guid? weddingId, int skipCount, int maxResultCount, string sorting);
        Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId, string sorting, int skipCount, int maxResultCount);
    }
}