using System;

namespace OOD.WeddingPlanner.Events.Dtos
{
    public class LookupEventsInputDto : LookupRequestDto
    {
        public Guid? WeddingId { get; set; }
    }
}
