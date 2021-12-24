using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Locations.Location.ViewModels;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Web.Pages.Invitees.Invitee.ViewModels;
using AutoMapper;

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
        }
    }
}
