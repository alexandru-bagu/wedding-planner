using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.TableMenus
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
