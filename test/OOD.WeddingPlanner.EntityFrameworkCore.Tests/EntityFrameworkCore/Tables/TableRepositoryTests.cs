using OOD.WeddingPlanner.Tables;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Tables
{
    public class TableRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly ITableRepository _tableRepository;

        public TableRepositoryTests()
        {
            _tableRepository = GetRequiredService<ITableRepository>();
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
