using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
