using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Weddings;

namespace OOD.WeddingPlanner.Invitees
{
    public class InviteeWithNavigationProperties
    {
        public Invitee Invitee { get; set; }
        public Invitation Invitation { get; set; }
        public Wedding Wedding { get; set; }
        public Table Table { get; set; }
    }
}
