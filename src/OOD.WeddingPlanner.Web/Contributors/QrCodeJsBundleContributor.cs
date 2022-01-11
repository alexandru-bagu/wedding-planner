using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
    public class QrCodeJsBundleContributor : IBundleContributor
    {
        public QrCodeJsBundleContributor()
        {

        }

        public Task ConfigureBundleAsync(BundleConfigurationContext context)
        {
            context.Files.Add("/libs/qrcodejs/qrcode.min.js");
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
