using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class DataProjectLogDetails_ViewModels
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string NamaProject { get; set; }
        public int? PegawaiIdFrom { get; set; }
        public int? PegawaiIdTo { get; set; }
        public string NamaPegawaiFrom { get; set; }
        public string NamaPegawaiUnit { get; set; }
        public string CreatedBy { get; set; }
        public string UnitFrom { get; set; }
        public string UnitTo { get; set; }
        public string Komentar { get; set; }
        public string Keterangan { get; set; }
        public string NamaActivity { get; set; }
        public string CreatedTime { get; set; }
        public string Time { get; set; }

    }
}
