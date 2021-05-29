using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterSektorUnggulan
    {
        public int Id { get; set; }
        public int? SektorUnggulanId { get; set; }
        public string SektorUnggulanName { get; set; }
    }
}
