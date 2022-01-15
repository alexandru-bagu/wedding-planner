using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitees;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Web.Controllers
{
    public class InvitationController : AbpController
    {
        public IInviteeRepository Repository { get; }
        public ITenantStore TenantStore { get; }

        public InvitationController(IInviteeRepository repository, ITenantStore tenantStore)
        {
            Repository = repository;
            TenantStore = tenantStore;
        }
        private async Task<Guid?> GetTenantId(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var tenant = await TenantStore.FindAsync(name);
            return tenant.Id;
        }

        [HttpPost]
        [Route("RSVP/{id}/{tenant_name?}")]
        [AllowAnonymous]
        public async Task RSVP(Guid id, string tenant_name, [FromBody] bool confirmed)
        {
            var tenantId = await GetTenantId(tenant_name);
            using (CurrentTenant.Change(tenantId))
            {
                var invitee = await Repository.GetAsync(id);
                invitee.RSVP = DateTime.Now;
                invitee.Confirmed = confirmed;
                await Repository.UpdateAsync(invitee, true);
            }
        }
    }
}
