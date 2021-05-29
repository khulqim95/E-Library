using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblPipelineActivityLog
    {
        public int Id { get; set; }
        public string NoPengajuan { get; set; }
        public string Cif { get; set; }
        public string NamaDebitur { get; set; }
        public int? Step { get; set; }
        public int? PkStep { get; set; }
        public DateTime? StartStepTime { get; set; }
        public DateTime? EndStepTime { get; set; }
        public int? RmId { get; set; }
        public int? UnitRmId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedById { get; set; }
        public string CreatedByUnitCode { get; set; }
        public bool? IsDone { get; set; }
    }
}
