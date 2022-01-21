using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.TableInvitees;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.Tables
{
    public class TableWithNavigationProperties
    {
        public Table Table { get; set; }
        public Event Event { get; set; }
        public List<TableInvitee> Assignments { get; set; }
    }
}
