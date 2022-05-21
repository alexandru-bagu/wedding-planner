using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace OOD.WeddingPlanner.Web.Pages.LanguageTexts
{
    public class IndexModel : WeddingPlannerPageModel
    {
        public string CultureFilter { get; set; }
        public List<SelectListItem> CultureList { get; set; }
        public ILanguageProvider LanguageProvider { get; }

        public IndexModel(ILanguageProvider languageProvider)
        {
            LanguageProvider = languageProvider;
        }

        public virtual async Task OnGetAsync()
        {
            CultureList = new List<SelectListItem>();
            CultureList.AddRange((await LanguageProvider.GetLanguagesAsync()).Select(p => new SelectListItem(p.DisplayName, p.CultureName)));
            await Task.CompletedTask;
        }
    }
}
