using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Events;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.Events
{
    public class EventRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly IEventRepository _eventRepository;

        public EventRepositoryTests()
        {
            _eventRepository = GetRequiredService<IEventRepository>();
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
