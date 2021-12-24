using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Weddings;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Weddings
{
    public class WeddingRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly IWeddingRepository _weddingRepository;

        public WeddingRepositoryTests()
        {
            _weddingRepository = GetRequiredService<IWeddingRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
