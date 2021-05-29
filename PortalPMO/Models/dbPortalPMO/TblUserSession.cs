using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblUserSession
    {
        public int UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime LastActive { get; set; }
        public string Info { get; set; }
        public int? RoleId { get; set; }
        public int? UnitId { get; set; }

        public virtual TblMasterRole Role { get; set; }
        public virtual TblUnit Unit { get; set; }
        public virtual TblUser User { get; set; }
    }
}
