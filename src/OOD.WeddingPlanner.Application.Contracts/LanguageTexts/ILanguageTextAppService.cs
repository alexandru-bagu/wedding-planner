using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.LanguageTexts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public interface ILanguageTextAppService :
        ICrudAppService< 
            LanguageTextDto, 
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateLanguageTextDto,
            CreateUpdateLanguageTextDto>
    {
        Task<PagedResultDto<LanguageTextDto>> GetAllListAsync(GetLanguageTextsInputDto input);
    }
}