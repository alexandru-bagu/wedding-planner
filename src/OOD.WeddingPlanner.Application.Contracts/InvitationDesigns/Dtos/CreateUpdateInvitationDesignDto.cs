using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.InvitationDesigns.Dtos
{
    [Serializable]
    public class CreateUpdateInvitationDesignDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public string MeasurementUnit { get; set; }

        public double? PaperWidth { get; set; }

        public double? PaperHeight { get; set; }

        public double? PaperDpi { get; set; }
    }
}