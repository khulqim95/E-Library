using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterIconMenu
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Images { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CeratedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? UpdatedById { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
