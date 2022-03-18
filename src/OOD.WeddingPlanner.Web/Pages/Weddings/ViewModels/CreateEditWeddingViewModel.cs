using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace OOD.WeddingPlanner.Web.Pages.Weddings.ViewModels
{
    public class CreateEditWeddingViewModel
    {
        [Display(Name = "WeddingName")]
        public string Name { get; set; }

        [Display(Name = "WeddingGroomName")]
        public string GroomName { get; set; }

        [Display(Name = "WeddingBrideName")]
        public string BrideName { get; set; }

        [Display(Name = "WeddingContactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [Display(Name = "WeddingInvitationNote")]
        [TextArea]
        public string InvitationNote { get; set; }
    }
}