using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.TableMenus;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.TableMenus
{
    public class TableMenuRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly ITableMenuRepository _tableMenuRepository;

        public TableMenuRepositoryTests()
        {
            _tableMenuRepository = GetRequiredService<ITableMenuRepository>();
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
