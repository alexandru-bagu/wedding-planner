using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Invitees
{
    public interface IInviteeRepository : IRepository<Invitee, Guid>
    {
        Task<IQueryable<InviteeWithNavigationProperties>> GetQueryableWithNavigation();
        Task<long> GetCountAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed);
        Task<List<Invitee>> GetPagedListAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, int skipCount, int maxResultCount, string sorting);
        Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, string sorting, int skipCount, int maxResultCount);
    }
}