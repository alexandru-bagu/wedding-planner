using System;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
    public class LookupInvitationsInputDto : LookupRequestDto
    {
        public Guid? WeddingId { get; set; }

        public string Destination { get; set; }

        public bool? GroomSide {get;set;}
        public bool? BrideSide {get;set;}

        public LookupInvitationsInputDto()
        {
        }
    }
}