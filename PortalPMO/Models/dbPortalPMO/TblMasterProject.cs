using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblMasterProject
    {
        public TblMasterProject()
        {
            TblProjectMember = new HashSet<TblProjectMember>();
            TblProjectRelasi = new HashSet<TblProjectRelasi>();
        }

        public int Id { get; set; }
        public int? KategoriProjectId { get; set; }
        public int? SkorProjectId { get; set; }
        public string LinkProjectId { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string NoMemo { get; set; }
        public DateTime? TanggalMemo { get; set; }
        public string NoDrf { get; set; }
        public DateTime? TanggalDrf { get; set; }
        public DateTime? TanggalDisposisi { get; set; }
        public DateTime? TanggakKlarifikasi { get; set; }
        public string Keterangan { get; set; }
        public int? OrderBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblMasterKategoriProject KategoriProject { get; set; }
        public virtual TblMasterSkorProject SkorProject { get; set; }
        public virtual ICollection<TblProjectMember> TblProjectMember { get; set; }
        public virtual ICollection<TblProjectRelasi> TblProjectRelasi { get; set; }
    }
}
