using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels
{
    public class CreatePlusOneViewModel
    {
        [Required]
        [Display(Name = "InviteeSurname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "InviteeName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "InviteeMale")]
        [SelectItems(nameof(GenderItems))]
        public bool? Male { get; set; }
        public List<SelectListItem> GenderItems { get; set; } = new List<SelectListItem>();
    }
}