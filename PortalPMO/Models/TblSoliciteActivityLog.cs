using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblSoliciteActivityLog
    {
        public int Id { get; set; }
        public string NoPengajuan { get; set; }
        public int? SoliciteId { get; set; }
        public int? ActivityType { get; set; }
        public string Title { get; set; }
        public DateTime? TanggalActivity { get; set; }
        public TimeSpan? WaktuCall { get; set; }
        public int? KategoriId { get; set; }
        public string KategoriName { get; set; }
        public string Keterangan { get; set; }
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
