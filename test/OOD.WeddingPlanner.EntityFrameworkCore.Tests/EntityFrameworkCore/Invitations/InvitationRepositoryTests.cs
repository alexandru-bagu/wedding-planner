using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Invitations;
using Volo.Abp.Domain.Repositories;
using Xunit;

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
