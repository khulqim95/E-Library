using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectNotes
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PegawaiId { get; set; }
        public int? UnitId { get; set; }
        public string Judul { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblProject Project { get; set; }
    }
}
