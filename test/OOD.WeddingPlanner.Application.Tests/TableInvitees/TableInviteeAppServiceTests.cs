using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.TableInvitees
{
    public class TableInviteeAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly ITableInviteeAppService _tableInviteeAppService;

        public TableInviteeAppServiceTests()
        {
            _tableInviteeAppService = GetRequiredService<ITableInviteeAppService>();
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
