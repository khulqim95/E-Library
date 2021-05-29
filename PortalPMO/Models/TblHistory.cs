using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblHistory
    {
        public int Id { get; set; }
        public int InfoDebiturId { get; set; }
        public string NamaDebitur { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Cif { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public int? UpdatedById { get; set; }
        public string UpdateByName { get; set; }
    }
}
