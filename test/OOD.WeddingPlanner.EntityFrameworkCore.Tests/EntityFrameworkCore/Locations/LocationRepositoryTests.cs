using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Locations;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Locations
{
    public class LocationRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationRepositoryTests()
        {
            _locationRepository = GetRequiredService<ILocationRepository>();
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
