using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Invitees.Dtos
{
    [Serializable]
    public class CreateUpdateInviteeDto
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public Guid? InvitationId { get; set; }

        public DateTime? RSVP { get; set; }

        public bool? Confirmed { get; set; }

        public bool Child { get; set; }

        public bool Male { get; set; }
        
        public string Menu { get; set; }

        public bool PlusOne { get; set; }

        public int Order { get; set; }
    }
}