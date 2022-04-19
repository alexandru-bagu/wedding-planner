﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NanoXLSX;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.TableInvitees;
using OOD.WeddingPlanner.Web.Download;
using OOD.WeddingPlanner.Web.Models;
using OOD.WeddingPlanner.Web.Providers;
using OOD.WeddingPlanner.Web.Services;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;
using Newtonsoft.Json;

namespace OOD.WeddingPlanner.Web.Controllers
{
    public class PrintController : AbpController
    {
        public IInvitationRepository Repository { get; }
        public IInvitationDesignRepository DesignRepository { get; }
        public IConverter HtmlConverter { get; }
        public IInvitationDownloadManager InvitationDownloadManager { get; }
        public ITenantStore TenantStore { get; }
        public ITableInviteeAppService TableInviteeAppService { get; }

        public PrintController(IInvitationRepository repository, IInvitationDesignRepository designRepository, IConverter htmlConverter,
            IInvitationDownloadManager invitationDownloadManager, ITenantStore tenantStore, ITableInviteeAppService tableInviteeAppService)
        {
            Repository = repository;
            DesignRepository = designRepository;
            HtmlConverter = htmlConverter;
            InvitationDownloadManager = invitationDownloadManager;
            TenantStore = tenantStore;
            TableInviteeAppService = tableInviteeAppService;
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
            return File(content, "application/pdf", $"{invitation.Destination}.pdf");
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
                var invitationData = ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(await Repository.GetWithFullNavigationByIdAsync(id));
                return View("Invitation/Print", invitationData);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/rsvp/{code}")]
        public async Task<IActionResult> GetViewByCode(string code)
        {
            var data = await Repository.GetWithFullNavigationByCodeAsync(code);
            string tenant_name = "";
            if (data.Invitation.TenantId.HasValue)
            {
                var tenant = await TenantStore.FindAsync(data.Invitation.TenantId.Value);
                tenant_name = tenant.Name;
            }
            using (CurrentTenant.Change(data.Invitation.TenantId))
            {
                HttpContext.Response.Cookies.Append("tenant_name", tenant_name ?? "");
                var invitationData = ObjectMapper.Map<InvitationWithNavigationProperties, InvitationWithNavigationPropertiesDto>(data);
                var viewModel = ObjectMapper.Map<InvitationWithNavigationPropertiesDto, ViewInvitationModel>(invitationData);
                await viewModel.PrepareAsync(HttpContext.RequestServices);
                if (HttpContext.Items.TryGetValue(UserPreferenceDefaultRequestCultureProvider.UserLanguagePreference, out _))
                    return Redirect($"/rsvp/{code}?culture={invitationData.Design.DefaultCulture}");
                HttpContext.Items["TenantName"] = tenant_name;
                return View("Invitation/View", viewModel);
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
                var data = await Repository.GetAsync(id);
                return Redirect($"/rsvp/{data.UniqueCode}");
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
        public async Task<IActionResult> DownloadBegin([FromBody] GetInvitationsInputDto input)
        {
            Logger.LogInformation($"Processing {JsonConvert.SerializeObject(input)}");
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

        [HttpGet]
        [Route("/download/tables/{eventId}")]
        [Authorize(WeddingPlannerPermissions.Table.Default)]
        public async Task<IActionResult> DownloadTables(Guid eventId)
        {
            var invitees = await TableInviteeAppService.GetListWithNavigationAsync(new TableInvitees.Dtos.GetTableInviteesDto() { EventId = eventId, SkipCount = 0, MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount });
            var stream = new MemoryStream();
            Workbook workbook = new Workbook(false);
            foreach (var tableGroupping in invitees.Items.GroupBy(p => p.Table.Id))
            {
                var table = tableGroupping.FirstOrDefault()?.Table;
                if (table != null)
                {
                    var worksheet = new Worksheet() { SheetName = table.Name };
                    workbook.AddWorksheet(worksheet);
                    int index = 0;
                    foreach (var invitee in tableGroupping)
                    {
                        worksheet.AddCell(index + 1, 0, index);
                        worksheet.AddCell(invitee.Invitee.Surname + " " + invitee.Invitee.Name, 1, index);
                        index++;
                    }
                }
            }
            workbook.SaveAsStream(stream, true);
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tables.xlsx");
        }
    }
}
