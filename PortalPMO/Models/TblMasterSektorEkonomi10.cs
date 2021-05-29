using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterSektorEkonomi10
    {
        public int Id { get; set; }
        public int? SektorEkonomiId { get; set; }
        public string SektorEkonomiName { get; set; }
        public bool? IsActive { get; set; }
    }
}
