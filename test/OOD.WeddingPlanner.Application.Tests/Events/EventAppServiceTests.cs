using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Events
{
    public class EventAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly IEventAppService _eventAppService;

        public EventAppServiceTests()
        {
            _eventAppService = GetRequiredService<IEventAppService>();
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
