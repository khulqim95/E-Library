using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoTierTemplate.ViewModels
{
    public class Tbl_Solicite
    {
        public int Total_Solicite { get; set; }
        public int Total_Debitur { get; set; }
        public string MaksimumKredit { get; set; }
        public string TotalMaxKredit { get; set; }
    }

    public class Tbl_Prospek
    {
        public int Total_Prospek { get; set; }
        public int Total_Debitur { get; set; }
        public string MaksimumKredit { get; set; }
        public string TotalMaxKredit { get; set; }
    }

    public class Tbl_Pipeline
    {
        public int Total_Pipeline { get; set; }
        public int Total_Debitur { get; set; }
        public string MaksimumKredit { get; set; }
        public string TotalMaxKredit { get; set; }
    }

    public class ExecutiveSummary
    {
        public int TotalUsulan { get; set; }
        public string MaksimumKreditUsulan { get; set; }
        public int TotalSetujui { get; set; }
        public string MaksimumKreditSetujui { get; set; }
        public int GapDebitur { get; set; }
        public string GapMaksimumKredit { get; set; }
    }

    public class Dashboard_ViewModels
    {
        public List<Tbl_Solicite> tblSolicite { get; set; }
        public List<Tbl_Prospek> tblProspek { get; set; }
        public List<Tbl_Pipeline> tblPipeline { get; set; }
        public List<ExecutiveSummary> ExecutiveSummary { get; set; }

    }


}
