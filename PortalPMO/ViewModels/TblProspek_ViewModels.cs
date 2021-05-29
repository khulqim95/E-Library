using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoTierTemplate.ViewModels
{
    public class TblProspek_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? LeadsId { get; set; }
        public int? KategoriLeadsId { get; set; }
        public int? SoliciteId { get; set; }
        public string NamaDebitur { get; set; }
        public int? BadanUsahaId { get; set; }
        public string BadanUsahaName { get; set; }
        public string NamaGroupUsaha { get; set; }
        public string NamaKeyPerson { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string CIF { get; set; }
        public string Npwp { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public int? TipeDebiturId { get; set; }
        public string TipeDebiturName { get; set; }
        public int? ProvinsiId { get; set; }
        public string Provinsi { get; set; }
        public int? KabupatenId { get; set; }
        public string Kabupaten { get; set; }
        public int? KecamatanId { get; set; }
        public string Kecamatan { get; set; }
        public long? KelurahanId { get; set; }
        public string Kelurahan { get; set; }
        public string KodePos { get; set; }
        public string NoFax { get; set; }
        public string Website { get; set; }
        public string NamaUnit { get; set; }
        public int? SektorEkonomi10Id { get; set; }
        public string SektorEkonomi10Name { get; set; }
        public int? SektorEkonomi20Id { get; set; }
        public string SektorEkonomi20Name { get; set; }
        public int? SubSektorEkonomiId { get; set; }
        public string SubSektorEkonomiName { get; set; }
        public int? SektorPrioritasId { get; set; }
        public string SektorPrioritasName { get; set; }
        public string BidangUsahaName { get; set; }
        public int? BidangUsahaId { get; set; }
        public int? SektorIndustriId { get; set; }
        public string SektorIndustriName { get; set; }
        public string Keterangan { get; set; }
        public string AlasanDiproses { get; set; }
        public string TempatDiproses { get; set; }
        public DateTime? TanggalDiproses { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
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
        public int? IdKmb { get; set; }
        public int? IdCrm { get; set; }
        public int? IdSkm { get; set; }
        public int? FlowStatusId { get; set; }
        public string FlowStatusName { get; set; }
        public DateTime? Schedule { get; set; }
        public string Nama_RM { get; set; }
        public string Nama_Unit { get; set; }
        public string MaksimumKredit { get; set; }
        public string NamaPegawai { get; set; }
        public string Npp { get; set; }
        public string NamaWilayah { get; set; }
        public string KodeUnit { get; set; }
    }
}
