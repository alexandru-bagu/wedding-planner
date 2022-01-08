using OOD.WeddingPlanner.Invitations;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Invitations
{
    public class InvitationRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationRepositoryTests()
        {
            _invitationRepository = GetRequiredService<IInvitationRepository>();
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
