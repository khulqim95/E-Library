using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectLogStatus
    {
        public int Id { get; set; }
        public int? Projectid { get; set; }
        public int? ProjectStatusForm { get; set; }
        public string ProjectStatusFormValue { get; set; }
        public int? ProjectStatusTo { get; set; }
        public string ProjectStatusToValue { get; set; }
        public DateTime? Tanggal { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
