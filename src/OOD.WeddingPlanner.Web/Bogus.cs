using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Locations;
using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.InvitationDesigns.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;
using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Invitations.Dtos;

namespace OOD.WeddingPlanner.Web
{
    public class Bogus
    {
        public static InvitationWithNavigationPropertiesDto InvitationWithNavigationProperties()
        {
            var invitationDesign = new InvitationDesignDto() { Id = Guid.NewGuid(), Name = "Invitation Design", Body = "Invitation Design Body", MeasurementUnit = "cm", PaperWidth = 21.0, PaperHeight = 29.7, PaperDpi = 96 };
            var wedding = new WeddingDto() { Id = Guid.NewGuid(), GroomName = "Groom", BrideName = "Bride", Name = "Wedding", ContactPhoneNumber = "Contact information" };
            var location = new LocationDto() { Id = Guid.NewGuid(), Name = "Location", Description = "Description", Latitude = 46, Longitude = 23 };
            wedding.Events = new List<EventDto>() {
              new EventDto() { Id = Guid.NewGuid(), LocationId =  location.Id, Location =location, WeddingId = wedding.Id,  Name = "Event",Time=  DateTime.Now}
            };
            var invitees = new List<InviteeDto>();
            var invitation = new InvitationDto() { Id = Guid.NewGuid(), WeddingId = wedding.Id, Destination = "Invitation", DesignId = invitationDesign.Id, UniqueCode = "code", Invitees = invitees };
            for (int i = 0; i < 4; i++)
            {
                invitees.Add(new InviteeDto()
                {
                    Id = Guid.NewGuid(),
                    Surname = "Surname",
                    Name = "Name " + i,
                    InvitationId = invitation.Id,
                    RSVP = i == 0 ? DateTime.Now : i == 1 ? null : i == 2 ? DateTime.Now.AddDays(-3) : null,
                    Confirmed = i == 0 ? null : i == 1 ? false : i == 2 ? true : null,
                    Child = i == 0 ? true : false,
                    Male = i == 1 ? true : false,
                    PlusOne = i == 2 ? true : false
                });
            }
            
            return new InvitationWithNavigationPropertiesDto()
            {
                Invitation = invitation,
                Design = invitationDesign,
                Wedding = wedding
            };
        }
    }
}
