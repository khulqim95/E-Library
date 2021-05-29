using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblDeskripsiKredit
    {
        public int Id { get; set; }
        public int? KategoriLeadsId { get; set; }
        public int? InfoDebiturId { get; set; }
        public int? SoliciteId { get; set; }
        public int? SkmId { get; set; }
        public string SkmName { get; set; }
        public string Deskripsi { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public int? UpdatedById { get; set; }
        public string UpdatedByName { get; set; }
        public int? DeletedById { get; set; }
        public string DeletedByName { get; set; }
        public bool? IsDelete { get; set; }
    }
}
