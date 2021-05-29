using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterBidangUsaha
    {
        public int Id { get; set; }
        public int? BidangUsahaId { get; set; }
        public string BidangUsahaName { get; set; }
        public int? SubSektorEkonomiId { get; set; }
    }
}
