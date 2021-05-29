using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class DataProjectNotes_ViewModels
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PegawaiId { get; set; }
        public string Npp { get; set; }
        public string NamaPegawai { get; set; }
        public string Judul { get; set; }

        public string Notes { get; set; }
        public string Images { get; set; }
        public string NameImages { get; set; }
        public string CreatedDate { get; set; }
       
    }
}
