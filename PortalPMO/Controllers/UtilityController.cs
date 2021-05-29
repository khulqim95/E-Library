using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalPMO.ViewModels;
using PortalPMO.Models.dbPortalPMO;
using PortalPMO.Component;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net;
using System.DirectoryServices;
using PortalPMO.Content;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalPMO.Controllers
{
    public class UtilityController : Controller
    {

        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;


        public UtilityController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            lastSession = new LastSessionLog(accessor, context, config);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            lastSession.Update();
            return View();
        }

        public IActionResult EmptyData()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

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




        #region Get All Role Pegawai
        public JsonResult GetAllRolePegawai(int Pegawai_Id)
        {
            lastSession.Update();
            List<Dropdown_ViewModels> data = new List<Dropdown_ViewModels>();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");

            data = StoredProcedureExecutor.ExecuteSPList<Dropdown_ViewModels>(_context, "sp_Login_GetDataRolePegawai", new SqlParameter[]{
                        new SqlParameter("@Pegawai_id", Pegawai_Id),
                        new SqlParameter("@Date", Date)
            });
            return Json(data);
        }
        #endregion

        #region Dropdown Role
        public JsonResult GetDropdownRolesByMenuId(string Roles)
        {
            lastSession.Update();
            List<DataDropdownServerSide> data = new List<DataDropdownServerSide>();

            data = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_Menu_GetDataRolesById]", new SqlParameter[]{
                        new SqlParameter("@Id", Roles)});

            return Json(data);
        }
        #endregion

        #region Get All Dropdown Unit
        public JsonResult GetAllDataUnit(string q, string page, int rowPerPage, string TypeUnit)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_Unit", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@TypeUnit", TypeUnit),

                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "SP_Dropdown_Unit_Count", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@TypeUnit", TypeUnit)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Sistem
        public JsonResult GetAllDataSistem(string q, string page, int rowPerPage, string TypeUnit)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_MasterSistem", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "SP_Dropdown_MasterSistem_Count", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Kategori Project
        public JsonResult GetAllDataKategoriProject(string q, string page, int rowPerPage, string TypeUnit)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_KategoriProject", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "SP_Dropdown_KategoriProject_Count", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Sub Kategori Project
        public JsonResult GetAllDataSubKategoriProject(string q, string page, int rowPerPage, int? KategoriProjectId)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_SubKategoriProject]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@KategoriProjectId", KategoriProjectId),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_SubKategoriProject_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@KategoriProjectId", KategoriProjectId)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Project
        public JsonResult GetAllDataProject(string q, string page, int rowPerPage, string TypeUnit)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_Project", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "SP_Dropdown_Project_Count", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Master Type Client
        public JsonResult GetAllDataTypeClient(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_MasterTypeClient", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "SP_Dropdown_MasterTypeClient_Count", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Master Client
        public JsonResult GetAllDataClient(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_Client]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_Client_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Kompleksitas Project
        public JsonResult GetAllDataKompleksitasProject(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_KompleksitasProject]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_KompleksitasProject_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Klasifikasi Project
        public JsonResult GetAllDataKlasifikasiProject(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_KlasifikasiProject]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_KlasifikasiProject_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Skor Project
        public JsonResult GetAllDataSkorProject(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_SkorProject]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_SkorProject_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Job Position
        public JsonResult GetAllDataJobPosition(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_JobPosition]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_JobPosition_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown All Pegawai
        public JsonResult GetAllDataPegawai(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_AllPegawai]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_AllPegawai_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Master Project Status
        public JsonResult GetDataProjectStatus(string q, string page, int rowPerPage)
        {
            lastSession.Update();
            if (page == null)
            {
                page = "1";
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_ProjectStatus]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q),
                        new SqlParameter("@PageNumber", page),
                        new SqlParameter("@RowsPage", rowPerPage)});

            source.total_count = StoredProcedureExecutor.ExecuteScalarInt(_context, "[SP_Dropdown_ProjectStatus_Count]", new SqlParameter[]{
                        new SqlParameter("@Parameter", q)
            });

            return Json(source);
        }
        #endregion

        #region LoadData Progress Project Member
        [HttpPost]
        public IActionResult LoadDataProgressKerjaMember(int ProjectMemberId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

                var draw = dict["draw"];

                //Untuk mengetahui info paging dari datatable
                var start = dict["start"];
                var length = dict["length"];

                //Server side datatable hanya support untuk mendapatkan data mulai ke berapa, untuk mengirim row ke berapa
                //Kita perlu membuat logika sendiri
                var pageNumber = (int.Parse(start) / int.Parse(length)) + 1;

                //Untuk mengetahui info order column datatable
                var sortColumn = dict["columns[" + dict["order[0][column]"] + "][data]"];
                var sortColumnDir = dict["order[0][dir]"];
                var JudulSearchParam = dict["columns[2][search][value]"];
                var TanggalAwalSearchParam = dict["columns[3][search][value]"];
                var TanggalAkhirSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<ProgressKerjaMember_ViewModels> list = new List<ProgressKerjaMember_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<ProgressKerjaMember_ViewModels>(_context, "[sp_ProjectMember_ProgressKerja_View]", new SqlParameter[]{
                        new SqlParameter("@Judul", JudulSearchParam),
                        new SqlParameter("@ProjectMemberId", ProjectMemberId),
                        new SqlParameter("@TanggalAwal", TanggalAwalSearchParam),
                        new SqlParameter("@TanggalAkhir", TanggalAkhirSearchParam),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                });

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_ProjectMember_ProgressKerja_Count]", new SqlParameter[]{
                        new SqlParameter("@Judul", JudulSearchParam),
                        new SqlParameter("@ProjectMemberId", ProjectMemberId),
                        new SqlParameter("@TanggalAwal", TanggalAwalSearchParam),
                        new SqlParameter("@TanggalAkhir", TanggalAkhirSearchParam)
                });

                if (list == null)
                {
                    list = new List<ProgressKerjaMember_ViewModels>();
                    recordsTotal = 0;
                }
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region Get All Dropdown RM by Unit
        public async Task<JsonResult> GetAllRMByUnit(int UnitId, int page, int rowPerPage)
        {
            lastSession.Update();

            if (page == null)
            {
                page = 1;
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "[SP_Dropdown_RMByUnit]", new SqlParameter[]{
                        new SqlParameter("@UnitId", UnitId)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Unit By Wilayah
        public async Task<JsonResult> GetAllDataUnitByWilayah(int page, int rowPerPage)
        {
            lastSession.Update();

            if (page == null)
            {
                page = 1;
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();
            int? WilayahId = 0;
            var Wilayah = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);
            if (Wilayah != "-")
            {
                WilayahId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id));
            }
            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_UnitByWilayahId", new SqlParameter[]{
                        new SqlParameter("@Parameter ", ""),
                        new SqlParameter("@TypeWilayah ", ""),
                        new SqlParameter("@WilayahId ", WilayahId),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage ", 10000)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Wilayah
        public async Task<JsonResult> GetAllDataWilayah(int page, int rowPerPage)
        {
            lastSession.Update();

            if (page == null)
            {
                page = 1;
            }
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_Wilayah", new SqlParameter[]{
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage ", 1000)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown SRMandRM
        public async Task<JsonResult> GetAllDataSRMandRM(int page, int rowPerPage)
        {
            lastSession.Update();

            if (page == null)
            {
                page = 1;
            }
            var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));
            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_SRMandRM", new SqlParameter[]{
                new SqlParameter("@Unitid", UnitId)
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Segmen
        public JsonResult GetAllDataSegmen(int page, int rowPerPage)
        {
            lastSession.Update();

            if (page == null)
            {
                page = 1;
            }

            ListDataDropdownServerSide source = new ListDataDropdownServerSide();
            source.items = new List<DataDropdownServerSide>();

            source.items = StoredProcedureExecutor.ExecuteSPList<DataDropdownServerSide>(_context, "SP_Dropdown_Segmen", new SqlParameter[]{
            });

            return Json(source);
        }
        #endregion

        #region Get All Dropdown Jabatan
        public async Task<JsonResult> GetAllDataJabatan(string q, string page, int rowPerPage)
        {

            if (page == null)
            {
                page = "1";
            }
            var data = await StoredProcedureExecutor.ExecuteSPListAsync<DataDropdownServerSide>(_context, "[sp_Dropdown_Jabatan]", new SqlParameter[]{});

            //var stringRes = await response.Content.ReadAsStringAsync();
            //var data = JsonConvert.DeserializeObject<ServiceResponseSingle2<ListDataDropdownServerSide>>(stringRes);
            if (data != null)
            {
                return Json(data);
            }
            else
            {
                List<ListDataDropdownServerSide> dataNull = new List<ListDataDropdownServerSide>();
                return Json(dataNull);
            }
        }
        #endregion
    }
}
