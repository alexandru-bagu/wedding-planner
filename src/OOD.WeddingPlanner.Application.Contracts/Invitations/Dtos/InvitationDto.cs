using OOD.WeddingPlanner.Invitees.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Invitations.Dtos
{
    [Serializable]
    public class InvitationDto : FullAuditedEntityDto<Guid>
    {
        public Guid? WeddingId { get; set; }

        public Guid? DesignId { get; set; }

        public bool PlusOne { get; set; }

        public string Destination { get; set; }

        public string UniqueCode { get; set; }

        public List<InviteeDto> Invitees { get; set; }
    }
}