using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Invitees.Dtos;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.Tables.Dtos
{
    public class TableWithNavigationPropertiesDto
    {
        public TableDto Table { get; set; }
        public EventDto Event { get; set; }
        public List<InviteeDto> Invitees { get; set; }
    }
}
