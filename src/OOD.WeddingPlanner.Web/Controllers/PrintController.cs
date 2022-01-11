using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Permissions;
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

        public PrintController(IInvitationRepository repository, IInvitationDesignRepository designRepository, IConverter htmlConverter)
        {
            Repository = repository;
            DesignRepository = designRepository;
            HtmlConverter = htmlConverter;
        }

        [HttpPost]
        [Route("/print/{id}")]
        [Authorize(WeddingPlannerPermissions.Invitation.Default)]
        public async Task<IActionResult> Print(Guid id)
        {
            var invitation = await Repository.GetAsync(id);
            var design = await DesignRepository.GetAsync(invitation.DesignId.Value);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize =  new PechkinPaperSize(design.PaperWidth + design.MeasurementUnit, design.PaperHeight+ design.MeasurementUnit),
                    DPI = (int)design.PaperDpi,
                    Margins = new MarginSettings(0, 0, 0, 0)
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        Page = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Print/" + id
                    }
                }
            };
            return File(HtmlConverter.Convert(doc), "application/pdf", "");
        }
    }
}
