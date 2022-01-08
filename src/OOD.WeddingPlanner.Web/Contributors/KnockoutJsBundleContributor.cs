using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
    public class KnockoutJsBundleContributor : IBundleContributor
    {
        public KnockoutJsBundleContributor()
        {

        }

        public Task ConfigureBundleAsync(BundleConfigurationContext context)
        {
            context.Files.Add("/libs/knockout/build/output/knockout-latest.js");
            return Task.CompletedTask;
        }

        public Task ConfigureDynamicResourcesAsync(BundleConfigurationContext context)
        {
            return Task.CompletedTask;
        }

        public Task PostConfigureBundleAsync(BundleConfigurationContext context)
        {
            return Task.CompletedTask;
        }

        public Task PreConfigureBundleAsync(BundleConfigurationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
