using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectLog
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PegawaiIdFrom { get; set; }
        public int? PegawaiIdTo { get; set; }
        public int? UnitIdPegawaiIdFrom { get; set; }
        public int? UnitIdPegawaiIdTo { get; set; }
        public int? ProjectStatusForm { get; set; }
        public string ProjectStatusFormValue { get; set; }
        public int? ProjectStatusTo { get; set; }
        public string ProjectStatusToValue { get; set; }
        public int? LogActivityId { get; set; }
        public string LogActivityName { get; set; }
        public string Komentar { get; set; }
        public DateTime? Tanggal { get; set; }
        public string Keterangan { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblPegawai PegawaiIdFromNavigation { get; set; }
        public virtual TblProject Project { get; set; }
        public virtual TblMasterStatusProject ProjectStatusFormNavigation { get; set; }
        public virtual TblMasterStatusProject ProjectStatusToNavigation { get; set; }
    }
}
