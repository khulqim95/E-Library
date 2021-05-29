using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblUnit
    {
        public TblUnit()
        {
            InverseParent = new HashSet<TblUnit>();
            TblPegawai = new HashSet<TblPegawai>();
            TblRolePegawai = new HashSet<TblRolePegawai>();
            TblUserSession = new HashSet<TblUserSession>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? WilayahId { get; set; }
        public int? DivisiId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telepon { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public string KodeRubrikDiv { get; set; }
        public string KodeRubrikMemo { get; set; }
        public string KodeRubrikNotin { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDelete { get; set; }

        public virtual TblUnit Parent { get; set; }
        public virtual ICollection<TblUnit> InverseParent { get; set; }
        public virtual ICollection<TblPegawai> TblPegawai { get; set; }
        public virtual ICollection<TblRolePegawai> TblRolePegawai { get; set; }
        public virtual ICollection<TblUserSession> TblUserSession { get; set; }
    }
}
