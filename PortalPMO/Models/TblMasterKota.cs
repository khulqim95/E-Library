using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterKota
    {
        public int Id { get; set; }
        public int? KotaId { get; set; }
        public int? ProvinsiId { get; set; }
        public string Kota { get; set; }
        public bool? IsActive { get; set; }
    }
}
