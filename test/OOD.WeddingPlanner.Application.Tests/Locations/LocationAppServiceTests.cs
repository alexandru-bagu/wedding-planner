using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Locations
{
    public class LocationAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly ILocationAppService _locationAppService;

        public LocationAppServiceTests()
        {
            _locationAppService = GetRequiredService<ILocationAppService>();
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
