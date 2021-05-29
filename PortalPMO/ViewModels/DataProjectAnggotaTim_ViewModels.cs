﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class DataProjectAnggotaTim_ViewModels
    {
        public int Id { get; set; }
        public Int64? Nomor { get; set; }

        public int? ProjectId { get; set; }
        public int? JobPositionId { get; set; }
        public int? PegawaiId { get; set; }
        public string Npp { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string TanggalTargetPenyelesaianAnggotaTim { get; set; }
        public string Keterangan { get; set; }
        public string Unit { get; set; }
        public string Sisa { get; set; }
        public string StatusDoneProject { get; set; }
        public string Warna { get; set; }

        public string TanggalPenyelesaian { get; set; }
        public int? SelisihAngka { get; set; }
        public int? JumlahHariPengerjaanAngka { get; set; }

        public string JumlahHariPengerjaan { get; set; }

        public string StatusProgress { get; set; }
        public string JobPosisi { get; set; }
        public string Telepon { get; set; }
        public string Images { get; set; }
        public string NameImages { get; set; }

        public string Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? SendAsTask { get; set; }

    }

    public class DetailProjectMember_ViewModels
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
        public string TanggalPenyelesaian { get; set; }
        public string KeteranganPenyelesaian { get; set; }
        public string CatatanPegawai { get; set; }
        public int? SendAsTask { get; set; }
        public bool IsDone { get; set; }
        public string Periode { get; set; }
        public decimal PresentasePenyelesaian { get; set; }
        public string Selisih { get; set; }
        public int? SelisihAngka { get; set; }

        public string Status { get; set; }
        public string Warna { get; set; }

        public int? JumlahHariPengerjaanAngka { get; set; }

        public string JumlahHariPengerjaan { get; set; }

        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class BerandaDetailProjectMember_ViewModels
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

    public class ProgressKerjaMember_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? ProjectMemberId { get; set; }

        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public string Tanggal { get; set; }
        public string Status { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
