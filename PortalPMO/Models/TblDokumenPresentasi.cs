using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblDokumenPresentasi
    {
        public int Id { get; set; }
        public int? IdProspek { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public decimal? FileSize { get; set; }
        public string FilePath { get; set; }
        public string DownloadPath { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
