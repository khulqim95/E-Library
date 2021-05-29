using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoTierTemplate.ViewModels
{
    public class PreScreening_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? Prospek_Id { get; set; }
        public string TrperusahaanBeroperasi { get; set; }
        public string Trperusahaan { get; set; }
        public string TrkeyPerson { get; set; }
        public string TrstrukturKepemilikan { get; set; }
        public string TrpengalamanKeyPerson { get; set; }
        public string TrkompetensiManajemen { get; set; }
        public string OprIndustri { get; set; }
        public string OprLegalitasUsaha { get; set; }
        public string OprLaporanKeuangan { get; set; }
        public string OprTuntutanHukum { get; set; }
        public string OprRisikoBisnis { get; set; }
        public string OprStrategiPerusahaan { get; set; }
        public string OprDaftarPelanggan { get; set; }
        public string OprDaftarPemasok { get; set; }
        public string OprPengelolaanLimbah { get; set; }
        public string OprAmdal { get; set; }
        public string PkmodalAwal { get; set; }
        public string Pkpertumbuhan { get; set; }
        public string Pkder { get; set; }
        public string PklabaBersih { get; set; }
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
        public string NamaCRM { get; set; }
        public string NamaKMB { get; set; }
        public string NamaSKM { get; set; }
    }

    public class PersyaratanLainnya_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public bool? IsNonPerformingLoan { get; set; }
        public bool? IsRetrukturisasi { get; set; }
        public bool? JenisUsahaDisekitar { get; set; }
        public int? SoalId { get; set; }
        public string Jawaban { get; set; }
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
