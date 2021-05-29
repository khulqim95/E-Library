using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblChecklistDisclaimer
    {
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public int? SoalId { get; set; }
        public string Deskripsi { get; set; }
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
