using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblConfigApps
    {
        public int Id { get; set; }
        public string PathFolderFile { get; set; }
        public decimal? MaxFileSize { get; set; }
        public string TypeFileUpload { get; set; }
        public string VirtualPath { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
    }
}
