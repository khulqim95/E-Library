using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectMemberProgressKerja
    {
        public int Id { get; set; }
        public int? ProjectMemberId { get; set; }
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public DateTime? TanggalAwal { get; set; }
        public DateTime? TanggalAkhir { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblProjectMember ProjectMember { get; set; }
    }
}
