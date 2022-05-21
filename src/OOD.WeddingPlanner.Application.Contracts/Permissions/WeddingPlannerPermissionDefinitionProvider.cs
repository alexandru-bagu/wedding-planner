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

            var inviteePermission = myGroup.AddPermission(WeddingPlannerPermissions.Invitee.Default, L("Permission:Invitee"));
            inviteePermission.AddChild(WeddingPlannerPermissions.Invitee.Create, L("Permission:Create"));
            inviteePermission.AddChild(WeddingPlannerPermissions.Invitee.Update, L("Permission:Update"));
            inviteePermission.AddChild(WeddingPlannerPermissions.Invitee.Delete, L("Permission:Delete"));

            var invitationPermission = myGroup.AddPermission(WeddingPlannerPermissions.Invitation.Default, L("Permission:Invitation"));
            invitationPermission.AddChild(WeddingPlannerPermissions.Invitation.Create, L("Permission:Create"));
            invitationPermission.AddChild(WeddingPlannerPermissions.Invitation.Update, L("Permission:Update"));
            invitationPermission.AddChild(WeddingPlannerPermissions.Invitation.Delete, L("Permission:Delete"));

            var weddingPermission = myGroup.AddPermission(WeddingPlannerPermissions.Wedding.Default, L("Permission:Wedding"));
            weddingPermission.AddChild(WeddingPlannerPermissions.Wedding.Create, L("Permission:Create"));
            weddingPermission.AddChild(WeddingPlannerPermissions.Wedding.Update, L("Permission:Update"));
            weddingPermission.AddChild(WeddingPlannerPermissions.Wedding.Delete, L("Permission:Delete"));

            var invitationDesignPermission = myGroup.AddPermission(WeddingPlannerPermissions.InvitationDesign.Default, L("Permission:InvitationDesign"));
            invitationDesignPermission.AddChild(WeddingPlannerPermissions.InvitationDesign.Create, L("Permission:Create"));
            invitationDesignPermission.AddChild(WeddingPlannerPermissions.InvitationDesign.Update, L("Permission:Update"));
            invitationDesignPermission.AddChild(WeddingPlannerPermissions.InvitationDesign.Delete, L("Permission:Delete"));

            var tablePermission = myGroup.AddPermission(WeddingPlannerPermissions.Table.Default, L("Permission:Table"));
            tablePermission.AddChild(WeddingPlannerPermissions.Table.Create, L("Permission:Create"));
            tablePermission.AddChild(WeddingPlannerPermissions.Table.Update, L("Permission:Update"));
            tablePermission.AddChild(WeddingPlannerPermissions.Table.Delete, L("Permission:Delete"));

            var tableInviteePermission = myGroup.AddPermission(WeddingPlannerPermissions.TableInvitee.Default, L("Permission:TableInvitee"));
            tableInviteePermission.AddChild(WeddingPlannerPermissions.TableInvitee.Create, L("Permission:Create"));
            tableInviteePermission.AddChild(WeddingPlannerPermissions.TableInvitee.Update, L("Permission:Update"));
            tableInviteePermission.AddChild(WeddingPlannerPermissions.TableInvitee.Delete, L("Permission:Delete"));

            var tableMenuPermission = myGroup.AddPermission(WeddingPlannerPermissions.TableMenu.Default, L("Permission:TableMenu"));
            tableMenuPermission.AddChild(WeddingPlannerPermissions.TableMenu.Create, L("Permission:Create"));
            tableMenuPermission.AddChild(WeddingPlannerPermissions.TableMenu.Update, L("Permission:Update"));
            tableMenuPermission.AddChild(WeddingPlannerPermissions.TableMenu.Delete, L("Permission:Delete"));

            var languageTextPermission = myGroup.AddPermission(WeddingPlannerPermissions.LanguageText.Default, L("Permission:LanguageText"));
            languageTextPermission.AddChild(WeddingPlannerPermissions.LanguageText.Create, L("Permission:Create"));
            languageTextPermission.AddChild(WeddingPlannerPermissions.LanguageText.Update, L("Permission:Update"));
            languageTextPermission.AddChild(WeddingPlannerPermissions.LanguageText.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WeddingPlannerResource>(name);
        }
    }
}
