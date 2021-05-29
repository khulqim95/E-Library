using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblFlowPipeline
    {
        public int Id { get; set; }
        public int? IdPipeline { get; set; }
        public int? IdProspek { get; set; }
        public int? ActionId { get; set; }
        public string ActionName { get; set; }
        public int? IdRolePengirim { get; set; }
        public string Komentar { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
