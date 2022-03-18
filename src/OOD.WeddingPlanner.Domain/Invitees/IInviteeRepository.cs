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
        Task<long> GetCountAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation);
        Task<List<Invitee>> GetPagedListAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation, int skipCount, int maxResultCount, string sorting);
        Task<InviteeWithNavigationProperties> GetWithNavigationByIdAsync(Guid id);
        Task<Invitee> GetPlusOneByIdAsync(Guid id);
        Task<List<InviteeWithNavigationProperties>> GetListWithNavigationAsync(string filter, Guid? invitationId, Guid? weddingId, string name, string surname, bool? confirmed, bool? child, bool? hasInvitation, string sorting, int skipCount, int maxResultCount);
    }
}