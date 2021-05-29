using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO { 
    public partial class TblBorrowedBook
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdBook { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsLate { get; set; }
        public bool? IsActive { get; set; }
    }
}
