using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblSoliciteLog
    {
        public int Id { get; set; }
        public string NoPengajuan { get; set; }
        public int? LeadsId { get; set; }
        public int? KategoriLeadsId { get; set; }
        public string NamaDebitur { get; set; }
        public int? BadanUsahaId { get; set; }
        public string BadanUsahaName { get; set; }
        public string NamaGroupUsaha { get; set; }
        public string NamaKeyPerson { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Cif { get; set; }
        public string Npwp { get; set; }
        public string Nik { get; set; }
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
        public string UnitCode { get; set; }
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
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? ExpDate { get; set; }
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
