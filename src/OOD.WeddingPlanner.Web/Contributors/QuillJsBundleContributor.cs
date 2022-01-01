using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
  public class QuillJsBundleContributor : IBundleContributor
  {
    public QuillJsBundleContributor()
    {

    }

    public Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
      context.Files.Add("/libs/quill/dist/quill.min.js");
      context.Files.Add("/libs/quill-image-resize-module/image-resize.min.js");
      context.Files.Add("/libs/quill-image-drop-module/image-drop.min.js");
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
