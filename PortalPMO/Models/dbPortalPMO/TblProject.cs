using System;
using System.Collections.Generic;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class TblProject
    {
        public TblProject()
        {
            TblProjectFile = new HashSet<TblProjectFile>();
            TblProjectLog = new HashSet<TblProjectLog>();
            TblProjectMember = new HashSet<TblProjectMember>();
            TblProjectNotes = new HashSet<TblProjectNotes>();
            TblProjectUser = new HashSet<TblProjectUser>();
            TblTaskPegawai = new HashSet<TblTaskPegawai>();
        }

        public int Id { get; set; }
        public int? KategoriProjectId { get; set; }
        public int? SubKategoriProjectId { get; set; }
        public int? SkorProjectId { get; set; }
        public int? KompleksitasProjectId { get; set; }
        public int? KlasifikasiProjectId { get; set; }
        public int? MandatoryId { get; set; }
        public int? PeriodeProjectId { get; set; }
        public int? NotifikasiId { get; set; }
        public int? IsPir { get; set; }
        public int? ProjectStatusId { get; set; }
        public int? CloseOpenId { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string ProjectNo { get; set; }
        public string NoMemo { get; set; }
        public DateTime? TanggalMemo { get; set; }
        public string NoDrf { get; set; }
        public DateTime? TanggalDrf { get; set; }
        public DateTime? TanggalDisposisi { get; set; }
        public DateTime? TanggalKlarifikasi { get; set; }
        public string DetailRequirment { get; set; }
        public DateTime? TanggalEstimasiMulai { get; set; }
        public DateTime? TanggalEstimasiSelesai { get; set; }
        public DateTime? TanggalEstimasiProduction { get; set; }
        public DateTime? TanggalEstimasiDevelopmentAwal { get; set; }
        public DateTime? TanggalEstimasiDevelopmentAkhir { get; set; }
        public DateTime? TanggalEstimasiTestingAwal { get; set; }
        public DateTime? TanggalEstimasiTestingAkhir { get; set; }
        public DateTime? TanggalEstimasiPilotingAwal { get; set; }
        public DateTime? TanggalEstimasiPilotingAkhir { get; set; }
        public DateTime? TanggalEstimasiPirawal { get; set; }
        public DateTime? TanggalEstimasiPirakhir { get; set; }
        public bool? IsDone { get; set; }
        public DateTime? TanggalSelesaiProject { get; set; }
        public int? OrderBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblMasterKategoriProject KategoriProject { get; set; }
        public virtual TblMasterKlasifikasiProject KlasifikasiProject { get; set; }
        public virtual TblMasterKompleksitasProject KompleksitasProject { get; set; }
        public virtual TblMasterStatusProject ProjectStatus { get; set; }
        public virtual TblMasterSkorProject SkorProject { get; set; }
        public virtual TblMasterSubKategoriProject SubKategoriProject { get; set; }
        public virtual ICollection<TblProjectFile> TblProjectFile { get; set; }
        public virtual ICollection<TblProjectLog> TblProjectLog { get; set; }
        public virtual ICollection<TblProjectMember> TblProjectMember { get; set; }
        public virtual ICollection<TblProjectNotes> TblProjectNotes { get; set; }
        public virtual ICollection<TblProjectUser> TblProjectUser { get; set; }
        public virtual ICollection<TblTaskPegawai> TblTaskPegawai { get; set; }
    }
}
