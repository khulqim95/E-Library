using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class Home_ViewModels
    {
        public HomeTotalProject_ViewModels ProjectTotal { get; set; }

    }

    public class HomeTotalProject_ViewModels {
        public int? DalamProses { get; set; }
        public int? Selesai { get; set; }
        public int? Total { get; set; }
        public int? Presentase { get; set; }
    }
}
