using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.InvitationDesigns;
using Volo.Abp.Domain.Repositories;
using Xunit;

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
