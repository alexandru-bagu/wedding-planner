using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitations.ViewModels
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

        [Required]
        [Display(Name = "InvitationPlusOne")]
        [SelectItems(nameof(BooleanItems))]
        public bool PlusOne { get; set; }

        [Required]
        [Display(Name = "InvitationUniqueCode")]
        [HiddenInput]
        [ReadOnlyInput]
        public string UniqueCode { get; set; }

        public List<SelectListItem> WeddingItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> InviteeItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DesignItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BooleanItems { get; set; } = new List<SelectListItem>();
    }
}