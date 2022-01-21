using System;
using System.Collections.Generic;
namespace OOD.WeddingPlanner.Invitations.Dtos
{
    [Serializable]
    public class CreateUpdateInvitationDto : UniqueInvitationDto
    {
        public Guid? WeddingId { get; set; }

        public Guid? DesignId { get; set; }

        public string Destination { get; set; }

        public bool PlusOne { get; set; }

        public List<Guid> InviteeIds { get; set; } = new List<Guid>();
    }
}