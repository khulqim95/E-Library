using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblFileSoliciteActivityLog
    {
        public int Id { get; set; }
        public int? ActivityId { get; set; }
        public int? AttchTypeId { get; set; }
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
