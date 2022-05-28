using IdentityServer4;
using Lsw.Abp.AspNetCore.Mvc.UI.Theme.Stisla;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OOD.WeddingPlanner.EntityFrameworkCore;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.MultiTenancy;
using OOD.WeddingPlanner.Web.Contributors;
using OOD.WeddingPlanner.Web.Download;
using OOD.WeddingPlanner.Web.Menus;
using OOD.WeddingPlanner.Web.Providers;
using OOD.WeddingPlanner.Web.Services;
using System;
using System.IO;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;
using EasyAbp.Abp.SettingUi.Web;

namespace OOD.WeddingPlanner.Web
{
    [DependsOn(
        typeof(WeddingPlannerHttpApiModule),
        typeof(WeddingPlannerApplicationModule),
        typeof(WeddingPlannerEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpSettingManagementWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreMvcUiStislaThemeModule)
        )]
    [DependsOn(typeof(AbpSettingUiWebModule))]
    public class WeddingPlannerWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                          typeof(WeddingPlannerResource),
                          typeof(WeddingPlannerDomainModule).Assembly,
                          typeof(WeddingPlannerDomainSharedModule).Assembly,
                          typeof(WeddingPlannerApplicationModule).Assembly,
                          typeof(WeddingPlannerApplicationContractsModule).Assembly,
                          typeof(WeddingPlannerWebModule).Assembly
                      );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);

            context.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            context.Services.AddHostedService<InvitationDownloadBuilderService>();
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(StandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                    bundle.AddContributors(new QuillCssBundleContributor());
                    bundle.AddContributors(new IoniconsCssBundleContributor());
                });
                options.ScriptBundles.Configure(StandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(new QuillJsBundleContributor());
                    bundle.AddContributors(new KnockoutJsBundleContributor());
                    bundle.AddContributors(new MonacoEditorJsBundleContributor());
                    bundle.AddContributors(new QrCodeJsBundleContributor());
                    bundle.AddContributors(new GlobalJsBundleContributor());
                    bundle.AddContributors(new GlobalizationJsBundleContributor());
                });
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "WeddingPlanner";
            });
            Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });
            Configure<CookieAuthenticationOptions>(IdentityServerConstants.DefaultCookieAuthenticationScheme, options =>
            {
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeddingPlannerWebModule>();
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<WeddingPlannerDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OOD.WeddingPlanner.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeddingPlannerDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OOD.WeddingPlanner.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeddingPlannerApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OOD.WeddingPlanner.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeddingPlannerApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OOD.WeddingPlanner.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeddingPlannerWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
                //options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
                //options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                //options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                //options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                //options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                //options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
                //options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                //options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
                //options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
                //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                //options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                //options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
                //options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                //options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                //options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
                //options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new WeddingPlannerMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(WeddingPlannerApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WeddingPlanner API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            ConfigureConverter(app);

            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();

            app.UseAbpRequestLocalization((options) =>
            {
                options.RequestCultureProviders.Clear();
                options.DefaultRequestCulture = new RequestCulture("en");
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
                options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
                options.RequestCultureProviders.Add(new UserPreferenceDefaultRequestCultureProvider());
                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
            });

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always,
            });
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WeddingPlanner API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureConverter(IApplicationBuilder app)
        {
            var converter = app.ApplicationServices.GetService<IConverter>();
            var logger = app.ApplicationServices.GetService<ILogger<IConverter>>();
            converter.Warning += (sender, e) => logger.LogWarning(e.Message);
            converter.Error += (sender, e) => logger.LogError(e.Message);
            converter.PhaseChanged += (sender, e) => logger.LogInformation($"Converter phase: [{e.CurrentPhase}/{e.PhaseCount}] {e.Description}");
            converter.ProgressChanged += (sender, e) => logger.LogInformation($"Converter progress: {e.Description}");
            converter.Finished += (sender, e) => logger.LogInformation($"Converter finished. Success: {e.Success}");
        }
    }
}
