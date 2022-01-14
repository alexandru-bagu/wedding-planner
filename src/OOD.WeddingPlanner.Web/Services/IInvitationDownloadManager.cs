using OOD.WeddingPlanner.Invitations.Dtos;
using OOD.WeddingPlanner.Web.Download;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Services
{
    public interface IInvitationDownloadManager
    {
        Task<Guid> Begin(GetInvitationsInputDto input, Guid? tenantId = null);
        Task<DownloadStatus> Status(Guid id);
        Task<Stream> Download(Guid id);
        Task Cancel(Guid id);
    }
}
