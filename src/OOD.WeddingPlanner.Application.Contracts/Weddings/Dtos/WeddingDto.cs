using OOD.WeddingPlanner.Events.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string InvitationNoteHtml { get; set; }
        
        public string InvitationHeaderHtml { get; set; }
        
        public string InvitationFooterHtml { get; set; }

        public List<EventDto> Events { get; set; }
    }
}