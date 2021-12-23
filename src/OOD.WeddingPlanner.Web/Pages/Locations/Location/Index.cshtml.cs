using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Locations.Location
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
