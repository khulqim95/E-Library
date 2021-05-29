using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoTierTemplate.ViewModels
{
    public class TblFasilitasKreditDashboard_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string JenisPengajuan_Name { get; set; }
        public string TypeKredit_Name { get; set; }
        public string JenisFasilitas { get; set; }
        public string Valuta { get; set; }
        public string MaksimumKredit { get; set; }
        public string Tujuan { get; set; }
        public string FlowStatus_Name { get; set; }
        public string Action_Name { get; set; }
    }
}
