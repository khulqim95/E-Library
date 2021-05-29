using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblRolePegawai
    {
        public int Id { get; set; }
        public int? PegawaiId { get; set; }
        public int? RoleId { get; set; }
        public int? UnitId { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? StatusRole { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TblPegawai Pegawai { get; set; }
        public virtual TblMasterRole Role { get; set; }
        public virtual TblUnit Unit { get; set; }
    }
}
