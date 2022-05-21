using System;
using Volo.Abp.Application.Dtos;

namespace OOD.WeddingPlanner.TableMenus.Dtos
{
    [Serializable]
    public class TableMenuDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public bool Adult { get; set; }

        public int Order { get; set; }
    }
}