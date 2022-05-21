using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.TableMenus
{
    public interface ITableMenuRepository : IRepository<TableMenu, Guid>
    {
    }
}