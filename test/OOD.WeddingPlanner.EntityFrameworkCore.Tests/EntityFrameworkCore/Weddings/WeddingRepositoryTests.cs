using OOD.WeddingPlanner.Weddings;

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
