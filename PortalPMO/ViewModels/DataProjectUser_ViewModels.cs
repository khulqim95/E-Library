using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortalPMO.ViewModels
{
    public class DataProjectUser_ViewModels
    {
        public int? ProjectId { get; set; }
        public Int64? Nomor { get; set; }

        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public string Client { get; set; }

        public string NppPic { get; set; }
        public string NamaPic { get; set; }
        public string Email { get; set; }
        public string NoHp { get; set; }
        public string Keterangan { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
