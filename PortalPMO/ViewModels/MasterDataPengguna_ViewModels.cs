using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class MasterDataPengguna_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public string Npp { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }

        public string Unit { get; set; }
        public int? UnitId { get; set; }
        public int? Jabatan_Id { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string PasswordBaru { get; set; }
        public string ConfirmPasswordBaru { get; set; }


        public string LastLogin { get; set; }

        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsLDAP { get; set; }

        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        //public MasterDataPengguna_ViewModels()
        //{
        //    //baru
        //    IsActive = true;
        //    IsLDAP = true;

        //}
    }
}
