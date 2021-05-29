using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class MasterKotaKabupaten
    {
        public int Id { get; set; }
        public int KotaId { get; set; }
        public string Kota { get; set; }
        public int? ProvinsiId { get; set; }
        public bool? IsActive { get; set; }
    }
}
