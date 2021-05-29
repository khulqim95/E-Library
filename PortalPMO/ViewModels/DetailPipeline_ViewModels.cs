using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoTierTemplate.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalPMO.ViewModels
{
    public class DetailPipeline_ViewModels
    {
        public int Id { get; set; }
        public string NamaDebitur { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public string Cif { get; set; }
        public string Npwp { get; set; }
        public string Alamat { get; set; }
        public string BidangUsaha { get; set; }
        public string NamaGroupUsaha { get; set; }
        public string NamaKeyPerson { get; set; }
        public string Provinsi { get; set; }
        public string Kabupaten { get; set; }
        public string Kecamatan { get; set; }
        public string Kelurahan { get; set; }
        public string KodePos { get; set; }
        public string NoFax { get; set; }
        public string Website { get; set; }
        public string NamaUnit { get; set; }
        public string SektorEkonomi10_Name { get; set; }
        public string SektorEkonomi20_Name { get; set; }
        public string SubSektorEkonomi_Name { get; set; }
        public string SektorPrioritas_Name { get; set; }
        public string SektorIndustri { get; set; } //gak ada
        public string BidangJenisUsaha { get; set; } //gak ada
        public string Keterangan { get; set; }
        public string catatan { get; set; }
        public PreScreening_ViewModels Prescreening { get; set; }
        public PersyaratanLainnya_ViewModels PrescreeningTambahan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


    }

    public class DetailFasilitas_ViewModels
    {
        public int MaksimumKredit { get; set; }
    }
}
