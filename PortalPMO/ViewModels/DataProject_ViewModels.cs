using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortalPMO.ViewModels
{
    public class DataProject_ViewModels
    {
        public Int64 Number { get; set; }
        public int Id { get; set; }
        public int? KategoriProjectId { get; set; }
        public string KategoriProject { get; set; }
        public string NoProject { get; set; }
        public string NamaProject { get; set; }

        public int? SubKategoriProjectId { get; set; }

        public string SubKategoriProject { get; set; }
        public int? MandatoryId { get; set; }
        public string Mandatory { get; set; }
        public string PIR { get; set; }
        public int? isPIR { get; set; }

        public int? SkorProjectId { get; set; }
        public string SkorProject { get; set; }
        public int? KompleksitasProjectId { get; set; }
        public string KompleksitasProject { get; set; }
        public int? KlasifikasiProjectId { get; set; }
        public string KlasifikasiProject { get; set; }
        public string CloseOpenStatus { get; set; }
        public string PeriodeProject { get; set; }
        public int? PeriodeProjectId { get; set; }
        public int? NotifikasiId { get; set; }
        public int? SLA { get; set; }

        public string Kode { get; set; }
        public string Warna { get; set; }

        public string Nama { get; set; }
        public string NoMemo { get; set; }
        public string TanggalMemo { get; set; }
        public string KeteranganMemo { get; set; }
        public string NoDrf { get; set; }
        public string TanggalDrf { get; set; }
        public string KeteranganDrf { get; set; }
        public string TanggalSelesaiProject { get; set; }

        public string TanggalDisposisi { get; set; }
        public string TanggalKlarifikasi { get; set; }
        public string TanggalEstimasiDone { get; set; }
        public string TanggalEstimasiProject { get; set; }
        public string DetailRequirment { get; set; }
        public string TanggalEstimasiProduction { get; set; }
        public string TanggalEstimasiDevelopment { get; set; }

        public string TanggalEstimasiDevelopmentAwal { get; set; }
        public string TanggalEstimasiDevelopmentAkhir { get; set; }
        public string TanggalEstimasiTesting { get; set; }
        public string TanggalEstimasiTestingAwal { get; set; }
        public string TanggalEstimasiTestingAkhir { get; set; }
        public string TanggalEstimasiPiloting { get; set; }

        public string TanggalEstimasiPilotingAwal { get; set; }
        public string TanggalEstimasiPilotingAkhir { get; set; }
        public string TanggalEstimasiPir { get; set; }

        public string TanggalEstimasiPirawal { get; set; }
        public string TanggalEstimasiPirakhir { get; set; }
        public string KeteranganNotulen { get; set; }
        public string CatatanAssignment { get; set; }
        public string CatatanUser { get; set; }

        public string AssignTo { get; set; }
        public string AssignToJoin { get; set; }
        public int? ProjectStatusId { get; set; }
        public string ProjectStatus { get; set; }

        public int? Selisih { get; set; }
        public IFormFile FileMemo { get; set; }
        public IFormFile FileNotulen { get; set; }

        public IFormFile FileDRF { get; set; }
    }
}
