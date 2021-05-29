using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class PengaturanLookup_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Order_By { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public PengaturanLookup_ViewModels()
        {
            //baru
            IsActive = true;
        }
    }
}
