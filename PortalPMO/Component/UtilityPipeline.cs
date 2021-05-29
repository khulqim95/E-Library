
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;
using TwoTierTemplate.Models;

namespace PortalPMO.Component
{
    public class UtilityPipeline
    {

        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;

        public const int StatusMember_MenungguInputKode = 1;
        public const int StatusMember_MenungguValidasiPanitia = 2;
        public const int StatusMember_Tervalidasi = 3;
        public const int StatusMember_Reject = 4;


        public const int StatusRole_SuperAdmin = 1;
        public const int StatusRole_Panitia = 2;
        public const int StatusRole_Anggota = 3;


        public const int TypeDokumen_Memo = 1;
        public const int TypeDokumen_Notulen = 2;
        public const int TypeDokumen_DRF = 3;
        public const int TypeDokumen_BAST = 4;
        public const int TypeDokumen_BAUAT = 5;
        public const int TypeDokumen_PIR = 6;
        public const int TypeDokumen_TestingQA = 7;
        public const int TypeDokumen_DokumenLainnya = 8;

        public const int ActivityCreateProject = 1;
        public const int ActivityUbahStatusProject = 2;
        public const int ActivityTambahAnggotTeamProject = 3;
        public const int ActivityEditDataAnggotaTeamProject = 4;
        public const int ActivityHapusAnggotaTeamProject = 5;
        public const int ActivityTambahDetailPekerjaan = 6;
        public const int ActivityEditDetailPekerjaan = 7;
        public const int ActivityHapusDetailPekerjaan = 8;
        public const int ActivityUpdateKategoriPerusahaan = 9;
        public const int ActivityHapusLampiranFile = 10;
        public const int ActivityTambahLampiranFile = 11;



        public const int StatusProjectOpen = 1;
        public const int StatusProjectClosed = 2;



        #region Select Data Lookup
        public static IEnumerable<TblLookup> SelectLookup(string Type, dbPipelineContext _context)
        {
            List<TblLookup> ListData = new List<TblLookup>();

            ListData = _context.TblLookup.Where(m => m.Type == Type && m.IsActive == true && m.IsDeleted == false).OrderBy(m => m.OrderBy).ToList();

            return ListData;
        }
        #endregion

        #region Select Data Lookup Name
        public static string SelectLookupName(string Type,int? Value, dbPipelineContext _context)
        {
            TblLookup ListData = new TblLookup();

            ListData = _context.TblLookup.Where(m => m.Type == Type && m.Value == Value && m.IsActive == true && m.IsDeleted == false).OrderBy(m => m.OrderBy).FirstOrDefault();

            return ListData.Name;
        }
        #endregion

        //#region Select Data Project Status
        //public static string SelectProjectStatusName(int? Id, dbPipelineContext _context)
        //{
        //    TblMasterStatusProject ListData = new TblMasterStatusProject();

        //    ListData = _context.TblMasterStatusProject.Where(m => m.Id == Id && m.IsActive == true && m.IsDeleted == false).OrderBy(m => m.OrderBy).FirstOrDefault();

        //    return ListData.Nama;
        //}
        //#endregion

        #region Select Data Type Dokumen
        public static IEnumerable<DataDropdownClientSide> SelectTypeDokumen(dbPipelineContext _context)
        {
            List<DataDropdownClientSide> data = new List<DataDropdownClientSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownClientSide>(_context, "[SP_Dropdown_TypeDokumen]", new SqlParameter[]{
                    });
            data = data != null ? data : new List<DataDropdownClientSide>();

            return data;
        }
        #endregion

        #region Select Data By ID For Dropdown Master Role
        public static IEnumerable<DataDropdownServerSide> SelectDataMasterRole(dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();

            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_MasterRole]", new SqlParameter[]{

                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;
        }

        public static IEnumerable<DataDropdownServerSide> SelectDataMasterRoleById(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();

            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_MasterRoleById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)

                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;
        }
        #endregion



        #region Select Data By ID For Dropdown Menu
        public static IEnumerable<DataDropdownServerSide> SelectDataMenu(dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();

            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "sp_dropdown_menu", new SqlParameter[]{
                        
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;
        }
        #endregion

        #region Select Data By ID For Dropdown Unit
        public static IEnumerable<DataDropdownServerSide> SelectDataUnit(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_Unit_ById", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Jabatan
        public static IEnumerable<DataDropdownServerSide> SelectDataJabatan(dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_Jabatan]", new SqlParameter[]{
                        //new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Unit By Wilayah Id
        public static IEnumerable<DataDropdownServerSide> SelectDataUnitByWilayahId(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_UnitByWilayahId", new SqlParameter[]{
                        new SqlParameter("@Parameter ", ""),
                        new SqlParameter("@TypeWilayah ", ""),
                        new SqlParameter("@WilayahId ", Id),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage ", 1000)

                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion


        #region Select Data By ID For Dropdown Sistem
        public static IEnumerable<DataDropdownServerSide> SelectDataMasterSistem(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_MasterSistemById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Type Client
        public static IEnumerable<DataDropdownServerSide> SelectDataTypeClient(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_MasterTypeClientById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Client
        public static IEnumerable<DataDropdownServerSide> SelectDataClient(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_ClientById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Pegawai
        public static IEnumerable<DataDropdownServerSide> SelectDataPegawai(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_AllPegawaibyId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown RM
        public static IEnumerable<DataDropdownServerSide> SelectDataRM(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_RMByUnit]", new SqlParameter[]{
                        new SqlParameter("@UnitId", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Project
        public static IEnumerable<DataDropdownServerSide> SelectDataProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_ProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Job Desc
        public static IEnumerable<DataDropdownServerSide> SelectDataJobPosition(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_JobPositionById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Klasifikasi Project
        public static IEnumerable<DataDropdownServerSide> SelectDataKlasifikasiProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_KlasifikasiProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Kompleksitas Project
        public static IEnumerable<DataDropdownServerSide> SelectDataKompleksitasProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_KompleksitasProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Kategori Project
        public static IEnumerable<DataDropdownServerSide> SelectDataKategoriProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_KategoriProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Sub Kategori Project
        public static IEnumerable<DataDropdownServerSide> SelectDataSubKategoriProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_SubKategoriProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region Select Data By ID For Dropdown Skor Project
        public static IEnumerable<DataDropdownServerSide> SelectDataSkorProject(int? Id, dbPipelineContext _context)
        {
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();
            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[sp_Dropdown_SkorProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)
                    });
            data = data != null ? data : new List<DataDropdownServerSide>();

            return data;

        }
        #endregion

        #region LDAP Login
        public static bool AuthenticationLdap(String userName, String passWord)
        {
            string Domain = string.Empty;
            string path = string.Empty;
            string sDomainAndUsername = Domain;
            path = GetConfig.AppSetting["AppSettings:LDAPConnection:Url"];
            Domain = "uid=" + userName + "," + GetConfig.AppSetting["AppSettings:LDAPConnection:LdapHierarchy"];
            AuthenticationTypes at = AuthenticationTypes.ServerBind;
            DirectoryEntry entry = new DirectoryEntry(path, Domain, passWord, at);
            bool cek = false;
            try
            {
                Object obj = entry.NativeObject;
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                SearchResult result;
                mySearcher.Filter = "(uid=" + userName + ")";
                mySearcher.PropertiesToLoad.Add("sn");
                result = mySearcher.FindOne();
                cek = true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error authenticating user. " + ex.Message);
                cek = false;
            }
            return cek;
        }
        #endregion

        #region GenerateNoProject
        public static string GenerateNoProject(dbPipelineContext _context)
        {
            string Bulan = DateTime.Now.ToString("MM");
            string Tahun = DateTime.Now.ToString("yyyy");

            string NomorProject = StoredProcedureExecutor.ExecuteScalarString(_context, "[sp_GetNoProject_ByDate]", new SqlParameter[]{
                        new SqlParameter("@Bulan", Bulan),
                        new SqlParameter("@Tahun", Tahun)
            });

            return NomorProject;

        }
        #endregion

    }
}
