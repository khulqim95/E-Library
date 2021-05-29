using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class MasterJabatanRes_VM
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public int? GradeAwal { get; set; }
        public int? GradeAkhir { get; set; }
        public string Keterangan { get; set; }
        public int? Order_By { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Status { get; set; }
        //public DateTime? DeletedTime { get; set; }
        //public int? CreatedById { get; set; }
        //public int? UpdatedById { get; set; }
        //public int? DeletedById { get; set; }
        public string IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
