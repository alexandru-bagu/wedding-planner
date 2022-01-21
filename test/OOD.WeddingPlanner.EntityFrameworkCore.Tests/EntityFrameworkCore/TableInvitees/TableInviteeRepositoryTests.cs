using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.TableInvitees;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.TableInvitees
{
    public class TableInviteeRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly ITableInviteeRepository _tableInviteeRepository;

        public TableInviteeRepositoryTests()
        {
            _tableInviteeRepository = GetRequiredService<ITableInviteeRepository>();
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
