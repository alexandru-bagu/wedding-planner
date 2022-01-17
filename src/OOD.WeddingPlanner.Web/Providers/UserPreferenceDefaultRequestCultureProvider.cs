using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Web.Providers
{
    public class UserPreferenceDefaultRequestCultureProvider : RequestCultureProvider, ISingletonDependency
    {
        public const string UserLanguagePreference = "_usr_lang";

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            httpContext.Items.Add(UserLanguagePreference, true);
            return Task.FromResult((ProviderCultureResult)null);
        }
    }
}
