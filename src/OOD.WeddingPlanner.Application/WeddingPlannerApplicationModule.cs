using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using EasyAbp.Abp.SettingUi;
using System.Threading.Tasks;
using Volo.Abp;
using OOD.WeddingPlanner.Invitations;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using Volo.Abp.Localization;
using OOD.WeddingPlanner.LanguageTexts;
using OOD.WeddingPlanner.Localization;

namespace OOD.WeddingPlanner
{
    [DependsOn(
        typeof(WeddingPlannerDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(WeddingPlannerApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    [DependsOn(typeof(AbpSettingUiApplicationModule))]
    public class WeddingPlannerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeddingPlannerApplicationModule>();
            });


            Configure<AbpLocalizationOptions>(options =>
            {
                var resource = options.Resources.Get<WeddingPlannerResource>();
                resource.Contributors.Add(new DbLocalizationResourceContributor());
            });
        }
    }
}
