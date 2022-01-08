using OOD.WeddingPlanner.Invitations;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public Invitation BogusInvitation { get; set; }

        public virtual async Task OnGetAsync()
        {
            BogusInvitation = Bogus.Invitation();
            await Task.CompletedTask;
        }
    }
}
