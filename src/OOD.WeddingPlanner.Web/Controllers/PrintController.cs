using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.Web.Download;
using OOD.WeddingPlanner.Web.Services;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
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

        public PrintController(IInvitationRepository repository, IInvitationDesignRepository designRepository, IConverter htmlConverter, IInvitationDownloadManager invitationDownloadManager)
        {
            Repository = repository;
            DesignRepository = designRepository;
            HtmlConverter = htmlConverter;
            InvitationDownloadManager = invitationDownloadManager;
        }

        [HttpPost]
        [Route("/print/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> Print(Guid id)
        {
            var invitation = await Repository.GetAsync(id);
            var design = await DesignRepository.GetAsync(invitation.DesignId.Value);
            var content = PrintInvitation(id, design, HttpContext.Request.Scheme + "://" + HttpContext.Request.Host, HtmlConverter);
            return File(content, "application/pdf", $"{id}.pdf");
        }

        public static byte[] PrintInvitation(Guid id, InvitationDesign design, string baseUrl, IConverter converter)
        {
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
                        Page = baseUrl  + "/Print/" + id
                    }
                }
            };
            return converter.Convert(doc);
        }

        [HttpPost]
        [Route("/download/begin")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> DownloadBegin(GetInvitationsInputDto input)
        {
            var id = await InvitationDownloadManager.Begin(input);
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
