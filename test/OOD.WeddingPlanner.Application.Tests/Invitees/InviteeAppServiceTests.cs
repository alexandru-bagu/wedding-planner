using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Invitees
{
    public class InviteeAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly IInviteeAppService _inviteeAppService;

        public InviteeAppServiceTests()
        {
            _inviteeAppService = GetRequiredService<IInviteeAppService>();
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
