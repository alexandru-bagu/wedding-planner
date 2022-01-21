using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.TableInvitees
{
    public interface ITableInviteeRepository : IRepository<TableInvitee, Guid>
    {
    }
}