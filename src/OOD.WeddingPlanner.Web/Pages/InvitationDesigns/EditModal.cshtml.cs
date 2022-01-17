using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditInvitationDesignViewModel ViewModel { get; set; }

        private readonly IInvitationDesignAppService _service;
        private readonly IOptions<AbpLocalizationOptions> _localizationOptions;

        public EditModalModel(IInvitationDesignAppService service, IOptions<AbpLocalizationOptions> localizationOptions)
        {
            _service = service;
            _localizationOptions = localizationOptions;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<InvitationDesignDto, CreateEditInvitationDesignViewModel>(dto);

            ViewModel.MeasurementUnits.AddRange(new[] {
                new SelectListItem("Millimeter", "mm"),
                new SelectListItem("Centimeter", "cm"),
                new SelectListItem("Inch", "in"),
            });

            ViewModel.CultureList.AddRange(_localizationOptions.Value.Languages.Select(p => new SelectListItem() { Value = p.CultureName, Text = p.DisplayName, Selected = dto.DefaultCulture == p.CultureName }));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}