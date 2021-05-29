using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblMasterStatusProject
    {
        public TblMasterStatusProject()
        {
            TblProject = new HashSet<TblProject>();
            TblProjectLogProjectStatusFormNavigation = new HashSet<TblProjectLog>();
            TblProjectLogProjectStatusToNavigation = new HashSet<TblProjectLog>();
            TblTaskPegawaiFromStatusProjectNavigation = new HashSet<TblTaskPegawai>();
            TblTaskPegawaiToStatusProjectNavigation = new HashSet<TblTaskPegawai>();
        }

        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public int? OrderBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblProject> TblProject { get; set; }
        public virtual ICollection<TblProjectLog> TblProjectLogProjectStatusFormNavigation { get; set; }
        public virtual ICollection<TblProjectLog> TblProjectLogProjectStatusToNavigation { get; set; }
        public virtual ICollection<TblTaskPegawai> TblTaskPegawaiFromStatusProjectNavigation { get; set; }
        public virtual ICollection<TblTaskPegawai> TblTaskPegawaiToStatusProjectNavigation { get; set; }
    }
}
