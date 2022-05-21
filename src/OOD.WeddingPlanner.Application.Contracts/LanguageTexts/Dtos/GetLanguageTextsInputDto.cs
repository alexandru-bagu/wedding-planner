using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.LanguageTexts.Dtos
{
    public class GetLanguageTextsInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        [Required]
        public string CultureName { get; set; }

        public GetLanguageTextsInputDto()
        {
        }
    }
}