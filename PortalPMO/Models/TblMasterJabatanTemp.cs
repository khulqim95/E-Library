using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterJabatanTemp
    {
        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public int? GradeAwal { get; set; }
        public int? GradeAkhir { get; set; }
        public string Keterangan { get; set; }
        public int? OrderBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
