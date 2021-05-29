using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterSektorIndustri
    {
        public int Id { get; set; }
        public int? SektorIndustriId { get; set; }
        public string SektorIndustriName { get; set; }
        public int? SektorUnggulanId { get; set; }
    }
}
