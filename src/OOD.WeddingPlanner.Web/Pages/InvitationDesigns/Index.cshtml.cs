using OOD.WeddingPlanner.Invitations;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public InvitationWithNavigationProperties BogusInvitation { get; set; }

        public virtual async Task OnGetAsync()
        {
            BogusInvitation = Bogus.InvitationWithNavigationProperties();
            await Task.CompletedTask;
        }
    }
}
