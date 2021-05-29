using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblMasterClient
    {
        public TblMasterClient()
        {
            TblProjectUser = new HashSet<TblProjectUser>();
        }

        public int Id { get; set; }
        public int? TypeClientId { get; set; }
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

        public virtual TblMasterTypeClient TypeClient { get; set; }
        public virtual ICollection<TblProjectUser> TblProjectUser { get; set; }
    }
}
