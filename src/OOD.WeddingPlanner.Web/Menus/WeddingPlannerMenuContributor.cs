using Microsoft.Extensions.DependencyInjection;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.MultiTenancy;
using OOD.WeddingPlanner.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace OOD.WeddingPlanner.Web.Menus
{
    public class WeddingPlannerMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var tenant = context.ServiceProvider.GetService<ICurrentTenant>();
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<WeddingPlannerResource>();

            context.Menu.TryRemoveMenuItem("Stisla.Dashboard");

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    WeddingPlannerMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            var multiTenancy = MultiTenancyConsts.IsEnabled && true;
            if (multiTenancy)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Wedding.Default))
            {
                if (tenant.Id != null)
                {
                    context.Menu.AddItem(
                        new ApplicationMenuItem(WeddingPlannerMenus.MyWedding, l["Menu:MyWedding"], "/MyWedding", icon: "fas fa-church")
                    );
                }
                else
                {
                    context.Menu.AddItem(
                        new ApplicationMenuItem(WeddingPlannerMenus.Wedding, l["Menu:Wedding"], "/Weddings", icon: "fas fa-church")
                    );
                }
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Location.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Location, l["Menu:Location"], "/Locations", icon: "fas fa-location-arrow")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.InvitationDesign.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.InvitationDesign, l["Menu:InvitationDesign"], "/InvitationDesigns", icon: "fas fa-object-group")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Event.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Event, l["Menu:Event"], "/Events", icon: "fas fa-calendar")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitee.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Invitee, l["Menu:Invitee"], "/Invitees", icon: "fas fa-user")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.TableMenu.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.TableMenu, l["Menu:TableMenu"], "/TableMenus", icon: "fas fa-utensils")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitation.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Invitation, l["Menu:Invitation"], "/Invitations", icon: "fas fa-envelope")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Table.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Table, l["Menu:Table"], "/Tables", icon: "fas fa-utensils")
                );
            }
        }
    }
}
