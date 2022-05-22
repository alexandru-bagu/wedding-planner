using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using System.Text;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public InvitationWithNavigationPropertiesDto Invitation { get; set; }
        public IInvitationAppService AppService { get; }

        public IndexModel(IInvitationAppService appService)
        {
            AppService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            var invitations = await AppService.GetListWithNavigationAsync(new GetInvitationsInputDto() { MaxResultCount = 1 });
            if (invitations.TotalCount == 0)
            {
                Invitation = Bogus.InvitationWithNavigationProperties();
            }
            else
            {
                Invitation = await AppService.GetWithFullNavigationByIdAsync(invitations.Items[0].Invitation.Id);
            }
            await Task.CompletedTask;
        }
    }
}
