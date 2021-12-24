using System.Threading.Tasks;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
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
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<WeddingPlannerResource>();

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
            
            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Location.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Location, l["Menu:Location"], "/Locations/Location")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Event.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Event, l["Menu:Event"], "/Events/Event")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitee.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Invitee, l["Menu:Invitee"], "/Invitees/Invitee")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitation.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Invitation, l["Menu:Invitation"], "/Invitations/Invitation")
                );
            }
            if (await context.IsGrantedAsync(WeddingPlannerPermissions.Wedding.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(WeddingPlannerMenus.Wedding, l["Menu:Wedding"], "/Weddings/Wedding")
                );
            }
        }
    }
}
