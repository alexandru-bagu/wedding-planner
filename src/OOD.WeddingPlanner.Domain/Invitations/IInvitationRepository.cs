using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitations
{
    public interface IInvitationRepository : IRepository<Invitation, Guid>
    {
        Task<InvitationWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<InvitationWithNavigationProperties> GetWithFullNavigationByIdAsync(Guid id);
        Task<InvitationWithNavigationProperties> GetWithFullNavigationByCodeAsync(string code);
        Task<long> GetCountAsync(Guid? weddingId = null, string destination = null);
        Task<List<Invitation>> GetListAsync(Guid? weddingId = null, string destination = null, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue);
        Task<List<InvitationWithNavigationProperties>> GetListWithNavigationAsync(Guid? weddingId = null, string destination = null, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue);
        Task<bool> UniqueCodeUsedAsync(string unique);
    }
}