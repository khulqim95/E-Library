using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectMember
    {
        public TblProjectMember()
        {
            TblProjectMemberProgressKerja = new HashSet<TblProjectMemberProgressKerja>();
        }

        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? JobPositionId { get; set; }
        public int? PegawaiId { get; set; }
        public int? UnitPegawaiId { get; set; }
        public string Keterangan { get; set; }
        public int? StatusProgressId { get; set; }
        public string CatatanPegawai { get; set; }
        public int? SendAsTask { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsDone { get; set; }
        public DateTime? TanggalPenyelesaian { get; set; }
        public string KeteranganPenyelesaian { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblMasterJobPosition JobPosition { get; set; }
        public virtual TblPegawai Pegawai { get; set; }
        public virtual TblProject Project { get; set; }
        public virtual ICollection<TblProjectMemberProgressKerja> TblProjectMemberProgressKerja { get; set; }
    }
}
