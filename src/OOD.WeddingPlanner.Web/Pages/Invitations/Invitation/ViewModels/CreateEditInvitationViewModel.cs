using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.Invitation.ViewModels
{
    public class CreateEditInvitationViewModel
    {
        [Required]
        [Display(Name = "InvitationWeddingId")]
        [SelectItems(nameof(WeddingItems))]
        public Guid? WeddingId { get; set; }

        [Required]
        [Display(Name = "InvitationDesignId")]
        [SelectItems(nameof(DesignItems))]
        public Guid? DesignId { get; set; }

        [Required]
        [Display(Name = "InvitationDestination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Invitee")]
        [SelectItems(nameof(InviteeItems))]
        public List<Guid> InviteeIds { get; set; }

        public List<SelectListItem> WeddingItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> InviteeItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DesignItems { get; set; } = new List<SelectListItem>();
    }
}