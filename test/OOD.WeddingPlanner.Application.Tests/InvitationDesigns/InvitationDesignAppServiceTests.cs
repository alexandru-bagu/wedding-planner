using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.InvitationDesigns
{
    public class InvitationDesignAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly IInvitationDesignAppService _invitationDesignAppService;

        public InvitationDesignAppServiceTests()
        {
            _invitationDesignAppService = GetRequiredService<IInvitationDesignAppService>();
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
