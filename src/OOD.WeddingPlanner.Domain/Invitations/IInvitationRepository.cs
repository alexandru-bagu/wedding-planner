using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationRepository : IRepository<Invitation, Guid>
    {
        Task<long> GetCountAsync(Guid? weddingId, string destination);
        Task<List<Invitation>> GetPagedListAsync(Guid? weddingId, string destination, int skipCount, int maxResultCount, string sorting);
        Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId, string destination, string sorting, int skipCount, int maxResultCount);
    }
}