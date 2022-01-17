using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitees.Dtos
{
    [Serializable]
    public class InviteeDto : FullAuditedEntityDto<Guid>
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public Guid? InvitationId { get; set; }

        public Guid? TableId { get; set; }

        public DateTime? RSVP { get; set; }

        public bool? Confirmed { get; set; }

        public bool Child { get; set; }

        public bool Male { get; set; }

        public bool PlusOne { get; set; }

        public int Order { get; set; }
    }
}