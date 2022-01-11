using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Tables.ViewModels
{
    public class CreateEditTableViewModel
    {
        [Required]
        [Display(Name = "TableWeddingId")]
        [SelectItems(nameof(WeddingItems))]
        public Guid? WeddingId { get; set; }

        [Required]
        [Display(Name = "TableEventId")]
        [SelectItems(nameof(EventItems))]
        public Guid? EventId { get; set; }

        [Required]
        [Display(Name = "TableName")]
        public string Name { get; set; }

        [Display(Name = "TableDescription")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "TableSize")]
        public int Size { get; set; } = 1;

        [Required]
        [Display(Name = "TableRow")]
        public int Row { get; set; } = 1;

        [Required]
        [Display(Name = "TableColumn")]
        public int Column { get; set; } = 1;

        public List<SelectListItem> WeddingItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EventItems { get; set; } = new List<SelectListItem>();
    }
}