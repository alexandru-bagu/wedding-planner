using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditInvitationDesignViewModel ViewModel { get; set; }

        private readonly IInvitationDesignAppService _service;

        public CreateModalModel(IInvitationDesignAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditInvitationDesignViewModel();

            var serviceProvider = HttpContext.RequestServices;
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var defaultFilePath = Path.Combine("html", "default.html");
            var absoluteDefaultFilePath = Path.Combine(environment.WebRootPath, defaultFilePath);
            if (System.IO.File.Exists(absoluteDefaultFilePath))
            {
                ViewModel.Body = await System.IO.File.ReadAllTextAsync(absoluteDefaultFilePath);
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}