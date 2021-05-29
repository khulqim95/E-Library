using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class DataProjectRelasi_ViewModels
    {
        public int Id { get; set; }

        public Int64? Nomor { get; set; }

        public int? ProjectId { get; set; }
        public int? RelasiProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string NamaProject { get; set; }
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
