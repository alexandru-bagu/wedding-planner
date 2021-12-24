using OOD.WeddingPlanner.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace OOD.WeddingPlanner
{
  [DependsOn(
      typeof(AbpAuditLoggingDomainSharedModule),
      typeof(AbpBackgroundJobsDomainSharedModule),
      typeof(AbpFeatureManagementDomainSharedModule),
      typeof(AbpIdentityDomainSharedModule),
      typeof(AbpIdentityServerDomainSharedModule),
      typeof(AbpPermissionManagementDomainSharedModule),
      typeof(AbpSettingManagementDomainSharedModule),
      typeof(AbpTenantManagementDomainSharedModule)
      )]
  public class WeddingPlannerDomainSharedModule : AbpModule
  {
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
      WeddingPlannerGlobalFeatureConfigurator.Configure();
      WeddingPlannerModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
      Configure<AbpVirtualFileSystemOptions>(options =>
      {
        options.FileSets.AddEmbedded<WeddingPlannerDomainSharedModule>();
      });

      Configure<AbpLocalizationOptions>(options =>
      {
        options.Resources
                  .Add<WeddingPlannerResource>("en")
                  .AddBaseTypes(typeof(AbpValidationResource))
                  .AddVirtualJson("/Localization/WeddingPlanner");

        options.DefaultResourceType = typeof(WeddingPlannerResource);
      });

      Configure<AbpExceptionLocalizationOptions>(options =>
      {
        options.MapCodeNamespace("WeddingPlanner", typeof(WeddingPlannerResource));
      });
    }
  }
}
