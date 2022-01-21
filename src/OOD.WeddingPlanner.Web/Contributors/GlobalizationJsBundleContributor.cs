using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace OOD.WeddingPlanner.Web.Contributors
{
    public class GlobalizationJsBundleContributor : IBundleContributor
    {
        public GlobalizationJsBundleContributor()
        {

        }

        public Task ConfigureBundleAsync(BundleConfigurationContext context)
        {
            var serviceProvider = context.ServiceProvider;
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            context.Files.Add("/libs/jquery-validation-globalize/jquery.validate.globalize.min.js");
            var cultureName = CultureInfo.CurrentCulture.Name;
            var globalizeInit = Path.Combine("libs", "jquery-validation-globalize", $"globalize-{cultureName}.js");
            var absoluteLoaderFilePath = Path.Combine(environment.WebRootPath, globalizeInit);
            if (!File.Exists(absoluteLoaderFilePath))
            {
                string locale = cultureName;
                if (!Directory.Exists(Path.Combine("libs", "cldr-data", "main", locale)))
                    locale = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                File.WriteAllText(absoluteLoaderFilePath, @$"
var paths = ['/libs/cldr-data/supplemental/likelySubtags.json',
  '/libs/cldr-data/main/{locale}/numbers.json',
  '/libs/cldr-data/supplemental/numberingSystems.json',
  '/libs/cldr-data/main/{locale}/ca-gregorian.json',
  '/libs/cldr-data/main/{locale}/timeZoneNames.json',
  '/libs/cldr-data/supplemental/timeData.json',
  '/libs/cldr-data/supplemental/weekData.json'];
$.when.apply(undefined, paths.map(function (path) {{
    return $.getJSON(path);
}})).then(function () {{
  return [].slice.apply(arguments, [0]).map(function (result) {{
    return result[0];
  }});
}}).then(Globalize.load).then(function(){{
    Globalize.locale('{locale}');
}});");
            }
            context.Files.Add('/' + globalizeInit.TrimStart('\\', '/').Replace('\\', '/'));
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
            context.Files.Add("/libs/cldrjs/dist/cldr.js");
            context.Files.Add("/libs/cldrjs/dist/cldr/event.js");
            context.Files.Add("/libs/cldrjs/dist/cldr/supplemental.js");

            context.Files.Add("/libs/globalize/dist/globalize.js");
            context.Files.Add("/libs/globalize/dist/globalize/number.js");
            context.Files.Add("/libs/globalize/dist/globalize/date.js");

            return Task.CompletedTask;
        }
    }
}
