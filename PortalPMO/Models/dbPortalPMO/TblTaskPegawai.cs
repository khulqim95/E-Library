using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblTaskPegawai
    {
        public TblTaskPegawai()
        {
            InversePegawaiIdFromNavigation = new HashSet<TblTaskPegawai>();
        }

        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string CatatanFrom { get; set; }
        public int? PegawaiIdFrom { get; set; }
        public int? PegawaiIdTo { get; set; }
        public int? FromStatusProject { get; set; }
        public int? ToStatusProject { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblMasterStatusProject FromStatusProjectNavigation { get; set; }
        public virtual TblTaskPegawai PegawaiIdFromNavigation { get; set; }
        public virtual TblProject Project { get; set; }
        public virtual TblMasterStatusProject ToStatusProjectNavigation { get; set; }
        public virtual ICollection<TblTaskPegawai> InversePegawaiIdFromNavigation { get; set; }
    }
}
