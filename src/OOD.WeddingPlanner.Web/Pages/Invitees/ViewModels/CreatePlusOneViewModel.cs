using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels
{
    public class CreatePlusOneViewModel
    {
        [Required]
        [Display(Name = "InviteeSurname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "InviteeName")]
        public string Name { get; set; }
    }
}