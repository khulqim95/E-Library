using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblLogBook
    {
        public int Id { get; set; }
        public string Nik { get; set; }
        public DateTime? Date { get; set; }
        public string Npp { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
