using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortalPMO.ViewModels
{
    public class Login_ViewModels
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DetailLogin_ViewModels
    {
        public string Pegawai_Id { get; set; }
        public string Npp { get; set; }
        public string Nama_Pegawai { get; set; }
        public string Unit_Id { get; set; }
        public string Nama_Unit { get; set; }
        public string Jenis_Kelamin { get; set; }
        public string Role_Id { get; set; }
        public string Role_Unit_Id { get; set; }
        public string Role_Nama_Unit { get; set; }
        public string User_Id { get; set; }
        public string Nama_Role { get; set; }
        public string Status_Role { get; set; }
        public string User_Role_Id { get; set; }
        public string Wilayah_Id { get; set; }

        public string Images_User { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? LDAPLogin { get; set; }

    }

    public class Profile_ViewModels
    {
        public int? Pegawai_Id { get; set; }
        public string Npp { get; set; }
        public string Nama_Pegawai { get; set; }
        public int? Unit_Id { get; set; }
        public string Nama_Unit { get; set; }
        public string Jenis_Kelamin { get; set; }
        public int? Role_Id { get; set; }
        public int? Role_Unit_Id { get; set; }
        public string Role_Nama_Unit { get; set; }
        public int? User_Id { get; set; }
        public string Nama_Role { get; set; }
        public string Last_Active { get; set; }

        public string Status_Role { get; set; }
        public int? User_Role_Id { get; set; }
        public string Images { get; set; }
        public string ImagesFullPath { get; set; }
        public string NameImages { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? LDAPLogin { get; set; }

    }

    public class ChangesPassword {
        public int? PegawaiId { get; set; }
        public string PasswordLama { get; set; }
        public string PasswordBaru { get; set; }
        public string ConfirmPasswordBaru { get; set; }

    }

    public class UploadFile {
        public IFormFile file { get; set; }
    }
}
