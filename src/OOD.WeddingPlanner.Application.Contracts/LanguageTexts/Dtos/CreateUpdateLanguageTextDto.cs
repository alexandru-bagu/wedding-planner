using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.LanguageTexts.Dtos
{
    [Serializable]
    public class CreateUpdateLanguageTextDto
    {
        public string CultureName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}