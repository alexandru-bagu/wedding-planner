using System;
using OOD.WeddingPlanner.TableMenus.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.TableMenus
{
    public interface ITableMenuAppService :
        ICrudAppService< 
            TableMenuDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateTableMenuDto,
            CreateUpdateTableMenuDto>
    {

    }
}