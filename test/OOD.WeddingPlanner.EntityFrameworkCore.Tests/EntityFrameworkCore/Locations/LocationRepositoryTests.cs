using OOD.WeddingPlanner.Locations;

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
