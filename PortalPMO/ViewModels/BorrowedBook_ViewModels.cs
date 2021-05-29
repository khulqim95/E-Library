using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class BorrowedBook_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdBook { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string BorrowDate { get; set; }
        public string FinishDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsLate { get; set; }
        public bool? IsActive { get; set; }
    }
}
