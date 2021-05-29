using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PortalPMO.Models.dbPortalPMO
{
    public partial class dbPortalPMOContext : DbContext
    {
        public dbPortalPMOContext()
        {
        }

        public dbPortalPMOContext(DbContextOptions<dbPortalPMOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Navigation> Navigation { get; set; }
        public virtual DbSet<NavigationAssignment> NavigationAssignment { get; set; }
        public virtual DbSet<TblConfigApps> TblConfigApps { get; set; }
        public virtual DbSet<TblFaq> TblFaq { get; set; }
        public virtual DbSet<TblFileRepository> TblFileRepository { get; set; }
        public virtual DbSet<TblHoliday> TblHoliday { get; set; }
        public virtual DbSet<TblLogActivity> TblLogActivity { get; set; }
        public virtual DbSet<TblLogBook> TblLogBook { get; set; }
        public virtual DbSet<TblLookup> TblLookup { get; set; }
        public virtual DbSet<TblMasterClient> TblMasterClient { get; set; }
        public virtual DbSet<TblMasterIconMenu> TblMasterIconMenu { get; set; }
        public virtual DbSet<TblMasterJabatan> TblMasterJabatan { get; set; }
        public virtual DbSet<TblMasterJobPosition> TblMasterJobPosition { get; set; }
        public virtual DbSet<TblMasterKategoriKompleksitas> TblMasterKategoriKompleksitas { get; set; }
        public virtual DbSet<TblMasterKategoriProject> TblMasterKategoriProject { get; set; }
        public virtual DbSet<TblMasterKlasifikasiProject> TblMasterKlasifikasiProject { get; set; }
        public virtual DbSet<TblMasterKompleksitasProject> TblMasterKompleksitasProject { get; set; }
        public virtual DbSet<TblMasterPeriodeProject> TblMasterPeriodeProject { get; set; }
        public virtual DbSet<TblMasterRole> TblMasterRole { get; set; }
        public virtual DbSet<TblMasterSistem> TblMasterSistem { get; set; }
        public virtual DbSet<TblMasterSkorProject> TblMasterSkorProject { get; set; }
        public virtual DbSet<TblMasterStatusProject> TblMasterStatusProject { get; set; }
        public virtual DbSet<TblMasterSubKategoriProject> TblMasterSubKategoriProject { get; set; }
        public virtual DbSet<TblMasterSubSistem> TblMasterSubSistem { get; set; }
        public virtual DbSet<TblMasterTypeClient> TblMasterTypeClient { get; set; }
        public virtual DbSet<TblMasterTypeDokumen> TblMasterTypeDokumen { get; set; }
        public virtual DbSet<TblMappingKewenanganJabatan> TblMappingKewenanganJabatan { get; set; }
        public virtual DbSet<TblPegawai> TblPegawai { get; set; }
        public virtual DbSet<TblProject> TblProject { get; set; }
        public virtual DbSet<TblProjectDetailUnitRequest> TblProjectDetailUnitRequest { get; set; }
        public virtual DbSet<TblProjectFile> TblProjectFile { get; set; }
        public virtual DbSet<TblProjectLog> TblProjectLog { get; set; }
        public virtual DbSet<TblProjectLogStatus> TblProjectLogStatus { get; set; }
        public virtual DbSet<TblProjectMember> TblProjectMember { get; set; }
        public virtual DbSet<TblProjectMemberProgressKerja> TblProjectMemberProgressKerja { get; set; }
        public virtual DbSet<TblProjectNotes> TblProjectNotes { get; set; }
        public virtual DbSet<TblProjectRelasi> TblProjectRelasi { get; set; }
        public virtual DbSet<TblProjectUser> TblProjectUser { get; set; }
        public virtual DbSet<TblRolePegawai> TblRolePegawai { get; set; }
        public virtual DbSet<TblSystemParameter> TblSystemParameter { get; set; }
        public virtual DbSet<TblTaskPegawai> TblTaskPegawai { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserLog> TblUserLog { get; set; }
        public virtual DbSet<TblUserSession> TblUserSession { get; set; }
        public virtual DbSet<TblBorrowedBook> TblBorrowedBook { get; set; }
        public virtual DbSet<TblMasterBook> TblMasterBook { get; set; }
        //public virtual DbSet<TblFasilitasKredit> TblFasilitasKredit { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=176.9.160.211;Database=dbPortalPMO;User Id=sa;Password=passw0rd4SQL");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

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

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RealeaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Navigation>(entity =>
            {
                entity.HasIndex(e => new { e.Type, e.Visible })
                    .HasName("NonClusteredIndex-20200531-134520");

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
                entity.HasIndex(e => new { e.NavigationId, e.RoleId, e.IsActive, e.IsDelete })
                    .HasName("NonClusteredIndex-20200531-134541");

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

            modelBuilder.Entity<TblConfigApps>(entity =>
            {
                entity.ToTable("Tbl_Config_Apps");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.MaxFileSize).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFaq>(entity =>
            {
                entity.ToTable("Tbl_FAQ");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFileRepository>(entity =>
            {
                entity.ToTable("Tbl_File_Repository");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.FileExt).HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UploadById).HasColumnName("UploadBy_Id");

                entity.Property(e => e.UploadTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblHoliday>(entity =>
            {
                entity.ToTable("Tbl_Holiday");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tanggal).HasColumnType("date");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblLogActivity>(entity =>
            {
                entity.ToTable("Tbl_LogActivity");

                entity.HasIndex(e => e.UserId)
                    .HasName("NonClusteredIndex-20200531-134556");

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

                entity.HasIndex(e => new { e.Type, e.Value, e.OrderBy, e.IsDeleted, e.IsActive })
                    .HasName("NonClusteredIndex-20200531-134448");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterClient>(entity =>
            {
                entity.ToTable("Tbl_Master_Client");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.TypeClient)
                    .WithMany(p => p.TblMasterClient)
                    .HasForeignKey(d => d.TypeClientId)
                    .HasConstraintName("FK_Tbl_Master_Client_Tbl_Master_Type_Client");
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

            modelBuilder.Entity<TblMasterJobPosition>(entity =>
            {
                entity.ToTable("Tbl_Master_Job_Position");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterKategoriKompleksitas>(entity =>
            {
                entity.ToTable("Tbl_Master_KategoriKompleksitas");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterKategoriProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Kategori_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterKlasifikasiProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Klasifikasi_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterKompleksitasProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Kompleksitas_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.KategoriKompleksitas)
                    .WithMany(p => p.TblMasterKompleksitasProject)
                    .HasForeignKey(d => d.KategoriKompleksitasId)
                    .HasConstraintName("FK_Tbl_Master_Kompleksitas_Project_Tbl_Master_KategoriKompleksitas");
            });

            modelBuilder.Entity<TblMasterPeriodeProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Periode_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterRole>(entity =>
            {
                entity.ToTable("Tbl_Master_Role");

                entity.HasIndex(e => new { e.IsDeleted, e.IsActive })
                    .HasName("NonClusteredIndex-20200531-134611");

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

            modelBuilder.Entity<TblMasterJabatan>(entity =>
            {
                entity.ToTable("Tbl_Master_Jabatan");

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

            modelBuilder.Entity<TblMasterSistem>(entity =>
            {
                entity.ToTable("Tbl_Master_Sistem");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterSkorProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Skor_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterStatusProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Status_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterSubKategoriProject>(entity =>
            {
                entity.ToTable("Tbl_Master_Sub_Kategori_Project");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterSubSistem>(entity =>
            {
                entity.ToTable("Tbl_Master_Sub_Sistem");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.MasterSistem)
                    .WithMany(p => p.TblMasterSubSistem)
                    .HasForeignKey(d => d.MasterSistemId)
                    .HasConstraintName("FK_Tbl_Master_Sub_Sistem_Tbl_Master_Sistem");
            });

            modelBuilder.Entity<TblMasterTypeClient>(entity =>
            {
                entity.ToTable("Tbl_Master_Type_Client");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMasterTypeDokumen>(entity =>
            {
                entity.ToTable("Tbl_Master_Type_Dokumen");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPegawai>(entity =>
            {
                entity.ToTable("Tbl_Pegawai");

                entity.HasIndex(e => new { e.UnitId, e.RoleId, e.Npp, e.IsDeleted, e.Ldaplogin })
                    .HasName("NonClusteredIndex-20200531-134632");

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
                    .WithMany(p => p.TblPegawai)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_Pegawai_Tbl_Master_Role");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TblPegawai)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Tbl_Pegawai_Tbl_Unit");
            });

            modelBuilder.Entity<TblProject>(entity =>
            {
                entity.ToTable("Tbl_Project");

                entity.Property(e => e.CloseOpenId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.DetailRequirment).HasColumnType("text");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPir).HasColumnName("IsPIR");

                entity.Property(e => e.Kode).HasMaxLength(50);

                entity.Property(e => e.NoDrf)
                    .HasColumnName("NoDRF")
                    .HasMaxLength(150);

                entity.Property(e => e.NoMemo).HasMaxLength(150);

                entity.Property(e => e.OrderBy).HasColumnName("Order_By");

                entity.Property(e => e.ProjectNo).HasMaxLength(150);

                entity.Property(e => e.TanggalDisposisi).HasColumnType("date");

                entity.Property(e => e.TanggalDrf)
                    .HasColumnName("TanggalDRF")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiDevelopmentAkhir).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiDevelopmentAwal).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiMulai).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiPilotingAkhir).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiPilotingAwal).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiPirakhir)
                    .HasColumnName("TanggalEstimasiPIRAkhir")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiPirawal)
                    .HasColumnName("TanggalEstimasiPIRAwal")
                    .HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiProduction).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiSelesai).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiTestingAkhir).HasColumnType("date");

                entity.Property(e => e.TanggalEstimasiTestingAwal).HasColumnType("date");

                entity.Property(e => e.TanggalKlarifikasi).HasColumnType("date");

                entity.Property(e => e.TanggalMemo).HasColumnType("date");

                entity.Property(e => e.TanggalSelesaiProject).HasColumnType("datetime");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.KategoriProject)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.KategoriProjectId)
                    .HasConstraintName("FK_Tbl_Master_Project_Tbl_Master_Kategori_Project");

                entity.HasOne(d => d.KlasifikasiProject)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.KlasifikasiProjectId)
                    .HasConstraintName("FK_Tbl_Project_Tbl_Master_Klasifikasi_Project");

                entity.HasOne(d => d.KompleksitasProject)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.KompleksitasProjectId)
                    .HasConstraintName("FK_Tbl_Project_Tbl_Master_Kompleksitas_Project");

                entity.HasOne(d => d.ProjectStatus)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.ProjectStatusId)
                    .HasConstraintName("FK_Tbl_Project_Tbl_Master_Status_Project");

                entity.HasOne(d => d.SkorProject)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.SkorProjectId)
                    .HasConstraintName("FK_Tbl_Master_Project_Tbl_Master_Skor_Project");

                entity.HasOne(d => d.SubKategoriProject)
                    .WithMany(p => p.TblProject)
                    .HasForeignKey(d => d.SubKategoriProjectId)
                    .HasConstraintName("FK_Tbl_Project_Tbl_Master_Sub_Kategori_Project");
            });

            modelBuilder.Entity<TblProjectDetailUnitRequest>(entity =>
            {
                entity.ToTable("Tbl_Project_Detail_Unit_Request");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.NoHpPic)
                    .HasColumnName("No_HP_PIC")
                    .HasMaxLength(50);

                entity.Property(e => e.NppPic)
                    .HasColumnName("Npp_PIC")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProjectFile>(entity =>
            {
                entity.ToTable("Tbl_Project_File");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.FileExt).HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UploadById).HasColumnName("UploadBy_Id");

                entity.Property(e => e.UploadTime).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblProjectFile)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Project_File_Tbl_Project");

                entity.HasOne(d => d.TypeDokumen)
                    .WithMany(p => p.TblProjectFile)
                    .HasForeignKey(d => d.TypeDokumenId)
                    .HasConstraintName("FK_Tbl_Project_File_Tbl_Master_Type_Dokumen");
            });

            modelBuilder.Entity<TblProjectLog>(entity =>
            {
                entity.ToTable("Tbl_Project_Log");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.Komentar).HasColumnType("text");

                entity.Property(e => e.Tanggal).HasColumnType("date");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.PegawaiIdFromNavigation)
                    .WithMany(p => p.TblProjectLog)
                    .HasForeignKey(d => d.PegawaiIdFrom)
                    .HasConstraintName("FK_Tbl_Project_Log_Tbl_Pegawai");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblProjectLog)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Project_Log_Tbl_Project");

                entity.HasOne(d => d.ProjectStatusFormNavigation)
                    .WithMany(p => p.TblProjectLogProjectStatusFormNavigation)
                    .HasForeignKey(d => d.ProjectStatusForm)
                    .HasConstraintName("FK_Tbl_Project_Log_Tbl_Master_Status_Project");

                entity.HasOne(d => d.ProjectStatusToNavigation)
                    .WithMany(p => p.TblProjectLogProjectStatusToNavigation)
                    .HasForeignKey(d => d.ProjectStatusTo)
                    .HasConstraintName("FK_Tbl_Project_Log_Tbl_Master_Status_Project1");
            });

            modelBuilder.Entity<TblProjectLogStatus>(entity =>
            {
                entity.ToTable("Tbl_Project_Log_Status");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tanggal).HasColumnType("date");
            });

            modelBuilder.Entity<TblProjectMember>(entity =>
            {
                entity.ToTable("Tbl_Project_Member");

                entity.Property(e => e.CatatanPegawai).HasColumnType("text");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDone).HasDefaultValueSql("((0))");

                entity.Property(e => e.Keterangan).HasColumnType("text");

                entity.Property(e => e.KeteranganPenyelesaian).HasColumnType("text");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StatusProgressId).HasDefaultValueSql("((0))");

                entity.Property(e => e.TanggalPenyelesaian).HasColumnType("date");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.JobPosition)
                    .WithMany(p => p.TblProjectMember)
                    .HasForeignKey(d => d.JobPositionId)
                    .HasConstraintName("FK_Tbl_Project_Member_Tbl_Master_Job_Position");

                entity.HasOne(d => d.Pegawai)
                    .WithMany(p => p.TblProjectMember)
                    .HasForeignKey(d => d.PegawaiId)
                    .HasConstraintName("FK_Tbl_Project_Member_Tbl_Pegawai");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblProjectMember)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Project_Member_Tbl_Master_Project");
            });

            modelBuilder.Entity<TblProjectMemberProgressKerja>(entity =>
            {
                entity.ToTable("Tbl_Project_Member_ProgressKerja");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Deskripsi).HasColumnType("text");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.TanggalAkhir).HasColumnType("date");

                entity.Property(e => e.TanggalAwal).HasColumnType("date");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectMember)
                    .WithMany(p => p.TblProjectMemberProgressKerja)
                    .HasForeignKey(d => d.ProjectMemberId)
                    .HasConstraintName("FK_Tbl_Project_Member_ProgressKerja_Tbl_Project_Member");
            });

            modelBuilder.Entity<TblProjectNotes>(entity =>
            {
                entity.ToTable("Tbl_Project_Notes");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes).HasColumnType("text");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblProjectNotes)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Project_Notes_Tbl_Project");
            });

            modelBuilder.Entity<TblProjectRelasi>(entity =>
            {
                entity.ToTable("Tbl_Project_Relasi");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblProjectUser>(entity =>
            {
                entity.ToTable("Tbl_Project_User");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NamaPic).HasColumnName("NamaPIC");

                entity.Property(e => e.NoHp).HasMaxLength(50);

                entity.Property(e => e.NppPic)
                    .HasColumnName("NppPIC")
                    .HasMaxLength(50);

                entity.Property(e => e.TanggalMulai).HasColumnType("date");

                entity.Property(e => e.TanggalSelesai).HasColumnType("date");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblProjectUser)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Tbl_Project_User_Tbl_Master_Client");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblProjectUser)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Project_User_Tbl_Project");
            });

            modelBuilder.Entity<TblRolePegawai>(entity =>
            {
                entity.ToTable("Tbl_Role_Pegawai");

                entity.HasIndex(e => new { e.PegawaiId, e.RoleId, e.UnitId, e.IsDeleted })
                    .HasName("NonClusteredIndex-20200531-134656");

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

            modelBuilder.Entity<TblSystemParameter>(entity =>
            {
                entity.ToTable("Tbl_SystemParameter");

                entity.HasIndex(e => new { e.IsActive, e.IsDelete })
                    .HasName("NonClusteredIndex-20200531-134714");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Key).IsRequired();

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblTaskPegawai>(entity =>
            {
                entity.ToTable("Tbl_Task_Pegawai");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.FromStatusProjectNavigation)
                    .WithMany(p => p.TblTaskPegawaiFromStatusProjectNavigation)
                    .HasForeignKey(d => d.FromStatusProject)
                    .HasConstraintName("FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project");

                entity.HasOne(d => d.PegawaiIdFromNavigation)
                    .WithMany(p => p.InversePegawaiIdFromNavigation)
                    .HasForeignKey(d => d.PegawaiIdFrom)
                    .HasConstraintName("FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblTaskPegawai)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tbl_Task_Pegawai_Tbl_Project");

                entity.HasOne(d => d.ToStatusProjectNavigation)
                    .WithMany(p => p.TblTaskPegawaiToStatusProjectNavigation)
                    .HasForeignKey(d => d.ToStatusProject)
                    .HasConstraintName("FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project1");
            });

            modelBuilder.Entity<TblUnit>(entity =>
            {
                entity.ToTable("Tbl_Unit");

                entity.HasIndex(e => new { e.ParentId, e.WilayahId, e.DivisiId, e.IsActive, e.IsDelete })
                    .HasName("NonClusteredIndex-20200531-134733");

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

                entity.HasIndex(e => new { e.Username, e.PegawaiId })
                    .HasName("NonClusteredIndex-20200531-134749");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PegawaiId).HasColumnName("Pegawai_Id");

                entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Pegawai)
                    .WithMany(p => p.TblUser)
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
