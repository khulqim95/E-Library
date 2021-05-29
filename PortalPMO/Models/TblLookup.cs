using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblLookup
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
        public int? OrderBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
