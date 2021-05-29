using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterKecamatan
    {
        public int Id { get; set; }
        public int? KecamatanId { get; set; }
        public int? KotaId { get; set; }
        public string Kecamatan { get; set; }
        public bool? IsActive { get; set; }
    }
}
