using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using AutoMapper;

namespace OOD.WeddingPlanner
{
    public class WeddingPlannerApplicationAutoMapperProfile : Profile
    {
        public WeddingPlannerApplicationAutoMapperProfile()
        {
            CreateMap<Location, LocationDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Invitee, InviteeDto>();
            CreateMap<Invitation, InvitationDto>();
            CreateMap<Wedding, WeddingDto>();
            
            CreateMap<CreateUpdateLocationDto, Location>(MemberList.Source);
            CreateMap<CreateUpdateEventDto, Event>(MemberList.Source);
            CreateMap<CreateUpdateInviteeDto, Invitee>(MemberList.Source);
            CreateMap<CreateUpdateInvitationDto, Invitation>(MemberList.Source);
            CreateMap<CreateUpdateWeddingDto, Wedding>(MemberList.Source);

            CreateMap<EventWithNavigationProperties, EventWithNavigationPropertiesDto>();
            CreateMap<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>();
            CreateMap<InviteeWithNavigationProperties, InviteeWithNavigationPropertiesDto>();
            CreateMap<LocationWithNavigationProperties, LocationWithNavigationPropertiesDto>();
            CreateMap<WeddingWithNavigationProperties, WeddingWithNavigationPropertiesDto>();
        }
    }
}
