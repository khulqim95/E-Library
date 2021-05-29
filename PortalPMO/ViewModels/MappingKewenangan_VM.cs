using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class MappingKewenangan_VM
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public int? Jabatan_Id { get; set; }
        public string JabatanName { get; set; }
        public string Jabatan { get; set; }
        public string RoleName { get; set; }
        public string Role { get; set; }
        public string Roles { get; set; }
        public string Unit_Id { get; set; }
        public string statusJabatanId { get; set; }
        public string statusJabatanName { get; set; }
        public string Keterangan { get; set; }
        public string unitName { get; set; }
        public string CreatedTime { get; set; }
        //public DateTime? CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public SelectList listRole { get; set; }
        public SelectList listStatusRole { get; set; }
        public SelectList listUnit { get; set; }
    }
}
