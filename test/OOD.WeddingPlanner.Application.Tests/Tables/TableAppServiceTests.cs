using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Tables
{
    public class TableAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly ITableAppService _tableAppService;

        public TableAppServiceTests()
        {
            _tableAppService = GetRequiredService<ITableAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
