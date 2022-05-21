using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OOD.WeddingPlanner.Web.Pages.TableMenus.ViewModels
{
    public class CreateEditTableMenuViewModel
    {
        [Display(Name = "TableMenuName")]
        public string Name { get; set; }

        [Display(Name = "TableMenuAdult")]
        public bool Adult { get; set; }

        [Display(Name = "TableMenuOrder")]
        [DefaultValue(0)]
        public int Order { get; set; } = 0;
    }
}