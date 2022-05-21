using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.LanguageTexts;
using OOD.WeddingPlanner.LanguageTexts.Dtos;
using OOD.WeddingPlanner.Web.Pages.LanguageTexts.ViewModels;
using Volo.Abp.Localization;

namespace OOD.WeddingPlanner.Web.Pages.LanguageTexts
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateLanguageTextViewModel ViewModel { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CultureName { get; set; }

        private readonly ILanguageTextAppService _service;
        private readonly ILanguageProvider _languageProvider;

        public CreateModalModel(ILanguageTextAppService service, ILanguageProvider languageProvider)
        {
            _service = service;
            _languageProvider = languageProvider;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateLanguageTextViewModel();
            ViewModel.CultureNameItem.AddRange((await _languageProvider.GetLanguagesAsync()).Select(p => new SelectListItem(p.DisplayName, p.CultureName, p.CultureName == CultureName)));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateLanguageTextViewModel, CreateUpdateLanguageTextDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}