using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly IInvitationAppService _invitationAppService;

        public InvitationAppServiceTests()
        {
            _invitationAppService = GetRequiredService<IInvitationAppService>();
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
