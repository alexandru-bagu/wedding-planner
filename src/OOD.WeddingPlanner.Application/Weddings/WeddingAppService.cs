using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Weddings.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OOD.WeddingPlanner.Weddings
{
    public class WeddingAppService : CrudAppService<Wedding, WeddingDto, Guid, GetWeddingsInputDto, CreateUpdateWeddingDto, CreateUpdateWeddingDto>,
        IWeddingAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.Wedding.Delete;

        private readonly IWeddingRepository _repository;
        
        public WeddingAppService(IWeddingRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
