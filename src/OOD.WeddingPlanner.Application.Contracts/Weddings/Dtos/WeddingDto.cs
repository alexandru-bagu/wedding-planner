using OOD.WeddingPlanner.Events.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.Weddings.Dtos
{
    [Serializable]
    public class WeddingDto : FullAuditedEntityDto<Guid>
    {
        public string GroomName { get; set; }

        public string BrideName { get; set; }

        public string Name { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string InvitationNote { get; set; }
        
        public string InvitationStyle { get; set; }

        public List<EventDto> Events { get; set; }
    }
}