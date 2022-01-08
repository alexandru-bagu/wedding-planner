using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels
{
    public class CreateEditEventViewModel
    {
        [Required]
        [Display(Name = "EventLocationId")]
        [SelectItems(nameof(LocationItems))]
        public Guid? LocationId { get; set; }

        [Required]
        [Display(Name = "EventWeddingId")]
        [SelectItems(nameof(WeddingItems))]
        public Guid? WeddingId { get; set; }

        [Required]
        [Display(Name = "EventName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "EventTime")]
        public DateTime Time { get; set; }

        public List<SelectListItem> LocationItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> WeddingItems { get; set; } = new List<SelectListItem>();
    }
}