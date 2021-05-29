using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblFileJaminanProspek
    {
        public int Id { get; set; }
        public int? KategoriFileId { get; set; }
        public int? ProspekId { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public double? FileSize { get; set; }
        public string FullPath { get; set; }
        public string DownloadPath { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
