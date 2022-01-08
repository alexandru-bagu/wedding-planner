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

        public Guid? TableId { get; set; }

        public DateTime? RSVP { get; set; }

        public bool? Confirmed { get; set; }
    }
}