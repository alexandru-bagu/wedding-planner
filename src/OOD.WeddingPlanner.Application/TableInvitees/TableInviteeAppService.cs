using System;
using System.Linq;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.TableInvitees.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace OOD.WeddingPlanner.TableInvitees
{
    public class TableInviteeAppService : CrudAppService<TableInvitee, TableInviteeDto, Guid, GetTableInviteesDto, CreateUpdateTableInviteeDto, CreateUpdateTableInviteeDto>,
        ITableInviteeAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.TableInvitee.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.TableInvitee.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.TableInvitee.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.TableInvitee.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.TableInvitee.Delete;

        private readonly ITableInviteeRepository _repository;
        
        public TableInviteeAppService(ITableInviteeRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<TableInvitee>> CreateFilteredQueryAsync(GetTableInviteesDto input)
        {
            var query = await base.CreateFilteredQueryAsync(input);
            query = query.WhereIf(input.TableId.HasValue,  p => p.TableId == input.TableId);
            query = query.WhereIf(input.InviteeId.HasValue,  p => p.InviteeId == input.InviteeId);
            return query;
        }
    }
}
