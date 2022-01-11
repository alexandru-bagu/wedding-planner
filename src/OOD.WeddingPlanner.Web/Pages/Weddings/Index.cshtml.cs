using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Weddings
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
