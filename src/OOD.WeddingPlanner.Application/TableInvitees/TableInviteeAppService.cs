using System;
using System.Linq;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.TableInvitees.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using OOD.WeddingPlanner.Invitees.Dtos;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Tables;

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
            query = query.Include(p => p.Table).Include(p => p.Invitee);
            query = query.WhereIf(input.TableId.HasValue, p => p.TableId == input.TableId);
            query = query.WhereIf(input.InviteeId.HasValue, p => p.InviteeId == input.InviteeId);
            query = query.WhereIf(input.EventId.HasValue, p => p.Table.EventId == input.EventId);
            return query;
        }

        public async Task<PagedResultDto<TableInviteeWithNavigationPropertiesDto>> GetListWithNavigationAsync(GetTableInviteesDto input)
        {
            var query = await CreateFilteredQueryAsync(input);
            return new PagedResultDto<TableInviteeWithNavigationPropertiesDto>(await query.LongCountAsync(),
                await query.Select(p => new TableInviteeWithNavigationPropertiesDto() { Invitee = ObjectMapper.Map<Invitee, InviteeDto>(p.Invitee), Table = ObjectMapper.Map<Table, TableDto>(p.Table) }).PageBy(input).ToListAsync());
        }
    }
}
