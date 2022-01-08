using System;

namespace OOD.WeddingPlanner.Tables.Dtos
{
    public class LookupTablesInputDto : LookupRequestDto
    {
        public Guid? EventId { get; set; }
    }
}
