using OOD.WeddingPlanner.InvitationDesigns;

namespace OOD.WeddingPlanner.EntityFrameworkCore.InvitationDesigns
{
    public class InvitationDesignRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly IInvitationDesignRepository _invitationDesignRepository;

        public InvitationDesignRepositoryTests()
        {
            _invitationDesignRepository = GetRequiredService<IInvitationDesignRepository>();
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
