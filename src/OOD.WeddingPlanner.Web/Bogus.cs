﻿using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Locations;
using System;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.Web
{
    public class Bogus
    {
        public static InvitationWithNavigationProperties InvitationWithNavigationProperties()
        {
            var invitationDesign = new InvitationDesign(Guid.NewGuid(), null, "Bogus Invitation Design", "Bogus Invitation Design Body", "cm", 21.0, 29.7, 96);
            var wedding = new Wedding(Guid.NewGuid(), null, "Bogus Groom", "Bogus Bride", "Bogus Wedding", "Bogus Contact Information");
            var location = new Location(Guid.NewGuid(), null, "Bogus Location", "Bogus Description", 46, 23);
            wedding.Events  = new List<Event>() { 
              new Event(Guid.NewGuid(), null, location.Id, wedding.Id, "Bogus Event", DateTime.Now)
            };
            var invitees = new List<Invitee>();
            var invitation = new Invitation(Guid.NewGuid(), null, wedding.Id, "Bogus Invitation")
            {
                Wedding = wedding,
                Design = invitationDesign,
                DesignId = invitationDesign.Id,
                Invitees = invitees
            };
            for (int i = 0; i < 4; i++)
            {
                invitees.Add(new Invitee(Guid.NewGuid(), null, "Bogus Surname", "Bogus Name " + i, invitation.Id,
                    i == 0 ? DateTime.Now : i == 1 ? null : i == 2 ? DateTime.Now.AddDays(-3) : null,
                    i == 0 ? null : i == 1 ? false : i == 2 ? true : null,
                    i == 0 ? true : false,
                    i == 1 ? true : false,
                    i == 2 ? true : false));
            }
            return new InvitationWithNavigationProperties()
            {
                Invitation = invitation,
                Design = invitationDesign,
                Wedding = wedding
            };
        }
    }
}
