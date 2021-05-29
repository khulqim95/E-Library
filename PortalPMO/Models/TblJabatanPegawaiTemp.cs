using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblJabatanPegawaiTemp
    {
        public int Id { get; set; }
        public int? PegawaiId { get; set; }
        public int? JabatanId { get; set; }
        public int? UnitId { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? StatusJabatan { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
