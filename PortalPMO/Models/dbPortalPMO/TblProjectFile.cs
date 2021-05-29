using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectFile
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? TypeDokumenId { get; set; }
        public string NamaFile { get; set; }
        public string FileExt { get; set; }
        public string FileType { get; set; }
        public decimal? Size { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string Keterangan { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? UploadById { get; set; }
        public int? PegawaiUploadUnitId { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TblProject Project { get; set; }
        public virtual TblMasterTypeDokumen TypeDokumen { get; set; }
    }
}
