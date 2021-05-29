using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblPegawai
    {
        public TblPegawai()
        {
            TblProjectLog = new HashSet<TblProjectLog>();
            TblProjectMember = new HashSet<TblProjectMember>();
            TblRolePegawai = new HashSet<TblRolePegawai>();
            TblUser = new HashSet<TblUser>();
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
        public int? JabatanId { get; set; }
        public DateTime? Lastlogin { get; set; }
        //public string Images { get; set; }
        //public string ImagesFullPath { get; set; }
        //public string NameImages { get; set; }
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
        public virtual TblUnit Unit { get; set; }
        public virtual ICollection<TblProjectLog> TblProjectLog { get; set; }
        public virtual ICollection<TblProjectMember> TblProjectMember { get; set; }
        public virtual ICollection<TblRolePegawai> TblRolePegawai { get; set; }
        public virtual ICollection<TblUser> TblUser { get; set; }
    }
}
