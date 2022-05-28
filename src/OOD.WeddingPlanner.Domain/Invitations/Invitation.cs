using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Invitations
{
    public class Invitation : FullAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid? WeddingId { get; set; }

        public virtual Guid? DesignId { get; set; }

        public virtual string Destination { get; set; }

        public virtual bool PlusOne { get; set; }

        public virtual string UniqueCode { get; set; }
        
        public virtual bool GroomSide { get; set; }
        
        public virtual bool BrideSide { get; set; }

        public virtual string Notes { get; set; }

        public virtual List<Invitee> Invitees { get; set; } = new List<Invitee>();

        public virtual Wedding Wedding { get; set; }

        public virtual InvitationDesign Design { get; set; }

        protected Invitation()
        {
        }

        public Invitation(
            Guid id,
            Guid? tenantId,
            Guid? weddingId,
            string destination
        ) : base(id)
        {
            TenantId = tenantId;
            WeddingId = weddingId;
            Destination = destination;
        }

        public static async Task<string> GenerateUniqueCode(IInvitationRepository repo)
        {
            var unique = "";
            while (unique == "")
            {
                var hash = Sha256(Guid.NewGuid().ToString());
                int len = 3;
                do { unique = hash.Substring(0, ++len); }
                while (len < hash.Length && await repo.UniqueCodeUsedAsync(unique));
            }
            return unique;
        }

        static string Sha256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
