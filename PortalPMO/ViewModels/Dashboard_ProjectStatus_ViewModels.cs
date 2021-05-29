using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{

    public class Dashboard_ViewModels
    {
        public List<Dashboard_ProjectStatus_ViewModels> data { get; set; }
    }

    public class Dashboard_ProjectStatus_ViewModels
    {
        public int? Id { get; set; }
        public string Nama { get; set; }
        public int? Jumlah { get; set; }
        //public int? Total { get; set; }
        public float Presentase { get; set; }


    }

    public class DashboardProject_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? JobPositionId { get; set; }
        public int? PegawaiId { get; set; }
        public string NamaProject { get; set; }
        public string DetailRequirment { get; set; }

        public string ProjectNo { get; set; }
        public string JobPosition { get; set; }
        public string NamaPegawai { get; set; }
        public string NppPegawai { get; set; }
        public string NamaUnit { get; set; }
        public string StatusProject { get; set; }
        public string Keterangan { get; set; }
        public string TanggalProject { get; set; }
        public string TimelinePengerjaan { get; set; }

        public string TanggalPenyelesaian { get; set; }
        public string KeteranganPenyelesaian { get; set; }
        public string CatatanPegawai { get; set; }
        public string Warna { get; set; }

        public int? SendAsTask { get; set; }
        public bool IsDone { get; set; }
        public string Periode { get; set; }
        public decimal PresentasePenyelesaian { get; set; }
        public int? Selisih { get; set; }
        public int? JumlahHariPengerjaan { get; set; }
        public string ProjectStatus { get; set; }
        public string CloseOpen { get; set; }

        public string Status { get; set; }

        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
