using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.LanguageTexts.ViewModels
{
    public class CreateLanguageTextViewModel
    {
        [Display(Name = "LanguageTextCultureName")]
        [SelectItems(nameof(CultureNameItem))]
        public string CultureName { get; set; }

        [Display(Name = "LanguageTextName")]
        public string Name { get; set; }

        [Display(Name = "LanguageTextValue")]
        public string Value { get; set; }
        public List<SelectListItem> CultureNameItem { get; set; } = new List<SelectListItem>();
    }
}