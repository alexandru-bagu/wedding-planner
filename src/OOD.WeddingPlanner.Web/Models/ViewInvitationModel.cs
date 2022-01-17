using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Localization;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using System;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Models
{
    public class ViewInvitationModel : InvitationWithNavigationPropertiesDto
    {
        public CreatePlusOneViewModel PlusOne { get; set; }

        public ViewInvitationModel()
        {

        }
        public Task PrepareAsync(IServiceProvider serviceProvider)
        {
            PlusOne = new CreatePlusOneViewModel();
            return Task.CompletedTask;
        }
    }
}
