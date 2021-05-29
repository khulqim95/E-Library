using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class PengaturanMenu_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Tipe { get; set; }
        public int TipeId { get; set; }

        public string Route { get; set; }
        public string Icon { get; set; }

        public string Role { get; set; }
        public int? ParentId { get; set; }

        public string Parent { get; set; }
        public int OrderBy { get; set; }
        public int Visible { get; set; }

        public string Visible_Name { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }

    }
}
