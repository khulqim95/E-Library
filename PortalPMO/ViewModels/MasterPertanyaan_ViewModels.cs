using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class MasterPertanyaan_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Pertanyaan { get; set; }
        public int Order_By { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
