using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.TableMenus.Dtos
{
    [Serializable]
    public class CreateUpdateTableMenuDto
    {
        public string Name { get; set; }

        public bool Adult { get; set; }

        public int Order { get; set; }
    }
}