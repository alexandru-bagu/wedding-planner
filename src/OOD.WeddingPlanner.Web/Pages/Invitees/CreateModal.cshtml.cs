using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Invitees
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInviteeViewModel ViewModel { get; set; }

        private readonly IInviteeAppService _service;
        private readonly IWeddingAppService _weddingAppService;

        public CreateModalModel(IInviteeAppService service, IWeddingAppService weddingAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditInviteeViewModel();
            ViewModel.BooleanItems.AddRange(new[] {
        new SelectListItem("", ""),
        new SelectListItem("No", "False"),
        new SelectListItem("Yes", "True"),
      });
            ViewModel.InvitationItems.AddRange(new[] {
        new SelectListItem("", "")
      });
            ViewModel.WeddingItems.AddRange(new[] {
        new SelectListItem("", "")
      });
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items.Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInviteeViewModel, CreateUpdateInviteeDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}