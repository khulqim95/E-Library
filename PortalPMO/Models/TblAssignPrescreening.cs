using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblAssignPrescreening
    {
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public int? JenisKomiteId { get; set; }
        public string JenisKomiteName { get; set; }
        public int? IdPemutusBisnis { get; set; }
        public int? IdPemutusResiko { get; set; }
        public string Alasan { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
