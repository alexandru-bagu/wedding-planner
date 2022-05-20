using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public InvitationWithNavigationPropertiesDto Invitation { get; set; }
        public IInvitationAppService AppService { get; }

        public IndexModel(IInvitationAppService appService) {
            AppService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            var invitations = await AppService.GetListWithNavigationAsync(new GetInvitationsInputDto(){ MaxResultCount = 1 });
            if(invitations.TotalCount == 0) {
                Invitation = Bogus.InvitationWithNavigationProperties();
            } else {
                Invitation = invitations.Items[0];
            }
            await Task.CompletedTask;
        }
    }
}
