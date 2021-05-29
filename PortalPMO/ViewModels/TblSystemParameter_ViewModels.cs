using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class TblSystemParameter_ViewModels
    {
        public Int64 Number { get; set; }
        public int? Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Keterangan { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string DeletedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Status { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
