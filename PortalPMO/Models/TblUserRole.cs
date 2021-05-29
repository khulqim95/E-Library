using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblUserRole
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
