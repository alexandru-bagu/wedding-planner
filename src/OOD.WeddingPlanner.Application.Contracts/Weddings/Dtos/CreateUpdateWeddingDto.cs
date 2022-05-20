using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Weddings.Dtos
{
    [Serializable]
    public class CreateUpdateWeddingDto
    {
        public string GroomName { get; set; }

        public string BrideName { get; set; }

        public string Name { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string InvitationNoteHtml { get; set; }
        
        public string InvitationHeaderHtml { get; set; }

        public string InvitationFooterHtml { get; set; }
    }
}