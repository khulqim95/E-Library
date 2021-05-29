using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMappingKelolaanSegment
    {
        public int Id { get; set; }
        public int? UnitId { get; set; }
        public int? SegmentKelolaan { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
