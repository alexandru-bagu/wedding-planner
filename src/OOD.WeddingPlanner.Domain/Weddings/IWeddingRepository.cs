using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.Weddings
{
    public interface IWeddingRepository : IRepository<Wedding, Guid>
    {
    }
}