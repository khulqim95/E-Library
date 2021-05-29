using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class MasterKelurahan
    {
        public int Id { get; set; }
        public int KelurahanId { get; set; }
        public string Kelurahan { get; set; }
        public int KecamatanId { get; set; }
        public string KodePos { get; set; }
        public int? IsActive { get; set; }
    }
}
