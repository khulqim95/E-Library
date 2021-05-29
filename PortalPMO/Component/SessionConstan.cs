using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PortalPMO.Controllers;
using PortalPMO.Models;

namespace PortalPMO.Component
{
    public class SessionConstan
    {
        public static string Session_Id = "_Session_Id";
        public static string Session_User_Id = "_Session_User_Id";
        public const string Session_Nama_Pegawai = "_Session_Nama_Pegawai";
        public const string Session_NPP_Pegawai = "_Session_NPP_Pegawai";
        public const string Session_Pegawai_Id = "_Session_Pegawai_Id";
        public const string Session_Nama_Unit = "_Session_Nama_Unit";
        public const string Session_Unit_Id = "_Session_Unit_Id";
        public const string Session_Role_Id = "_Session_role_Id";
        public const string Session_Role_Unit_Id = "_Session_Role_Unit_Id";
        public const string Session_Nama_Role = "_Session_Nama_Role";
        public const string Session_Role_Nama_Unit = "_Session_Role_Nama_Unit";
        public const string Session_Images_User = "_Session_Images_User";
        public const string Session_Status_Role = "_Session_Status_Role";
        public const string Session_User_Role_Id = "_Session_User_Role_Id";
        public const string Session_Menu = "_Session_Menu";
        public const string Session_Wilayah_Id = "_Session_Wilayah_Id";


        public static string Sess_Nama_Pegawai;
        public static string Sess_Pegawai_Id;
        public static string Sess_Unit_Id;
        public static string Sess_Nama_Unit;
        public static string Sess_Nama_Id;
        public static string Sess_User_Role_Id;
        public static string Sess_Role_Id;
        public static string Sess_Nama_Role;
        public static string Sess_Role_Nama_Unit;
        public static string Sess_Images_User;
        public static string Sess_Status_Role;
        public static string Sess_Menu;
        public static string CurrentPath;
        public static string Sess_Wilayah_Id;

    }
}
