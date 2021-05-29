using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblFasilitasKredit
    {
        public int Id { get; set; }
        public string NoPengajuan { get; set; }
        public int? IdDebitur { get; set; }
        public int? IdSolicite { get; set; }
        public int? KategoriLeadsId { get; set; }
        public int? DeskripsiKreditId { get; set; }
        public int? JenisPengajuanId { get; set; }
        public string JenisPengajuanName { get; set; }
        public int? TypeKreditId { get; set; }
        public string TypeKreditName { get; set; }
        public int? IdTingkatKomite { get; set; }
        public string TingkatKomite { get; set; }
        public int? IdJenisFasilitas { get; set; }
        public string JenisFasilitas { get; set; }
        public int? IdValuta { get; set; }
        public string Valuta { get; set; }
        public decimal? MaksimumKredit { get; set; }
        public decimal? MaksimumKreditIdr { get; set; }
        public string Tujuan { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsActive { get; set; }
        public int? FlowStatusId { get; set; }
        public string FlowStatusName { get; set; }
    }
}
