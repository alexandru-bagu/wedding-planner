using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace OOD.WeddingPlanner.Web.Pages.Invitations
{
    public class ViewModel : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public IInvitationRepository Repository { get; }
        public IObjectMapper ObjectMapper { get; }
        public InvitationWithNavigationPropertiesDto InvitationData { get; set; }

        public ViewModel(IInvitationRepository repository, IObjectMapper objectMapper)
        {
            Repository = repository;
            ObjectMapper = objectMapper;
        }

        public async Task OnGetAsync()
        {
            InvitationData = ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(await Repository.GetWithNavigationByIdAsync(Id));
        }
    }
}
