using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblChecklistChecking
    {
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public bool? ChecklistDukcapil { get; set; }
        public bool? ChecklistNpwp { get; set; }
        public bool? ChecklistSlik { get; set; }
        public bool? ChecklistDhn { get; set; }
        public bool? ChecklistOss { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
