using OOD.WeddingPlanner.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OOD.WeddingPlanner.Permissions
{
    public class WeddingPlannerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(WeddingPlannerPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(WeddingPlannerPermissions.MyPermission1, L("Permission:MyPermission1"));

            var locationPermission = myGroup.AddPermission(WeddingPlannerPermissions.Location.Default, L("Permission:Location"));
            locationPermission.AddChild(WeddingPlannerPermissions.Location.Create, L("Permission:Create"));
            locationPermission.AddChild(WeddingPlannerPermissions.Location.Update, L("Permission:Update"));
            locationPermission.AddChild(WeddingPlannerPermissions.Location.Delete, L("Permission:Delete"));

            var eventPermission = myGroup.AddPermission(WeddingPlannerPermissions.Event.Default, L("Permission:Event"));
            eventPermission.AddChild(WeddingPlannerPermissions.Event.Create, L("Permission:Create"));
            eventPermission.AddChild(WeddingPlannerPermissions.Event.Update, L("Permission:Update"));
            eventPermission.AddChild(WeddingPlannerPermissions.Event.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WeddingPlannerResource>(name);
        }
    }
}
