using AutoMapper;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Locations;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Weddings.Dtos;
using OOD.WeddingPlanner.TableInvitees;
using OOD.WeddingPlanner.TableInvitees.Dtos;
using OOD.WeddingPlanner.TableMenus;
using OOD.WeddingPlanner.TableMenus.Dtos;
using OOD.WeddingPlanner.LanguageTexts;
using OOD.WeddingPlanner.LanguageTexts.Dtos;
using System;

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
            CreateMap<InvitationDesign, InvitationDesignDto>();
            CreateMap<Table, TableDto>();

            CreateMap<CreateUpdateLocationDto, Location>(MemberList.Source);
            CreateMap<CreateUpdateEventDto, Event>(MemberList.Source);
            CreateMap<CreateUpdateInviteeDto, Invitee>(MemberList.Source);
            CreateMap<CreateUpdateInvitationDto, Invitation>(MemberList.Source);
            CreateMap<CreateUpdateWeddingDto, Wedding>(MemberList.Source);
            CreateMap<CreateUpdateInvitationDesignDto, InvitationDesign>(MemberList.Source);
            CreateMap<CreateUpdateTableDto, Table>(MemberList.Source);

            CreateMap<EventWithNavigationProperties, EventWithNavigationPropertiesDto>();
            CreateMap<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>();
            CreateMap<InviteeWithNavigationProperties, InviteeWithNavigationPropertiesDto>();
            CreateMap<TableWithNavigationProperties, TableWithNavigationPropertiesDto>();

            CreateMap<Event, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => q.Name));
            CreateMap<Invitation, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => q.Destination));
            CreateMap<Invitee, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => $"{q.Surname} {q.Name}"));
            CreateMap<Location, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => $"{q.Name} - {q.Hall}"));
            CreateMap<Wedding, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => q.Name));
            CreateMap<InvitationDesign, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => q.Name));
            CreateMap<Table, LookupDto<Guid>>()
              .ForMember(p => p.DisplayName, p => p.MapFrom(q => q.Name));
            CreateMap<TableInvitee, TableInviteeDto>();
            CreateMap<CreateUpdateTableInviteeDto, TableInvitee>(MemberList.Source);
            CreateMap<TableMenu, TableMenuDto>();
            CreateMap<CreateUpdateTableMenuDto, TableMenu>(MemberList.Source);
            CreateMap<LanguageText, LanguageTextDto>();
            CreateMap<CreateUpdateLanguageTextDto, LanguageText>(MemberList.Source);
        }
    }
}
