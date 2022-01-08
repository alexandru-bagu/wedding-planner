using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Weddings;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationWithNavigationProperties
    {
        public Invitation Invitation { get; set; }

        public Wedding Wedding { get; set; }

        public InvitationDesign Design { get; set; }
    }
}
