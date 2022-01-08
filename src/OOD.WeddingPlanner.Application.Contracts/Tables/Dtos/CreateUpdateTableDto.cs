using System;
namespace OOD.WeddingPlanner.Tables.Dtos
{
    [Serializable]
    public class CreateUpdateTableDto
    {
        public Guid? EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Size { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}