using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.TableMenus.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.TableMenus
{
    public class TableMenuAppService : CrudAppService<TableMenu, TableMenuDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTableMenuDto, CreateUpdateTableMenuDto>,
        ITableMenuAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.TableMenu.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.TableMenu.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.TableMenu.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.TableMenu.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.TableMenu.Delete;

        private readonly ITableMenuRepository _repository;
        
        public TableMenuAppService(ITableMenuRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
