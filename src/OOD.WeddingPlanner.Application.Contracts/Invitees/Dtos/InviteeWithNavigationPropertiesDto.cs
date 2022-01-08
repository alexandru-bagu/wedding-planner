using System;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Weddings.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
    public class InviteeWithNavigationPropertiesDto
    {
        public InviteeDto Invitee { get; set; }
        public InvitationDto Invitation { get; set; }
        public WeddingDto Wedding { get; set; }
        public TableDto Table { get; set; }
    }
}
