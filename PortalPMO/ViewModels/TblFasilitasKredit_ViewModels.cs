using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalPMO.ViewModels
{
    public partial class TblFasilitasKredit_ViewModels
    {
        public int Id { get; set; }
        public int? IdDebitur { get; set; }
        public int? IdSolicite { get; set; }
        public int? KategoriLeadsId { get; set; }
        public int? DeskripsiKreditId { get; set; }
        public int? JenisPengajuanId { get; set; }
        public string JenisPengajuanName { get; set; }
        public int? TypeKreditId { get; set; }
        public string TypeKreditName { get; set; }
        public int? IdTingkatKomite { get; set; }
        public string TingkatKomite { get; set; }
        public int? IdJenisFasilitas { get; set; }
        public string JenisFasilitas { get; set; }
        public int? IdValuta { get; set; }
        public string Valuta { get; set; }
        public string MaksimumKredit { get; set; }
        public string Tujuan { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
