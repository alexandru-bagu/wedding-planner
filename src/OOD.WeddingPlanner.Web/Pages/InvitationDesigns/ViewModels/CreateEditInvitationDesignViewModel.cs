using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns.ViewModels
{
    public class CreateEditInvitationDesignViewModel
    {
        [Display(Name = "InvitationDesignName")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "InvitationDesignDefaultCulture")]
        [SelectItems(nameof(CultureList))]
        [Required]
        public string DefaultCulture { get; set; }

        [Required]
        [SelectItems(nameof(MeasurementUnits))]
        [Display(Name = "InvitationDesignMeasurementUnit")]
        public string MeasurementUnit { get; set; }

        [Required]
        [Display(Name = "InvitationDesignPaperWidth")]
        public double? PaperWidth { get; set; }

        [Required]
        [Display(Name = "InvitationDesignPaperHeight")]
        public double? PaperHeight { get; set; }

        [Required]
        [Display(Name = "InvitationDesignPaperDpi")]
        public double? PaperDpi { get; set; }

        [Display(Name = "InvitationDesignBody")]
        [TextArea]
        [Required]
        [DynamicFormIgnore]
        public string Body { get; set; }

        public List<SelectListItem> MeasurementUnits { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CultureList { get; set; } = new List<SelectListItem>();
    }
}