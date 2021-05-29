using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProjectUser
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ClientId { get; set; }
        public string NppPic { get; set; }
        public string NamaPic { get; set; }
        public string Email { get; set; }
        public string NoHp { get; set; }
        public string Keterangan { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalSelesai { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblMasterClient Client { get; set; }
        public virtual TblProject Project { get; set; }
    }
}
