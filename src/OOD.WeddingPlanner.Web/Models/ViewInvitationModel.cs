using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Models
{
    public class ViewInvitationModel : InvitationWithNavigationPropertiesDto
    {
        public IStringLocalizer<WeddingPlannerResource> L { get; private set; }
        public CreatePlusOneViewModel PlusOne { get; set; }

        public ViewInvitationModel()
        {
        }

        public SelectListItem[] GetMenuItems(string selected, bool child)
        {
            SelectListItem[] ret;
            if (child)
            {
                ret = new[] {
                    new SelectListItem(L["MenuChild"].Value, "Child", selected == "Child"),
                    new SelectListItem(L["MenuChildFree"].Value, "ChildFree", selected == "ChildFree"),
                    new SelectListItem(L["MenuAdult"].Value, "Adult", selected == "Adult"),
                    new SelectListItem(L["MenuNone"].Value, "None", selected == "None")
                };
            }
            else
            {
                ret = new[] {
                    new SelectListItem(L["MenuAdult"].Value, "Adult", selected == "Adult")
                };
            }
            if (!ret.Any(p => p.Selected)) ret[0].Selected = true;
            return ret;
        }

        public Task PrepareAsync(IServiceProvider serviceProvider)
        {
            L = serviceProvider.GetService<IStringLocalizer<WeddingPlannerResource>>();
            PlusOne = new CreatePlusOneViewModel();
            PlusOne.GenderItems.Add(new SelectListItem(L["Male"], "true"));
            PlusOne.GenderItems.Add(new SelectListItem(L["Female"], "false"));
            return Task.CompletedTask;
        }
    }
}
