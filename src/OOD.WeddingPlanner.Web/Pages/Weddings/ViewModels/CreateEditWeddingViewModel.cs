using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Weddings.ViewModels
{
    public class CreateEditWeddingViewModel
    {
        [Display(Name = "WeddingGroomName")]
        public string GroomName { get; set; }

        [Display(Name = "WeddingBrideName")]
        public string BrideName { get; set; }

        [Display(Name = "WeddingName")]
        public string Name { get; set; }

        [Display(Name = "WeddingContactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }
    }
}