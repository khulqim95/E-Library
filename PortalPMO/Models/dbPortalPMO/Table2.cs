using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class Table2
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? LinkProjectId { get; set; }

        public virtual TblMasterProject Project { get; set; }
    }
}
