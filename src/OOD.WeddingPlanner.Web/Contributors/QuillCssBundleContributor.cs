using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
  public class QuillCssBundleContributor : IBundleContributor
  {
    public QuillCssBundleContributor()
    {

    }

    public Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
      context.Files.Add("/libs/quill/dist/quill.snow.css");
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
