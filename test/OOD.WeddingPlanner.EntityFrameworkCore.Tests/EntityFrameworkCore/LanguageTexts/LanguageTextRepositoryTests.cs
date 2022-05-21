using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.LanguageTexts;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace OOD.WeddingPlanner.EntityFrameworkCore.LanguageTexts
{
    public class LanguageTextRepositoryTests : WeddingPlannerEntityFrameworkCoreTestBase
    {
        private readonly ILanguageTextRepository _languageTextRepository;

        public LanguageTextRepositoryTests()
        {
            _languageTextRepository = GetRequiredService<ILanguageTextRepository>();
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
