using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblFlowProspek
    {
        public int Id { get; set; }
        public int? IdProspek { get; set; }
        public int? ActionId { get; set; }
        public string ActionName { get; set; }
        public int? IdRolePengirim { get; set; }
        public int? IdPengirim { get; set; }
        public int? IdPenerima { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsActive { get; set; }
        public string Komentar { get; set; }
    }
}
