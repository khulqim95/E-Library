using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblPengajuanKredit
    {
        public int Id { get; set; }
        public int? DeskripsiKreditId { get; set; }
        public int PengajuanKreditId { get; set; }
        public string PengajuanKreditName { get; set; }
        public int CcyId { get; set; }
        public string CcyName { get; set; }
        public string MaxKreditDiajukan { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int? JenisKreditId { get; set; }
        public string JenisKreditName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public int? UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
        public int? DeletedById { get; set; }
        public string DeletedByName { get; set; }
        public bool? IsDelete { get; set; }
    }
}
