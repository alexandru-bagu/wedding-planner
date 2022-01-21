using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.TableInvitees.Dtos
{
    [Serializable]
    public class CreateUpdateTableInviteeDto
    {
        public Guid? TableId { get; set; }

        public Guid? InviteeId { get; set; }
    }
}