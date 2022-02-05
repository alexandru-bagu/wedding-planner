using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels
{
    public class CreateEditInviteeViewModel
    {
        [Required]
        [Display(Name = "InviteeSurname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "InviteeName")]
        public string Name { get; set; }

        [HiddenInput]
        [Required]
        [Display(Name = "InviteeChild")]
        [SelectItems(nameof(BooleanItems))]
        public bool? Child { get; set; }

        [HiddenInput]
        [Required]
        [Display(Name = "InviteeMale")]
        [SelectItems(nameof(BooleanItems))]
        public bool? Male { get; set; }

        [Display(Name = "InviteeMenu")]
        [SelectItems(nameof(MenuTypes))]
        public string Menu { get; set; }

        [Required]
        [Display(Name = "InviteeOrder")]
        public int Order { get; set; }

        [Display(Name = "Wedding")]
        [SelectItems(nameof(WeddingItems))]
        public Guid? WeddingId { get; set; }

        [Display(Name = "Invitation")]
        [SelectItems(nameof(InvitationItems))]
        public Guid? InvitationId { get; set; }
        
        [Display(Name = "InviteeRSVP")]
        public DateTime? RSVP { get; set; }

        [Display(Name = "InviteeConfirmed")]
        [SelectItems(nameof(BooleanItems))]
        public bool? Confirmed { get; set; }
        
        [Required]
        [Display(Name = "InviteePlusOne")]
        [SelectItems(nameof(BooleanItems))]
        public bool? PlusOne { get; set; }

        public List<SelectListItem> BooleanItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> InvitationItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> WeddingItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MenuTypes { get; set; } = new List<SelectListItem>();
    }
}