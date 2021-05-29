using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblPegawaiTemp
    {
        public int Id { get; set; }
        public int? UnitId { get; set; }
        public int? JabatanId { get; set; }
        public int? IdJenisKelamin { get; set; }
        public string Npp { get; set; }
        public string Nama { get; set; }
        public string TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Email { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string Images { get; set; }
        public string NoHp { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int? DeleteById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Ldaplogin { get; set; }
    }
}
