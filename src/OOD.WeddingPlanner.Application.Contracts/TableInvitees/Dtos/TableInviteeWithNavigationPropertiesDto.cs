using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Tables.Dtos;

namespace OOD.WeddingPlanner.TableInvitees.Dtos
{
    public class TableInviteeWithNavigationPropertiesDto
    {
        public TableDto Table { get; set; }
        public InviteeDto Invitee { get; set; }
    }
}
