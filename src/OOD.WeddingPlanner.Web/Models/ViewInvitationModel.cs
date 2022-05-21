using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.TableMenus;
using OOD.WeddingPlanner.TableMenus.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Web.Models
{
    public class ViewInvitationModel : InvitationWithNavigationPropertiesDto
    {
        public IStringLocalizer<WeddingPlannerResource> L { get; private set; }
        public TableMenuDto[] AdultMenus { get; private set; }
        public TableMenuDto[] ChildMenus { get; private set; }
        public CreatePlusOneViewModel PlusOne { get; set; }

        public ViewInvitationModel()
        {
        }

        public SelectListItem[] GetMenuItems(string selected, bool child)
        {
            SelectListItem[] ret;
            if (child)
                ret = ChildMenus.Select(p => new SelectListItem(L[p.Name], p.Name, p.Name == selected)).ToArray();
            else
                ret = AdultMenus.Select(p => new SelectListItem(L[p.Name], p.Name, p.Name == selected)).ToArray();
            if (!ret.Any(p => p.Selected)) ret[0].Selected = true;
            return ret;
        }

        public async Task PrepareAsync(IServiceProvider serviceProvider)
        {
            L = serviceProvider.GetService<IStringLocalizer<WeddingPlannerResource>>();
            var menuAppService = serviceProvider.GetService<ITableMenuAppService>();
            var menus = await menuAppService.GetListAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount });
            AdultMenus = menus.Items.Where(p => p.Adult).OrderBy(p => p.Order).ToArray();
            ChildMenus = menus.Items.Where(p => !p.Adult).OrderBy(p => p.Order).ToArray();
            PlusOne = new CreatePlusOneViewModel();
            PlusOne.GenderItems.Add(new SelectListItem(L["Male"], "true"));
            PlusOne.GenderItems.Add(new SelectListItem(L["Female"], "false"));
        }
    }
}
