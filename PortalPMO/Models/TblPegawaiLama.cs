using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblPegawaiLama
    {
        public TblPegawaiLama()
        {
            TblRolePegawai = new HashSet<TblRolePegawai>();
            TblUserLama = new HashSet<TblUserLama>();
        }

        public int Id { get; set; }
        public int? UnitId { get; set; }
        public int? RoleId { get; set; }
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

        public virtual TblMasterRole Role { get; set; }
        public virtual TblUnitLama Unit { get; set; }
        public virtual ICollection<TblRolePegawai> TblRolePegawai { get; set; }
        public virtual ICollection<TblUserLama> TblUserLama { get; set; }
    }
}
