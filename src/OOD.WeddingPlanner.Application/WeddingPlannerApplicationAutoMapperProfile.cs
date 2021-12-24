using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using AutoMapper;

namespace OOD.WeddingPlanner
{
    public class WeddingPlannerApplicationAutoMapperProfile : Profile
    {
        public WeddingPlannerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Location, LocationDto>();
            CreateMap<CreateUpdateLocationDto, Location>(MemberList.Source);
            CreateMap<Event, EventDto>();
            CreateMap<CreateUpdateEventDto, Event>(MemberList.Source);
            CreateMap<Invitee, InviteeDto>();
            CreateMap<CreateUpdateInviteeDto, Invitee>(MemberList.Source);
        }
    }
}
