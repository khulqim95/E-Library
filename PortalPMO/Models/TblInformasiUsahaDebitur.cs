using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblInformasiUsahaDebitur
    {
        public int Id { get; set; }
        public int? ProspekId { get; set; }
        public int? StatusLokasiId { get; set; }
        public string StatusLokasiName { get; set; }
        public int? LamaUsaha { get; set; }
        public int? SektorUnggulanId { get; set; }
        public string SektorUnggulanName { get; set; }
        public int? SektorIndustriId { get; set; }
        public string SektorIndustriName { get; set; }
        public int? BidangUsahaId { get; set; }
        public string BidangUsahaName { get; set; }
        public int? SektorPrioritasWilayahId { get; set; }
        public string SektorPrioritasWilayahName { get; set; }
        public int? SektorEkonomiBiId { get; set; }
        public string SektorEkonomiBiName { get; set; }
        public int? SubSektorEkonomiBiId { get; set; }
        public string SubSektorEkonomiBiName { get; set; }
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
