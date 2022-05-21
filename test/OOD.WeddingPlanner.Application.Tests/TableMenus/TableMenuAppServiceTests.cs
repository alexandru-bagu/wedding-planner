using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.TableMenus
{
    public class TableMenuAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly ITableMenuAppService _tableMenuAppService;

        public TableMenuAppServiceTests()
        {
            _tableMenuAppService = GetRequiredService<ITableMenuAppService>();
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
