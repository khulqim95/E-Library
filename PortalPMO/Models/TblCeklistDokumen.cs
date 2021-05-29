using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblCeklistDokumen
    {
        public int Id { get; set; }
        public int? IdDebitur { get; set; }
        public int? ProspekId { get; set; }
        public int? IdPertanyaan { get; set; }
        public bool? Jawaban { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
