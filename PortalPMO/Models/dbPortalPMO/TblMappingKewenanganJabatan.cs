using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblMappingKewenanganJabatan
    {
        public int Id { get; set; }
        public int? JabatanId { get; set; }
        public int? RoleId { get; set; }
        public string Keterangan { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
