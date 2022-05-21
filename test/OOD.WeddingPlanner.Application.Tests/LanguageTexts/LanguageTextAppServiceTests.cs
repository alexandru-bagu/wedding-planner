using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public class LanguageTextAppServiceTests : WeddingPlannerApplicationTestBase
    {
        private readonly ILanguageTextAppService _languageTextAppService;

        public LanguageTextAppServiceTests()
        {
            _languageTextAppService = GetRequiredService<ILanguageTextAppService>();
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
