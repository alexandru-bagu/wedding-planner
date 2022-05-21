using System;
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
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Value { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CultureName { get; set; }

        [BindProperty]
        public EditLanguageTextViewModel ViewModel { get; set; }

        private readonly ILanguageTextAppService _service;
        private readonly ILanguageProvider _languageProvider;

        public EditModalModel(ILanguageTextAppService service, ILanguageProvider languageProvider)
        {
            _service = service;
            _languageProvider = languageProvider;
        }

        public virtual async Task OnGetAsync()
        {
            if (Id != Guid.Empty)
            {
                var dto = await _service.GetAsync(Id);
                ViewModel = ObjectMapper.Map<LanguageTextDto, EditLanguageTextViewModel>(dto);
            }
            else
            {
                ViewModel = new EditLanguageTextViewModel() { Name = Name, Value = Value, CultureName = CultureName };
            }
            ViewModel.CultureNameItem.AddRange((await _languageProvider.GetLanguagesAsync()).Select(p => new SelectListItem(p.DisplayName, p.CultureName, p.CultureName == CultureName)));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            if (Id != Guid.Empty)
            {
                var dto = ObjectMapper.Map<EditLanguageTextViewModel, CreateUpdateLanguageTextDto>(ViewModel);
                await _service.UpdateAsync(Id, dto);
            }
            else
            {
                var dto = ObjectMapper.Map<EditLanguageTextViewModel, CreateUpdateLanguageTextDto>(ViewModel);
                await _service.CreateAsync(dto);
            }
            return NoContent();
        }
    }
}