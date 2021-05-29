using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class MasterDataUnit_ViewModels
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public int? Parent_Id { get; set; }
        public string Parent_Name { get; set; }
        public string Wilayah_Divisi { get; set; }

        public string Short_Name { get; set; }
        public int? Type_Unit_Id { get; set; }
        public string Type_Unit_Name { get; set; }
        public string Kode_Unit { get; set; }
        public string Nama_Unit { get; set; }
        public string Alamat { get; set; }
        public string No_Telepon { get; set; }
        public string No_Fax { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public MasterDataUnit_ViewModels()
        {
            //baru
            IsActive = true;
        }
    }
}
