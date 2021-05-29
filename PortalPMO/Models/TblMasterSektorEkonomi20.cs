using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterSektorEkonomi20
    {
        public int Id { get; set; }
        public int? SektorEkonomi20Id { get; set; }
        public string SektorEkonomi20Name { get; set; }
        public int? SektorEkonomi10Id { get; set; }
    }
}
