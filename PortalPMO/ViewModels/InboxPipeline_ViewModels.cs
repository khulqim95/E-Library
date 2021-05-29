using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoTierTemplate.ViewModels
{
    public class InboxPipeline_ViewModels
    {
        public int Id { get; set; }
        public int LeadsId { get; set; }
        public int ProspekId { get; set; }
        public int KategoriLeadsId { get; set; }
        public Int64 Number { get; set; }
        public string NamaRM { get; set; }
        public string UnitPengusul { get; set; }
        public string NamaDebitur { get; set; }
        public string MaksimumKredit { get; set; }
        public string NoPengajuan { get; set; }
    }

    public class Checking_ViewModels
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public bool ChecklistDukcapil { get; set; }
        public bool ChecklistNPWP { get; set; }
        public bool ChecklistSLIK { get; set; }
        public bool ChecklistDHN { get; set; }
        public bool ChecklistOSS { get; set; }
    }

    public class Disclaimer_ViewModels
    {
        public int Id { get; set; }
        public int ProspekId { get; set; }
        public Int64 Number { get; set; }
        public string Deskripsi { get; set; }
        public string Soal { get; set; }
    }

    public class File_ViewModels
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public string FileName { get; set; }
        public string DownloadPath { get; set; }
    }

    public class RiwayatKomentarr_ViewModels
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public string ActionName { get; set; }
        public string Komentar { get; set; }
        public string Role { get; set; }
    }

    public class Tracking_ViewModels
    {
        public int Id { get; set; }
        public Int64 Number { get; set; }
        public string NoPengajuan { get; set; }
        public string Cif { get; set; }
        public string NamaDebitur { get; set; }
        public int Step { get; set; }
        public int PK_Step { get; set; }
        public string StartStepTime { get; set; }
        public string EndStepTime { get; set; }
        //public string CreatedTime { get; set; }
        //public string ActionName { get; set; }
        //public string Komentar { get; set; }
        public string Alasan { get; set; }
        public string NamaRM { get; set; }
        public string Status { get; set; }
        //public string UnitRM { get; set; }
        //public string RolePengirim { get; set; }
    }
}
