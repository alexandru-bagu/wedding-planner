using System;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Weddings.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Weddings
{
    public interface IWeddingAppService :
        ICrudAppService< 
            WeddingDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateWeddingDto,
            CreateUpdateWeddingDto>
    {
        Task<WeddingWithNavigationPropertiesDto> GetWithNavigationById(Guid id);
    }
}