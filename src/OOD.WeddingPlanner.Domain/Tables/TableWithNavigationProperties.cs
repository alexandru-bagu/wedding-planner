using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitees;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.Tables
{
    public class TableWithNavigationProperties
    {
        public Table Table { get; set; }
        public Event Event { get; set; }
        public List<Invitee> Invitees { get; set; }
    }
}
