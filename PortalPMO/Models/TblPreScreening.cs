using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblPreScreening
    {
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public bool? TrperusahaanBeroperasi { get; set; }
        public bool? Trperusahaan { get; set; }
        public bool? TrkeyPerson { get; set; }
        public bool? TrstrukturKepemilikan { get; set; }
        public bool? TrpengalamanKeyPerson { get; set; }
        public bool? TrkompetensiManajemen { get; set; }
        public bool? OprIndustri { get; set; }
        public bool? OprLegalitasUsaha { get; set; }
        public bool? OprLaporanKeuangan { get; set; }
        public bool? OprTuntutanHukum { get; set; }
        public bool? OprRisikoBisnis { get; set; }
        public bool? OprStrategiPerusahaan { get; set; }
        public bool? OprDaftarPelanggan { get; set; }
        public bool? OprDaftarPemasok { get; set; }
        public bool? OprPengelolaanLimbah { get; set; }
        public bool? OprAmdal { get; set; }
        public bool? PkmodalAwal { get; set; }
        public bool? Pkpertumbuhan { get; set; }
        public bool? Pkder { get; set; }
        public bool? PklabaBersih { get; set; }
        public string Alasan { get; set; }
        public string Keputusan { get; set; }
        public string Pengusul { get; set; }
        public string Menyetujui { get; set; }
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
