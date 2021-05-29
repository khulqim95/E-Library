using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblUnitLama
    {
        public TblUnitLama()
        {
            InverseParent = new HashSet<TblUnitLama>();
            TblPegawaiLama = new HashSet<TblPegawaiLama>();
            TblRolePegawai = new HashSet<TblRolePegawai>();
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
        public string NamaWilayah { get; set; }

        public virtual TblUnitLama Parent { get; set; }
        public virtual ICollection<TblUnitLama> InverseParent { get; set; }
        public virtual ICollection<TblPegawaiLama> TblPegawaiLama { get; set; }
        public virtual ICollection<TblRolePegawai> TblRolePegawai { get; set; }
    }
}
