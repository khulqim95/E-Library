using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwoTierTemplate.Models
{
    public partial class dbPipelineContext : DbContext
    {
        public dbPipelineContext()
        {
        }

        public dbPipelineContext(DbContextOptions<dbPipelineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MasterKecamatan> MasterKecamatan { get; set; }
        public virtual DbSet<MasterKelurahan> MasterKelurahan { get; set; }
        public virtual DbSet<MasterKotaKabupaten> MasterKotaKabupaten { get; set; }
        public virtual DbSet<MasterPropinsi> MasterPropinsi { get; set; }
        public virtual DbSet<MjwtRepository> MjwtRepository { get; set; }
        public virtual DbSet<Navigation> Navigation { get; set; }
        public virtual DbSet<NavigationAssignment> NavigationAssignment { get; set; }
        public virtual DbSet<Propinsi> Propinsi { get; set; }
        public virtual DbSet<TblAssignPrescreening> TblAssignPrescreening { get; set; }
        public virtual DbSet<TblCeklistDokumen> TblCeklistDokumen { get; set; }
        public virtual DbSet<TblChecklistChecking> TblChecklistChecking { get; set; }
        public virtual DbSet<TblChecklistDisclaimer> TblChecklistDisclaimer { get; set; }
        public virtual DbSet<TblDeskripsiKredit> TblDeskripsiKredit { get; set; }
        public virtual DbSet<TblDokumenPresentasi> TblDokumenPresentasi { get; set; }
        public virtual DbSet<TblFasilitasKredit> TblFasilitasKredit { get; set; }
        public virtual DbSet<TblFasilitasKreditLog> TblFasilitasKreditLog { get; set; }
        public virtual DbSet<TblFileDisclaimer> TblFileDisclaimer { get; set; }
        public virtual DbSet<TblFileInfoUsahaDebitur> TblFileInfoUsahaDebitur { get; set; }
        public virtual DbSet<TblFileJaminanProspek> TblFileJaminanProspek { get; set; }
        public virtual DbSet<TblFileSoliciteActivityLog> TblFileSoliciteActivityLog { get; set; }
        public virtual DbSet<TblFlowPipeline> TblFlowPipeline { get; set; }
        public virtual DbSet<TblFlowProspek> TblFlowProspek { get; set; }
        public virtual DbSet<TblHistory> TblHistory { get; set; }
        public virtual DbSet<TblInfoDebiturCalon> TblInfoDebiturCalon { get; set; }
        public virtual DbSet<TblInfoDebiturKelolaan> TblInfoDebiturKelolaan { get; set; }
        public virtual DbSet<TblInfoDebiturKelolaanLama> TblInfoDebiturKelolaanLama { get; set; }
        public virtual DbSet<TblInfoDebiturKp> TblInfoDebiturKp { get; set; }
        public virtual DbSet<TblInformasiUsahaDebitur> TblInformasiUsahaDebitur { get; set; }
        public virtual DbSet<TblJabatanPegawai> TblJabatanPegawai { get; set; }
        public virtual DbSet<TblLogActivity> TblLogActivity { get; set; }
        public virtual DbSet<TblLogBook> TblLogBook { get; set; }
        public virtual DbSet<TblLookup> TblLookup { get; set; }
        public virtual DbSet<TblMappingKelolaanSegment> TblMappingKelolaanSegment { get; set; }
        public virtual DbSet<TblMappingKewenanganJabatan> TblMappingKewenanganJabatan { get; set; }
        public virtual DbSet<TblMasterBidangUsaha> TblMasterBidangUsaha { get; set; }
        public virtual DbSet<TblMasterIconMenu> TblMasterIconMenu { get; set; }
        public virtual DbSet<TblMasterJabatan> TblMasterJabatan { get; set; }
        public virtual DbSet<TblMasterJabatanTemp> TblMasterJabatanTemp { get; set; }
        public virtual DbSet<TblMasterKecamatan> TblMasterKecamatan { get; set; }
        public virtual DbSet<TblMasterKelurahan> TblMasterKelurahan { get; set; }
        public virtual DbSet<TblMasterKota> TblMasterKota { get; set; }
        public virtual DbSet<TblMasterPertanyaan> TblMasterPertanyaan { get; set; }
        public virtual DbSet<TblMasterProvinsi> TblMasterProvinsi { get; set; }
        public virtual DbSet<TblMasterRole> TblMasterRole { get; set; }
        public virtual DbSet<TblMasterSektorEkonomi10> TblMasterSektorEkonomi10 { get; set; }
        public virtual DbSet<TblMasterSektorEkonomi20> TblMasterSektorEkonomi20 { get; set; }
        public virtual DbSet<TblMasterSektorIndustri> TblMasterSektorIndustri { get; set; }
        public virtual DbSet<TblMasterSektorUnggulan> TblMasterSektorUnggulan { get; set; }
        public virtual DbSet<TblMasterSubSektorEkonomi> TblMasterSubSektorEkonomi { get; set; }
        public virtual DbSet<TblPegawai> TblPegawai { get; set; }
        public virtual DbSet<TblPegawaiLama> TblPegawaiLama { get; set; }
        public virtual DbSet<TblPegawaiTemp> TblPegawaiTemp { get; set; }
        public virtual DbSet<TblPengajuanKredit> TblPengajuanKredit { get; set; }
        public virtual DbSet<TblPersyaratanLainnya> TblPersyaratanLainnya { get; set; }
        public virtual DbSet<TblPipeline> TblPipeline { get; set; }
        public virtual DbSet<TblPipelineActivityLog> TblPipelineActivityLog { get; set; }
        public virtual DbSet<TblPipelinePengajuan> TblPipelinePengajuan { get; set; }
        public virtual DbSet<TblPreScreening> TblPreScreening { get; set; }
        public virtual DbSet<TblProspek> TblProspek { get; set; }
        public virtual DbSet<TblProspekLog> TblProspekLog { get; set; }
        public virtual DbSet<TblRolePegawai> TblRolePegawai { get; set; }
        public virtual DbSet<TblSolicite> TblSolicite { get; set; }
        public virtual DbSet<TblSoliciteActivityLog> TblSoliciteActivityLog { get; set; }
        public virtual DbSet<TblSoliciteLog> TblSoliciteLog { get; set; }
        public virtual DbSet<TblSystemParameter> TblSystemParameter { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }
        public virtual DbSet<TblUnitLama> TblUnitLama { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserLama> TblUserLama { get; set; }
        public virtual DbSet<TblUserLog> TblUserLog { get; set; }
        public virtual DbSet<TblUserRole> TblUserRole { get; set; }
        public virtual DbSet<TblUserSession> TblUserSession { get; set; }
        public virtual DbSet<TblBorrowedBook> TblBorrowedBook { get; set; }
        public virtual DbSet<TblMasterBook> TblMasterBook { get; set; }
        // Unable to generate entity type for table 'dbo.TBL_UNIT_TEMP'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ofadev.id;Database=dbPipeline;user=sa;password=smo4dm1n;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");


            modelBuilder.Entity<TblBorrowedBook>(entity =>
            {
                entity.ToTable("Tbl_Borrowed_Book");

                entity.Property(e => e.BorrowDate)
                    .HasColumnName("Borrow_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FinishDate)
                    .HasColumnName("Finish_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdBook).HasColumnName("Id_Book");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterBook>(entity =>
            {
                entity.ToTable("Tbl_Master_Book");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RealeaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterKecamatan>(entity =>
            {
                entity.Property(e => e.Kecamatan)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KotaId).HasColumnName("Kota_Id");
            });

            modelBuilder.Entity<MasterKelurahan>(entity =>
            {
                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.Kelurahan)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.KodePos).HasMaxLength(12);
            });

            modelBuilder.Entity<MasterKotaKabupaten>(entity =>
            {
                entity.Property(e => e.Kota)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.KotaId).HasColumnName("Kota_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");
            });

            modelBuilder.Entity<MasterPropinsi>(entity =>
            {
                entity.Property(e => e.NegaraId).HasColumnName("Negara_Id");

                entity.Property(e => e.Provinsi)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");
            });

            modelBuilder.Entity<MjwtRepository>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Clientid)
                    .HasColumnName("CLIENTID")
                    .HasMaxLength(35);

                entity.Property(e => e.Clientip)
                    .HasColumnName("CLIENTIP")
                    .HasMaxLength(35);

                entity.Property(e => e.Isstop).HasColumnName("ISSTOP");

                entity.Property(e => e.Refreshtoken).HasColumnName("REFRESHTOKEN");

                entity.Property(e => e.Tokenid)
                    .HasColumnName("TOKENID")
                    .HasMaxLength(300);

                entity.Property(e => e.Userid)
                    .HasColumnName("USERID")
                    .HasMaxLength(35);
            });

            modelBuilder.Entity<Navigation>(entity =>
            {
                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IconClass).HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentNavigationId).HasColumnName("ParentNavigation_Id");

                entity.Property(e => e.Route).HasMaxLength(255);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.ParentNavigationId)
                    .HasConstraintName("FK_dbo.Navigation_dbo.Navigation_ParentNavigation_Id");
            });

            modelBuilder.Entity<NavigationAssignment>(entity =>
            {
                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.NavigationId).HasColumnName("Navigation_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Navigation)
                    .WithMany(p => p.NavigationAssignment)
                    .HasForeignKey(d => d.NavigationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NavigationAssignment_Navigation");
            });

            modelBuilder.Entity<TblAssignPrescreening>(entity =>
            {
                entity.ToTable("Tbl_AssignPrescreening");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.JenisKomiteId).HasColumnName("JenisKomite_Id");

                entity.Property(e => e.JenisKomiteName).HasColumnName("JenisKomite_Name");

                entity.Property(e => e.ProspekId).HasColumnName("Prospek_id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCeklistDokumen>(entity =>
            {
                entity.ToTable("Tbl_Ceklist_Dokumen");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblChecklistChecking>(entity =>
            {
                entity.ToTable("Tbl_ChecklistChecking");

                entity.Property(e => e.ChecklistDhn).HasColumnName("ChecklistDHN");

                entity.Property(e => e.ChecklistNpwp).HasColumnName("ChecklistNPWP");

                entity.Property(e => e.ChecklistOss).HasColumnName("ChecklistOSS");

                entity.Property(e => e.ChecklistSlik).HasColumnName("ChecklistSLIK");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblChecklistDisclaimer>(entity =>
            {
                entity.ToTable("Tbl_ChecklistDisclaimer");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.SoalId).HasColumnName("soal_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDeskripsiKredit>(entity =>
            {
                entity.ToTable("Tbl_DeskripsiKredit");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.InfoDebiturId).HasColumnName("InfoDebitur_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.SkmId).HasColumnName("SKM_Id");

                entity.Property(e => e.SkmName).HasColumnName("SKM_Name");

                entity.Property(e => e.SoliciteId).HasColumnName("Solicite_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDokumenPresentasi>(entity =>
            {
                entity.ToTable("Tbl_Dokumen_Presentasi");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.FileSize).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FileType).HasMaxLength(50);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFasilitasKredit>(entity =>
            {
                entity.ToTable("Tbl_Fasilitas_Kredit");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeskripsiKreditId).HasColumnName("DeskripsiKredit_Id");

                entity.Property(e => e.FlowStatusId).HasColumnName("FlowStatus_Id");

                entity.Property(e => e.FlowStatusName).HasColumnName("FlowStatus_Name");

                entity.Property(e => e.JenisFasilitas).HasMaxLength(255);

                entity.Property(e => e.JenisPengajuanId).HasColumnName("JenisPengajuan_Id");

                entity.Property(e => e.JenisPengajuanName).HasColumnName("JenisPengajuan_Name");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.MaksimumKredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaksimumKreditIdr)
                    .HasColumnName("MaksimumKreditIDR")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TingkatKomite).HasMaxLength(255);

                entity.Property(e => e.TypeKreditId).HasColumnName("TypeKredit_Id");

                entity.Property(e => e.TypeKreditName).HasColumnName("TypeKredit_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Valuta).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFasilitasKreditLog>(entity =>
            {
                entity.ToTable("Tbl_Fasilitas_Kredit_Log");

                entity.Property(e => e.ActionId).HasColumnName("Action_Id");

                entity.Property(e => e.ActionName).HasColumnName("Action_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeskripsiKreditId).HasColumnName("DeskripsiKredit_Id");

                entity.Property(e => e.FlowStatusId).HasColumnName("FlowStatus_Id");

                entity.Property(e => e.FlowStatusName).HasColumnName("FlowStatus_Name");

                entity.Property(e => e.JenisFasilitas).HasMaxLength(255);

                entity.Property(e => e.JenisPengajuanId).HasColumnName("JenisPengajuan_Id");

                entity.Property(e => e.JenisPengajuanName).HasColumnName("JenisPengajuan_Name");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.MaksimumKredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaksimumKreditIdr)
                    .HasColumnName("MaksimumKreditIDR")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TingkatKomite).HasMaxLength(255);

                entity.Property(e => e.TypeKreditId).HasColumnName("TypeKredit_Id");

                entity.Property(e => e.TypeKreditName).HasColumnName("TypeKredit_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Valuta).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFileDisclaimer>(entity =>
            {
                entity.ToTable("Tbl_FileDisclaimer");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.ProspekId).HasColumnName("Prospek_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFileInfoUsahaDebitur>(entity =>
            {
                entity.ToTable("tbl_FileInfoUsahaDebitur");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFileJaminanProspek>(entity =>
            {
                entity.ToTable("tbl_FileJaminanProspek");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KategoriFileId).HasColumnName("kategoriFileId");

                entity.Property(e => e.ProspekId).HasColumnName("Prospek_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFileSoliciteActivityLog>(entity =>
            {
                entity.ToTable("tbl_FileSoliciteActivityLog");

                entity.Property(e => e.ActivityId).HasColumnName("Activity_id");

                entity.Property(e => e.AttchTypeId).HasColumnName("AttchType_Id");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFlowPipeline>(entity =>
            {
                entity.ToTable("Tbl_Flow_Pipeline");

                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Komentar).HasColumnType("text");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFlowProspek>(entity =>
            {
                entity.ToTable("Tbl_Flow_Prospek");

                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime)
                    .HasColumnName("UpdatedTIme")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblHistory>(entity =>
            {
                entity.ToTable("Tbl_History");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.InfoDebiturId).HasColumnName("InfoDebitur_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.UpdateByName).HasColumnName("UpdateBy_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblInfoDebiturCalon>(entity =>
            {
                entity.ToTable("Tbl_InfoDebitur_Calon");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblInfoDebiturKelolaan>(entity =>
            {
                entity.ToTable("Tbl_InfoDebitur_Kelolaan");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.NppPengelola).HasMaxLength(100);

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.PengelolaId).HasColumnName("Pengelola_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblInfoDebiturKelolaanLama>(entity =>
            {
                entity.ToTable("Tbl_InfoDebitur_Kelolaan_Lama");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.NppPengelola).HasMaxLength(100);

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.PengelolaId).HasColumnName("Pengelola_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblInfoDebiturKp>(entity =>
            {
                entity.ToTable("Tbl_InfoDebitur_KP");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.NppPengelola).HasMaxLength(100);

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.PengelolaId).HasColumnName("Pengelola_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblInformasiUsahaDebitur>(entity =>
            {
                entity.ToTable("Tbl_InformasiUsaha_Debitur");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.SektorEkonomiBiId).HasColumnName("SektorEkonomiBI_Id");

                entity.Property(e => e.SektorEkonomiBiName).HasColumnName("SektorEkonomiBI_Name");

                entity.Property(e => e.SubSektorEkonomiBiId).HasColumnName("SubSektorEkonomiBI_Id");

                entity.Property(e => e.SubSektorEkonomiBiName).HasColumnName("SubSektorEkonomiBI_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblJabatanPegawai>(entity =>
            {
                entity.ToTable("Tbl_Jabatan_Pegawai");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.JabatanId).HasColumnName("Jabatan_Id");

                entity.Property(e => e.PegawaiId).HasColumnName("Pegawai_Id");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblLogActivity>(entity =>
            {
                entity.ToTable("Tbl_LogActivity");

                entity.Property(e => e.ActionTime).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(150);

                entity.Property(e => e.Npp).HasMaxLength(150);

                entity.Property(e => e.Os).HasColumnName("OS");
            });

            modelBuilder.Entity<TblLogBook>(entity =>
            {
                entity.ToTable("Tbl_Log_Book");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Nik)
                    .HasColumnName("NIK")
                    .HasMaxLength(50);

                entity.Property(e => e.Npp).HasMaxLength(50);
            });

            modelBuilder.Entity<TblLookup>(entity =>
            {
                entity.ToTable("Tbl_Lookup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMappingKelolaanSegment>(entity =>
            {
                entity.ToTable("Tbl_MappingKelolaanSegment");
            });

            modelBuilder.Entity<TblMappingKewenanganJabatan>(entity =>
            {
                entity.ToTable("Tbl_Mapping_Kewenangan_Jabatan");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterBidangUsaha>(entity =>
            {
                entity.ToTable("Tbl_MasterBidangUsaha");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");
            });

            modelBuilder.Entity<TblMasterIconMenu>(entity =>
            {
                entity.ToTable("Tbl_MasterIconMenu");

                entity.Property(e => e.CeratedById).HasColumnName("CeratedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasMaxLength(150);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterJabatan>(entity =>
            {
                entity.ToTable("Tbl_Master_Jabatan");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(250);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterJabatanTemp>(entity =>
            {
                entity.ToTable("Tbl_Master_JabatanTEMP");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(250);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterKecamatan>(entity =>
            {
                entity.ToTable("Tbl_MasterKecamatan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Kecamatan).HasMaxLength(100);

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KotaId).HasColumnName("Kota_Id");
            });

            modelBuilder.Entity<TblMasterKelurahan>(entity =>
            {
                entity.ToTable("Tbl_MasterKelurahan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.Kelurahan).HasMaxLength(100);

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");
            });

            modelBuilder.Entity<TblMasterKota>(entity =>
            {
                entity.ToTable("Tbl_MasterKota");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Kota).HasMaxLength(100);

                entity.Property(e => e.KotaId).HasColumnName("Kota_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");
            });

            modelBuilder.Entity<TblMasterPertanyaan>(entity =>
            {
                entity.ToTable("Tbl_MasterPertanyaan");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterProvinsi>(entity =>
            {
                entity.ToTable("Tbl_MasterProvinsi");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NegaraId).HasColumnName("Negara_Id");

                entity.Property(e => e.Provinsi).HasMaxLength(100);

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");
            });

            modelBuilder.Entity<TblMasterRole>(entity =>
            {
                entity.ToTable("Tbl_Master_Role");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nama).HasMaxLength(150);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterSektorEkonomi10>(entity =>
            {
                entity.ToTable("Tbl_MasterSektorEkonomi10");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SektorEkonomiId).HasColumnName("SektorEkonomi_Id");

                entity.Property(e => e.SektorEkonomiName)
                    .HasColumnName("SektorEkonomi_Name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblMasterSektorEkonomi20>(entity =>
            {
                entity.ToTable("Tbl_MasterSektorEkonomi20");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name)
                    .HasColumnName("SektorEkonomi20_Name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblMasterSektorIndustri>(entity =>
            {
                entity.ToTable("Tbl_MasterSektorIndustri");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_Name");

                entity.Property(e => e.SektorUnggulanId).HasColumnName("SektorUnggulan_Id");
            });

            modelBuilder.Entity<TblMasterSektorUnggulan>(entity =>
            {
                entity.ToTable("Tbl_MasterSektorUnggulan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SektorUnggulanId).HasColumnName("SektorUnggulan_Id");

                entity.Property(e => e.SektorUnggulanName)
                    .HasColumnName("SektorUnggulan_Name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblMasterSubSektorEkonomi>(entity =>
            {
                entity.ToTable("Tbl_MasterSubSektorEkonomi");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SektorEkonomiId).HasColumnName("SektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");
            });

            modelBuilder.Entity<TblPegawai>(entity =>
            {
                entity.ToTable("Tbl_Pegawai");

                entity.Property(e => e.Alamat).HasMaxLength(150);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeleteById).HasColumnName("DeleteBy_Id");

                entity.Property(e => e.DeleteDate)
                    .HasColumnName("Delete_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdJenisKelamin).HasColumnName("Id_JenisKelamin");

                entity.Property(e => e.JabatanId).HasColumnName("Jabatan_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.Lastlogin).HasColumnType("datetime");

                entity.Property(e => e.Ldaplogin).HasColumnName("LDAPLogin");

                entity.Property(e => e.Nama).HasMaxLength(300);

                entity.Property(e => e.NoHp)
                    .HasColumnName("No_HP")
                    .HasMaxLength(25);

                entity.Property(e => e.Npp).HasMaxLength(80);

                entity.Property(e => e.TanggalLahir)
                    .HasColumnName("Tanggal_Lahir")
                    .HasColumnType("date");

                entity.Property(e => e.TempatLahir)
                    .HasColumnName("Tempat_Lahir")
                    .HasMaxLength(150);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("Updated_Date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPegawaiLama>(entity =>
            {
                entity.ToTable("Tbl_Pegawai_Lama");

                entity.Property(e => e.Alamat).HasMaxLength(150);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeleteById).HasColumnName("DeleteBy_Id");

                entity.Property(e => e.DeleteDate)
                    .HasColumnName("Delete_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdJenisKelamin).HasColumnName("Id_JenisKelamin");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lastlogin).HasColumnType("datetime");

                entity.Property(e => e.Ldaplogin).HasColumnName("LDAPLogin");

                entity.Property(e => e.Nama).HasMaxLength(300);

                entity.Property(e => e.NoHp)
                    .HasColumnName("No_HP")
                    .HasMaxLength(25);

                entity.Property(e => e.Npp).HasMaxLength(80);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.TanggalLahir)
                    .HasColumnName("Tanggal_Lahir")
                    .HasColumnType("date");

                entity.Property(e => e.TempatLahir)
                    .HasColumnName("Tempat_Lahir")
                    .HasMaxLength(150);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("Updated_Date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblPegawaiLama)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_Pegawai_Tbl_Master_Role");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TblPegawaiLama)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Tbl_Pegawai_Tbl_Unit");
            });

            modelBuilder.Entity<TblPegawaiTemp>(entity =>
            {
                entity.ToTable("Tbl_Pegawai_Temp");

                entity.Property(e => e.Alamat).HasMaxLength(150);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeleteById).HasColumnName("DeleteBy_Id");

                entity.Property(e => e.DeleteDate)
                    .HasColumnName("Delete_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdJenisKelamin).HasColumnName("Id_JenisKelamin");

                entity.Property(e => e.JabatanId).HasColumnName("Jabatan_Id");

                entity.Property(e => e.Lastlogin).HasColumnType("datetime");

                entity.Property(e => e.Ldaplogin).HasColumnName("LDAPLogin");

                entity.Property(e => e.Nama).HasMaxLength(300);

                entity.Property(e => e.NoHp)
                    .HasColumnName("No_HP")
                    .HasMaxLength(25);

                entity.Property(e => e.Npp).HasMaxLength(80);

                entity.Property(e => e.TanggalLahir)
                    .HasColumnName("Tanggal_Lahir")
                    .HasColumnType("date");

                entity.Property(e => e.TempatLahir)
                    .HasColumnName("Tempat_Lahir")
                    .HasMaxLength(150);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("Updated_Date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPengajuanKredit>(entity =>
            {
                entity.ToTable("Tbl_PengajuanKredit");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CcyId).HasColumnName("Ccy_Id");

                entity.Property(e => e.CcyName).HasColumnName("Ccy_Name");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.DeskripsiKreditId).HasColumnName("DeskripsiKredit_Id");

                entity.Property(e => e.JenisKreditId).HasColumnName("JenisKredit_Id");

                entity.Property(e => e.JenisKreditName).HasColumnName("JenisKredit_Name");

                entity.Property(e => e.PengajuanKreditId).HasColumnName("PengajuanKredit_Id");

                entity.Property(e => e.PengajuanKreditName).HasColumnName("PengajuanKredit_Name");

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.Property(e => e.TypeName).HasColumnName("Type_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPersyaratanLainnya>(entity =>
            {
                entity.ToTable("Tbl_PersyaratanLainnya");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPipeline>(entity =>
            {
                entity.ToTable("Tbl_Pipeline");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.LeadsId).HasColumnName("Leads_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProspekId).HasColumnName("Prospek_Id");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TanggalDiproses).HasColumnType("datetime");

                entity.Property(e => e.TempatDiproses).HasMaxLength(150);

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPipelineActivityLog>(entity =>
            {
                entity.ToTable("Tbl_PipelineActivityLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cif).HasMaxLength(25);

                entity.Property(e => e.CreatedByUnitCode).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndStepTime).HasColumnType("datetime");

                entity.Property(e => e.NoPengajuan).HasMaxLength(50);

                entity.Property(e => e.PkStep).HasColumnName("PK_Step");

                entity.Property(e => e.RmId).HasColumnName("RM_Id");

                entity.Property(e => e.StartStepTime).HasColumnType("datetime");

                entity.Property(e => e.UnitRmId).HasColumnName("UnitRM_Id");
            });

            modelBuilder.Entity<TblPipelinePengajuan>(entity =>
            {
                entity.ToTable("Tbl_Pipeline_Pengajuan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cif).HasMaxLength(25);

                entity.Property(e => e.CreatedByUnitCode).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndStepTime).HasColumnType("datetime");

                entity.Property(e => e.NoPengajuan).HasMaxLength(50);

                entity.Property(e => e.PkStep).HasColumnName("PK_Step");

                entity.Property(e => e.RmId).HasColumnName("RM_Id");

                entity.Property(e => e.StartStepTime).HasColumnType("datetime");

                entity.Property(e => e.UnitRmId).HasColumnName("UnitRM_Id");
            });

            modelBuilder.Entity<TblPreScreening>(entity =>
            {
                entity.ToTable("Tbl_PreScreening");

                entity.Property(e => e.Alasan).IsUnicode(false);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Keputusan).IsUnicode(false);

                entity.Property(e => e.Menyetujui).IsUnicode(false);

                entity.Property(e => e.Pengusul).IsUnicode(false);

                entity.Property(e => e.Pkder).HasColumnName("PKDer");

                entity.Property(e => e.PklabaBersih).HasColumnName("PKLabaBersih");

                entity.Property(e => e.PkmodalAwal).HasColumnName("PKModalAwal");

                entity.Property(e => e.Pkpertumbuhan).HasColumnName("PKPertumbuhan");

                entity.Property(e => e.ProspekId).HasColumnName("Prospek_Id");

                entity.Property(e => e.TrkeyPerson).HasColumnName("TRKeyPerson");

                entity.Property(e => e.TrkompetensiManajemen).HasColumnName("TRKompetensiManajemen");

                entity.Property(e => e.TrpengalamanKeyPerson).HasColumnName("TRPengalamanKeyPerson");

                entity.Property(e => e.Trperusahaan).HasColumnName("TRPerusahaan");

                entity.Property(e => e.TrperusahaanBeroperasi).HasColumnName("TRPerusahaanBeroperasi");

                entity.Property(e => e.TrstrukturKepemilikan).HasColumnName("TRStrukturKepemilikan");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProspek>(entity =>
            {
                entity.ToTable("Tbl_Prospek");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.FlowStatusName).HasMaxLength(255);

                entity.Property(e => e.IdCrm).HasColumnName("IdCRM");

                entity.Property(e => e.IdKmb).HasColumnName("IdKMB");

                entity.Property(e => e.IdSkm).HasColumnName("IdSKM");

                entity.Property(e => e.IsApproveCrm).HasColumnName("IsApproveCRM");

                entity.Property(e => e.IsApproveKmb).HasColumnName("IsApproveKMB");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.LeadsId).HasColumnName("Leads_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.Schedule).HasColumnType("datetime");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SoliciteId).HasColumnName("Solicite_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TanggalDiproses).HasColumnType("datetime");

                entity.Property(e => e.TempatDiproses).HasMaxLength(150);

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProspekLog>(entity =>
            {
                entity.ToTable("Tbl_Prospek_Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.FlowStatusId).HasColumnName("FlowStatus_Id");

                entity.Property(e => e.FlowStatusName).HasColumnName("FlowStatus_Name");

                entity.Property(e => e.IdCrm).HasColumnName("IdCRM");

                entity.Property(e => e.IdKmb).HasColumnName("IdKMB");

                entity.Property(e => e.IdSkm).HasColumnName("IdSKM");

                entity.Property(e => e.IsApproveCrm).HasColumnName("IsApproveCRM");

                entity.Property(e => e.IsApproveKmb).HasColumnName("IsApproveKMB");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.LeadsId).HasColumnName("Leads_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.SoliciteId).HasColumnName("Solicite_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TanggalDiproses).HasColumnType("datetime");

                entity.Property(e => e.TempatDiproses).HasMaxLength(150);

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblRolePegawai>(entity =>
            {
                entity.ToTable("Tbl_Role_Pegawai");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.PegawaiId).HasColumnName("Pegawai_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Pegawai)
                    .WithMany(p => p.TblRolePegawai)
                    .HasForeignKey(d => d.PegawaiId)
                    .HasConstraintName("FK_Tbl_Role_Pegawai_Tbl_Pegawai");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblRolePegawai)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_Role_Pegawai_Tbl_Master_Role");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TblRolePegawai)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Tbl_Role_Pegawai_Tbl_Unit");
            });

            modelBuilder.Entity<TblSolicite>(entity =>
            {
                entity.ToTable("Tbl_Solicite");

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.ExpDate).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.LeadsId).HasColumnName("Leads_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSoliciteActivityLog>(entity =>
            {
                entity.ToTable("Tbl_SoliciteActivityLog");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.KategoriId).HasColumnName("Kategori_Id");

                entity.Property(e => e.KategoriName).HasColumnName("Kategori_Name");

                entity.Property(e => e.SoliciteId).HasColumnName("Solicite_Id");

                entity.Property(e => e.TanggalActivity).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSoliciteLog>(entity =>
            {
                entity.ToTable("Tbl_Solicite_Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BadanUsahaId).HasColumnName("BadanUsaha_Id");

                entity.Property(e => e.BadanUsahaName).HasColumnName("BadanUsaha_Name");

                entity.Property(e => e.BidangUsahaId).HasColumnName("BidangUsaha_Id");

                entity.Property(e => e.BidangUsahaName).HasColumnName("BidangUsaha_Name");

                entity.Property(e => e.Cif).HasColumnName("CIF");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedByName).HasColumnName("CreatedBy_Name");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedByName).HasColumnName("DeletedBy_Name");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.ExpDate).HasColumnType("datetime");

                entity.Property(e => e.KabupatenId).HasColumnName("Kabupaten_Id");

                entity.Property(e => e.KategoriLeadsId).HasColumnName("KategoriLeads_Id");

                entity.Property(e => e.KecamatanId).HasColumnName("Kecamatan_Id");

                entity.Property(e => e.KelurahanId).HasColumnName("Kelurahan_Id");

                entity.Property(e => e.LeadsId).HasColumnName("Leads_Id");

                entity.Property(e => e.Nik).HasColumnName("NIK");

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ProvinsiId).HasColumnName("Provinsi_Id");

                entity.Property(e => e.SektorEkonomi10Id).HasColumnName("SektorEkonomi10_Id");

                entity.Property(e => e.SektorEkonomi10Name).HasColumnName("SektorEkonomi10_Name");

                entity.Property(e => e.SektorEkonomi20Id).HasColumnName("SektorEkonomi20_Id");

                entity.Property(e => e.SektorEkonomi20Name).HasColumnName("SektorEkonomi20_Name");

                entity.Property(e => e.SektorIndustriId).HasColumnName("SektorIndustri_Id");

                entity.Property(e => e.SektorIndustriName).HasColumnName("SektorIndustri_name");

                entity.Property(e => e.SektorPrioritasId).HasColumnName("SektorPrioritas_Id");

                entity.Property(e => e.SektorPrioritasName).HasColumnName("SektorPrioritas_Name");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.StatusName).HasColumnName("Status_Name");

                entity.Property(e => e.SubSektorEkonomiId).HasColumnName("SubSektorEkonomi_Id");

                entity.Property(e => e.SubSektorEkonomiName).HasColumnName("SubSektorEkonomi_Name");

                entity.Property(e => e.TipeDebiturId).HasColumnName("TipeDebitur_Id");

                entity.Property(e => e.TipeDebiturName).HasColumnName("TipeDebitur_Name");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedByName).HasColumnName("UpdatedBy_Name");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSystemParameter>(entity =>
            {
                entity.ToTable("Tbl_SystemParameter");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Key).IsRequired();

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUnit>(entity =>
            {
                entity.ToTable("Tbl_Unit");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Dati2).HasMaxLength(300);

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.DivisiId).HasColumnName("Divisi_Id");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.IbuKota).HasMaxLength(300);

                entity.Property(e => e.IsJabodetabek).HasMaxLength(300);

                entity.Property(e => e.IsPulauJawa).HasMaxLength(300);

                entity.Property(e => e.Kecamatan).HasMaxLength(300);

                entity.Property(e => e.Kelas).HasMaxLength(50);

                entity.Property(e => e.Kelurahan).HasMaxLength(300);

                entity.Property(e => e.KodeBi)
                    .HasColumnName("KodeBI")
                    .HasMaxLength(50);

                entity.Property(e => e.KodeEis)
                    .HasColumnName("KodeEIS")
                    .HasMaxLength(50);

                entity.Property(e => e.KodeOutlet).HasMaxLength(100);

                entity.Property(e => e.KodeParentUnit).HasMaxLength(100);

                entity.Property(e => e.KodeRubrikDiv).HasMaxLength(50);

                entity.Property(e => e.KodeRubrikMemo).HasMaxLength(50);

                entity.Property(e => e.KodeRubrikNotin).HasMaxLength(50);

                entity.Property(e => e.KodeUnitVersiPms)
                    .HasColumnName("KodeUnit_VersiPMS")
                    .HasMaxLength(150);

                entity.Property(e => e.KodeWilayah).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasMaxLength(100);

                entity.Property(e => e.Longitude).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NameVerEis)
                    .HasColumnName("NameVerEIS")
                    .HasMaxLength(500);

                entity.Property(e => e.Npwp).HasColumnName("NPWP");

                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.Province).HasMaxLength(250);

                entity.Property(e => e.Pulau).HasMaxLength(300);

                entity.Property(e => e.Rbb)
                    .HasColumnName("RBB")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.Property(e => e.StatusOutlet).HasMaxLength(50);

                entity.Property(e => e.Telepon).HasMaxLength(50);

                entity.Property(e => e.TypeInAlfabeth).HasMaxLength(5);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.WilayahId).HasColumnName("Wilayah_Id");

                entity.Property(e => e.ZonaBi)
                    .HasColumnName("ZonaBI")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUnitLama>(entity =>
            {
                entity.ToTable("Tbl_Unit_Lama");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.DivisiId).HasColumnName("Divisi_Id");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.KodeRubrikDiv).HasMaxLength(50);

                entity.Property(e => e.KodeRubrikMemo).HasMaxLength(50);

                entity.Property(e => e.KodeRubrikNotin).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.Property(e => e.Telepon).HasMaxLength(50);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.WilayahId).HasColumnName("Wilayah_Id");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_dbo.Unit_dbo.Unit_Parent_Id");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("Tbl_User");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                //entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PegawaiId).HasColumnName("Pegawai_Id");

                //entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUserLama>(entity =>
            {
                entity.ToTable("Tbl_User_Lama");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PegawaiId).HasColumnName("Pegawai_Id");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Pegawai)
                    .WithMany(p => p.TblUserLama)
                    .HasForeignKey(d => d.PegawaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Tbl_Pegawai");
            });

            modelBuilder.Entity<TblUserLog>(entity =>
            {
                entity.ToTable("Tbl_UserLog");

                entity.Property(e => e.Browser).HasMaxLength(255);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(10);
            });

            modelBuilder.Entity<TblUserRole>(entity =>
            {
                entity.ToTable("Tbl_User_Role");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserSession>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Tbl_UserSession");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Info).HasMaxLength(255);

                entity.Property(e => e.LastActive).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasColumnName("SessionID")
                    .HasMaxLength(50);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserSession)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_UserSession_Tbl_Master_Role");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TblUserSession)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Tbl_UserSession_Tbl_Unit");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblUserSession)
                    .HasForeignKey<TblUserSession>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_UserSession_Tbl_User");
            });
        }
    }
}
