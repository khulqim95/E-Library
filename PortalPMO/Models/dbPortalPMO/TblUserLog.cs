using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblUserLog
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Ipaddress { get; set; }
        public string Browser { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Data { get; set; }
    }
}
