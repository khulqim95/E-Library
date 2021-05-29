using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterPertanyaan
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Pertanyaan { get; set; }
        public bool? DefaultJawaban { get; set; }
        public int? Score { get; set; }
        public bool? IsMandatory { get; set; }
        public int? OrderBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
