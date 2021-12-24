using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Weddings
{
    public class WeddingAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly IWeddingAppService _weddingAppService;

        public WeddingAppServiceTests()
        {
            _weddingAppService = GetRequiredService<IWeddingAppService>();
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
