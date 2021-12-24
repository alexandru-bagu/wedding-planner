using System;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
    public class InvitationWithNavigationPropertiesDto
    {
        public InvitationDto Invitation { get; set; }

        public WeddingDto Wedding { get; set; }
    }
}
