using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.InvitationDesigns.Dtos
{
    [Serializable]
    public class CreateUpdateInvitationDesignDto
    {
        public string Name { get; set; }

        public string Body { get; set; }
    }
}