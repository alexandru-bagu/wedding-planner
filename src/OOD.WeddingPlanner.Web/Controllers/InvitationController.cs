using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Web.Pages.Invitees.ViewModels;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.MultiTenancy;
using MicroKnights.Gender_API;
using System.Net.Http;
using System.Globalization;
using OOD.WeddingPlanner.Invitations;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OOD.WeddingPlanner.Web.Controllers
{
    public class InvitationController : AbpController
    {
        public IInviteeRepository Repository { get; }
        public IInvitationRepository InvitationRepository { get; }
        public ITenantStore TenantStore { get; }

        public InvitationController(IInviteeRepository repository, IInvitationRepository invitationRepository, ITenantStore tenantStore)
        {
            Repository = repository;
            InvitationRepository = invitationRepository;
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

        [HttpPost]
        [Route("PlusOne/{id}/{tenant_name?}")]
        [AllowAnonymous]
        public async Task<IActionResult> PlusOne(Guid id, string tenant_name, [FromBody] CreatePlusOneViewModel model)
        {
            var tenantId = await GetTenantId(tenant_name);
            using (CurrentTenant.Change(tenantId))
            {
                var invitation = await InvitationRepository.GetWithFullNavigationByIdAsync(id);
                string gender = "male";
                try
                {
                    var client = new GenderApiClient(new HttpClient { BaseAddress = new Uri("https://gender-api.com") }, new GenderApiConfiguration { ApiKey = "3eluzDcaydE8LpLhTXyDBBkY8xF34Cm7WeeU" });
                    var response = await client.GetByNameAndCountry2Alpha($"{model.Name} {model.Surname}", new CultureInfo(invitation.Design.DefaultCulture).TwoLetterISOLanguageName);
                    gender = response.Gender;
                }
                catch (Exception ex) { Logger.LogError(ex, "PlusOne"); }
                if (!invitation.Invitation.Invitees.Any(p => p.PlusOne))
                {
                    var invitee = new Invitee(Guid.NewGuid(), tenantId, model.Surname, model.Name, id, null, true, false, gender.Equals("male", StringComparison.InvariantCultureIgnoreCase), true);
                    await Repository.InsertAsync(invitee);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
