using AutoMapper;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Web.Models;
using OOD.WeddingPlanner.Web.Pages.Events.ViewModels;
using OOD.WeddingPlanner.Web.Pages.InvitationDesigns.ViewModels;
using OOD.WeddingPlanner.Web.Pages.Invitations.ViewModels;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using OOD.WeddingPlanner.Web.Pages.Locations.ViewModels;
using OOD.WeddingPlanner.Web.Pages.Tables.ViewModels;
using OOD.WeddingPlanner.Web.Pages.Weddings.ViewModels;
using OOD.WeddingPlanner.TableInvitees.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Web
{
    public class WeddingPlannerWebAutoMapperProfile : Profile
    {
        public WeddingPlannerWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<LocationDto, CreateEditLocationViewModel>();
            CreateMap<CreateEditLocationViewModel, CreateUpdateLocationDto>();
            CreateMap<EventDto, CreateEditEventViewModel>();
            CreateMap<CreateEditEventViewModel, CreateUpdateEventDto>();
            CreateMap<InviteeDto, CreateEditInviteeViewModel>();
            CreateMap<CreateEditInviteeViewModel, CreateUpdateInviteeDto>();
            CreateMap<InvitationDto, CreateEditInvitationViewModel>();
            CreateMap<CreateEditInvitationViewModel, CreateUpdateInvitationDto>();
            CreateMap<WeddingDto, CreateEditWeddingViewModel>();
            CreateMap<CreateEditWeddingViewModel, CreateUpdateWeddingDto>();
            CreateMap<InvitationDesignDto, CreateEditInvitationDesignViewModel>();
            CreateMap<CreateEditInvitationDesignViewModel, CreateUpdateInvitationDesignDto>();
            CreateMap<TableDto, CreateEditTableViewModel>();
            CreateMap<CreateEditTableViewModel, CreateUpdateTableDto>();
            CreateMap<InvitationWithNavigationPropertiesDto, ViewInvitationModel>();
        }
    }
}
