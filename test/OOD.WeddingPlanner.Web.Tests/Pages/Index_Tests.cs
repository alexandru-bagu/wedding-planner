using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace OOD.WeddingPlanner.Pages
{
    public class Index_Tests : WeddingPlannerWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
