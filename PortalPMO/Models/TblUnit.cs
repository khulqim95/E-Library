using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblUnit
    {
        public TblUnit()
        {
            TblUserSession = new HashSet<TblUserSession>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string KodeParentUnit { get; set; }
        public int? WilayahId { get; set; }
        public string KodeWilayah { get; set; }
        public int? DivisiId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string KodeOutlet { get; set; }
        public string KodeUnitVersiPms { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telepon { get; set; }
        public string PostalCode { get; set; }
        public string Dati2 { get; set; }
        public string Province { get; set; }
        public string NameVerEis { get; set; }
        public string KodeEis { get; set; }
        public string Rbb { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Kecamatan { get; set; }
        public string Kelurahan { get; set; }
        public string IbuKota { get; set; }
        public string IsJabodetabek { get; set; }
        public string Pulau { get; set; }
        public string IsPulauJawa { get; set; }
        public string Fax { get; set; }
        public string Kelas { get; set; }
        public string KodeBi { get; set; }
        public string StatusOutlet { get; set; }
        public string Npwp { get; set; }
        public string OldAddress { get; set; }
        public string ZonaBi { get; set; }
        public string TypeInAlfabeth { get; set; }
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

        public virtual ICollection<TblUserSession> TblUserSession { get; set; }
    }
}
