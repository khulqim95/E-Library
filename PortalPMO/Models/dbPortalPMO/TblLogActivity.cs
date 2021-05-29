﻿using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblLogActivity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Npp { get; set; }
        public string Url { get; set; }
        public string DataLama { get; set; }
        public string DataBaru { get; set; }
        public DateTime? ActionTime { get; set; }
        public string Browser { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public string ClientInfo { get; set; }
        public string Keterangan { get; set; }
    }
}
