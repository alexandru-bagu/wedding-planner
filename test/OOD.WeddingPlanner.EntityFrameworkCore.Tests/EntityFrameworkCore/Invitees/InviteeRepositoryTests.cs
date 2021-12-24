using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Invitees;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Invitees
{
    public class InviteeRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly IInviteeRepository _inviteeRepository;

        public InviteeRepositoryTests()
        {
            _inviteeRepository = GetRequiredService<IInviteeRepository>();
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
