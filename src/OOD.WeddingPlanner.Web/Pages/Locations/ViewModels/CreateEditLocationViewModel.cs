
using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.Locations.ViewModels
{
    public class CreateEditLocationViewModel
    {
        [Display(Name = "LocationName")]
        public string Name { get; set; }

        [Display(Name = "LocationDescription")]
        public string Description { get; set; }

        [Display(Name = "LocationLongitude")]
        public double Longitude { get; set; }

        [Display(Name = "LocationLatitude")]
        public double Latitude { get; set; }
    }
}