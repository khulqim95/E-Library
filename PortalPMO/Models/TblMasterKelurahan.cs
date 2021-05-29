using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterKelurahan
    {
        public int Id { get; set; }
        public long? KelurahanId { get; set; }
        public int? KecamatanId { get; set; }
        public string Kelurahan { get; set; }
        public bool? IsActive { get; set; }
    }
}
