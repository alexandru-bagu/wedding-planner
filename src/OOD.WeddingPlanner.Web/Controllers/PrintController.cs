using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Web.Download;
using OOD.WeddingPlanner.Web.Models;
using OOD.WeddingPlanner.Web.Providers;
using OOD.WeddingPlanner.Web.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace OOD.WeddingPlanner.Web.Controllers
{
    public class PrintController : AbpController
    {
        public IInvitationRepository Repository { get; }
        public IInvitationDesignRepository DesignRepository { get; }
        public IConverter HtmlConverter { get; }
        public IInvitationDownloadManager InvitationDownloadManager { get; }
        public ITenantStore TenantStore { get; }

        public PrintController(IInvitationRepository repository, IInvitationDesignRepository designRepository, IConverter htmlConverter,
            IInvitationDownloadManager invitationDownloadManager, ITenantStore tenantStore)
        {
            Repository = repository;
            DesignRepository = designRepository;
            HtmlConverter = htmlConverter;
            InvitationDownloadManager = invitationDownloadManager;
            TenantStore = tenantStore;
        }

        private async Task<Guid?> GetTenantId(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var tenant = await TenantStore.FindAsync(name);
            return tenant.Id;
        }

        [HttpPost]
        [Route("Invitation/Print/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> PostPrint(Guid id)
        {
            var invitation = await Repository.GetAsync(id);
            var design = await DesignRepository.GetAsync(invitation.DesignId.Value);
            var content = PrintInvitation(id, CurrentTenant.Name, design, HttpContext.Request.Scheme + "://" + HttpContext.Request.Host, HtmlConverter, Logger);
            return File(content, "application/pdf", $"{id}.pdf");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Invitation/Print/{id}/{tenant_name?}")]
        public async Task<IActionResult> GetPrint(Guid id, string tenant_name = null)
        {
            var tenantId = await GetTenantId(tenant_name);
            using (CurrentTenant.Change(tenantId))
            {
                HttpContext.Response.Cookies.Append("tenant_name", tenant_name ?? "");
                var invitationData = ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(await Repository.GetWithNavigationByIdAsync(id));
                return View("Invitation/Print", invitationData);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/{id}/{tenant_name?}")]
        public async Task<IActionResult> GetView(Guid id, string tenant_name = null)
        {
            var tenantId = await GetTenantId(tenant_name);
            using (CurrentTenant.Change(tenantId))
            {
                HttpContext.Response.Cookies.Append("tenant_name", tenant_name ?? "");
                var invitationData = ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(await Repository.GetWithFullNavigationByIdAsync(id));
                var viewModel = ObjectMapper.Map<InvitationWithNavigationPropertiesDto, ViewInvitationModel>(invitationData);
                await viewModel.PrepareAsync(HttpContext.RequestServices);
                if (HttpContext.Items.TryGetValue(UserPreferenceDefaultRequestCultureProvider.UserLanguagePreference, out _))
                    return Redirect($"/{id}/{tenant_name}?culture={invitationData.Design.DefaultCulture}");
                HttpContext.Items["TenantName"] = tenant_name;
                return View("Invitation/View", viewModel);
            }
        }

        public static byte[] PrintInvitation(Guid id, string tenant_name, InvitationDesign design, string baseUrl, IConverter converter, ILogger logger)
        {
            var url = baseUrl + $"/Invitation/Print/{id}/{tenant_name}";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize =  new PechkinPaperSize(design.PaperWidth + design.MeasurementUnit, design.PaperHeight + design.MeasurementUnit),
                    DPI = (int)design.PaperDpi,
                    Margins = new MarginSettings(0, 0, 0, 0)
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        Page = url,
                        LoadSettings = { DebugJavascript = true }
                    }
                }
            };
            logger.LogInformation($"Printing invitation by url {url}");
            return converter.Convert(doc);
        }

        [HttpPost]
        [Route("/download/begin")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> DownloadBegin(GetInvitationsInputDto input)
        {
            var id = await InvitationDownloadManager.Begin(input, CurrentTenant.Id);
            return Json(new { id });
        }

        [HttpPost]
        [Route("/download/cancel/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task DownloadCancel(Guid id)
        {
            await InvitationDownloadManager.Cancel(id);
        }

        [HttpGet]
        [Route("/download/status/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<DownloadStatus> DownloadStatus(Guid id)
        {
            return await InvitationDownloadManager.Status(id);
        }

        [HttpGet]
        [Route("/download/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> Download(Guid id)
        {
            var stream = await InvitationDownloadManager.Download(id);
            if (stream == null) return NotFound();
            return File(stream, "application/pdf", $"{id}.pdf");
        }
    }
}
