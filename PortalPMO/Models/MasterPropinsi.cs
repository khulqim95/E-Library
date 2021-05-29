using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class MasterPropinsi
    {
        public int Id { get; set; }
        public int? ProvinsiId { get; set; }
        public string Provinsi { get; set; }
        public int? NegaraId { get; set; }
        public bool? IsActive { get; set; }
    }
}
