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
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.Location.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.Location, l["Menu:Location"], "/Locations/Location", icon: "fas fa-location-arrow")
        );
      }
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.Event.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.Event, l["Menu:Event"], "/Events/Event", icon: "fas fa-calendar")
        );
      }
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitee.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.Invitee, l["Menu:Invitee"], "/Invitees/Invitee", icon: "fas fa-user")
        );
      }
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.Invitation.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.Invitation, l["Menu:Invitation"], "/Invitations/Invitation", icon: "fas fa-envelope")
        );
      }
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.Wedding.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.Wedding, l["Menu:Wedding"], "/Weddings/Wedding", icon: "fas fa-church")
        );
      }
      if (await context.IsGrantedAsync(WeddingPlannerPermissions.InvitationDesign.Default))
      {
        context.Menu.AddItem(
            new ApplicationMenuItem(WeddingPlannerMenus.InvitationDesign, l["Menu:InvitationDesign"], "/InvitationDesigns/InvitationDesign", icon: "fas fa-object-group")
        );
      }
    }
  }
}
