using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
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
            ViewModel = new CreateEditInvitationDesignViewModel()
            {
                MeasurementUnit = "cm",
                PaperWidth = 21,
                PaperHeight = 29.7,
                PaperDpi = 96
            };

            var serviceProvider = HttpContext.RequestServices;
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var defaultFilePath = Path.Combine("html", "default.html");
            var absoluteDefaultFilePath = Path.Combine(environment.WebRootPath, defaultFilePath);
            if (System.IO.File.Exists(absoluteDefaultFilePath))
            {
                ViewModel.Body = await System.IO.File.ReadAllTextAsync(absoluteDefaultFilePath);
            }

            ViewModel.MeasurementUnits.AddRange(new[] {
                new SelectListItem("Millimeter", "mm"),
                new SelectListItem("Centimeter", "cm"),
                new SelectListItem("Inch", "in"),
            });
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}