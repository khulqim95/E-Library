using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblMasterSubSektorEkonomi
    {
        public int Id { get; set; }
        public int? SubSektorEkonomiId { get; set; }
        public string SubSektorEkonomiName { get; set; }
        public int? SektorEkonomiId { get; set; }
    }
}
