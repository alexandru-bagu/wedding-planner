
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.InvitationDesigns.InvitationDesign.ViewModels
{
    public class CreateEditInvitationDesignViewModel
    {
        [Display(Name = "InvitationDesignName")]
        public string Name { get; set; }

        [Display(Name = "InvitationDesignBody")]
        [TextArea]
        public string Body { get; set; }
    }
}