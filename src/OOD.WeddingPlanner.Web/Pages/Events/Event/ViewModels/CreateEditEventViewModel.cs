using System;

using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels
{
    public class CreateEditEventViewModel
    {
        [Display(Name = "EventLocationId")]
        public Guid? LocationId { get; set; }

        [Display(Name = "EventWeddingId")]
        public Guid? WeddingId { get; set; }

        [Display(Name = "EventName")]
        public string Name { get; set; }

        [Display(Name = "EventTime")]
        public DateTime Time { get; set; }
    }
}