using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class Dashboard_TotalProject_ViewModels
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int? Jumlah { get; set; }
        public string Keterangan { get; set; }
    }


    public class Dashboard_Workload_ViewModels
    {
        public int? PegawaiId { get; set; }
        public string label { get; set; }
        public int? y { get; set; }
        public int? total { get; set; }
        public int? jumlah { get; set; }

    }

    public class Dashboard_Summary_ViewModels
    {
        public int? PresentaseProjectClose { get; set; }
        public int? PresentaseOverdueProject { get; set; }
        public int? PresentaseUpcomingProject { get; set; }

    }
}
