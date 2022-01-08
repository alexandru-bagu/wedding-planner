namespace OOD.WeddingPlanner.Permissions
{
    public static class WeddingPlannerPermissions
    {
        public const string GroupName = "WeddingPlanner";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public class Location
        {
            public const string Default = GroupName + ".Location";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Event
        {
            public const string Default = GroupName + ".Event";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Invitee
        {
            public const string Default = GroupName + ".Invitee";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Invitation
        {
            public const string Default = GroupName + ".Invitation";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Wedding
        {
            public const string Default = GroupName + ".Wedding";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class InvitationDesign
        {
            public const string Default = GroupName + ".InvitationDesign";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Table
        {
            public const string Default = GroupName + ".Table";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
