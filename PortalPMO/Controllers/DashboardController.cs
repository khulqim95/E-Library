using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalPMO.Component;
using PortalPMO.Models;
using PortalPMO.ViewModels;
using PortalPMO.Models.dbPortalPMO;
using System.Transactions;
using PortalPMO.Component;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using UAParser;
using System.Globalization;

namespace PortalPMO.Controllers
{
    public class DashboardController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public DashboardController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _accessor = accessor;
            lastSession = new LastSessionLog(accessor, context, config);
        }
        public IActionResult Index()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");
            string Path = location.AbsolutePath;
            ViewBag.CurrentPath = Path;
            int UnitIDLogin = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            TblUnit dataUnit = _context.TblUnit.Where(m => m.Id == UnitIDLogin).FirstOrDefault();

            ViewBag.UnitIdPegawai = dataUnit;

            return View();
        }

        #region LoadData Table Project
        [HttpPost]
        public IActionResult LoadDataProjectByTimeline(int? TypeTable, int? PegawaiIdSearchParam)
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
                var ProjectNoSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));


                List<BerandaDetailProjectMember_ViewModels> list = new List<BerandaDetailProjectMember_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<BerandaDetailProjectMember_ViewModels>(_context, "[sp_Beranda_LoadDataProject_View]", new SqlParameter[]{
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@PegawaiIdSearchParam",PegawaiIdSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitLoginId", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Beranda_LoadDataProject_Count]", new SqlParameter[]{
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@PegawaiIdSearchParam",PegawaiIdSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitLoginId", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId)});

                if (list == null)
                {
                    list = new List<BerandaDetailProjectMember_ViewModels>();
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


        #region LoadData Table TOP Project

        [HttpPost]
        public IActionResult LoadDataTableTOPProject(string UnitId, string Periode)
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
                var ProjectNoSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                string TanggalAwal = "";
                string TanggalAkhir = "";
                if (Periode != null)
                {
                    var splitTanggal = Periode.Replace(" ", "").Split("-");

                    TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                List<DashboardProject_ViewModels> list = new List<DashboardProject_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DashboardProject_ViewModels>(_context, "[sp_Dashboard_LoadDataTopProject_View]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_LoadDataTopProject_Count]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitId)
                });

                if (list == null)
                {
                    list = new List<DashboardProject_ViewModels>();
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

        #region Get All Data Total Project
        public JsonResult GetAllTotalProject(string UnitIdSearch, string Periode)
        {
            lastSession.Update();
           
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            string TanggalAwal = "";
            string TanggalAkhir = "";
            if (Periode != null)
            {
                var splitTanggal = Periode.Replace(" ", "").Split("-");

                TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            List<Dashboard_TotalProject_ViewModels> data = new List<Dashboard_TotalProject_ViewModels>();

            data = StoredProcedureExecutor.ExecuteSPList<Dashboard_TotalProject_ViewModels>(_context, "[sp_Dashboard_GetTotalProject]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });

            return Json(data);
        }
        #endregion

        #region Get All Data Total Project Status
        public JsonResult GetAllTotalProjectStatus(string UnitIdSearch, string Periode)
        {
            lastSession.Update();
          
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            string TanggalAwal = "";
            string TanggalAkhir = "";
            if (Periode != null)
            {
                var splitTanggal = Periode.Replace(" ", "").Split("-");

                TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            List<Dashboard_TotalProject_ViewModels> data = new List<Dashboard_TotalProject_ViewModels>();

            data = StoredProcedureExecutor.ExecuteSPList<Dashboard_TotalProject_ViewModels>(_context, "[sp_Dashboard_GetTotalProject]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });

            return Json(data);
        }
        #endregion


        public ActionResult GetAllProjectStatus(string UnitIdSearch, string Periode)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            Dashboard_ViewModels data = new Dashboard_ViewModels();

            try
            {
                int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                string TanggalAwal = "1900-01-01";
                string TanggalAkhir = "2200-12-31";
                if (Periode != null)
                {
                    var splitTanggal = Periode.Replace(" ", "").Split("-");

                    TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }
                
                data.data = new List<Dashboard_ProjectStatus_ViewModels>();
                data.data = StoredProcedureExecutor.ExecuteSPList<Dashboard_ProjectStatus_ViewModels>(_context, "[sp_Dashboard_GetTotalProject_ByStatuProject]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });
            }
            catch (Exception Ex)
            {

                throw;
            }


            return PartialView("_DataProjectStatus",data);
        }

        #region LoadData Table Project

        [HttpPost]
        public IActionResult LoadDataTableProject(int? TypeTable, string UnitId, string Periode)
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
                var ProjectNoSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                string TanggalAwal = "";
                string TanggalAkhir = "";
                if (Periode != null)
                {
                    var splitTanggal = Periode.Replace(" ", "").Split("-");

                    TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                List<DashboardProject_ViewModels> list = new List<DashboardProject_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DashboardProject_ViewModels>(_context, "[sp_Dashboard_LoadDataProject_View]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@TypeTable",TypeTable ),
                        new SqlParameter("@UnitId", UnitId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_LoadDataProject_Count]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@TypeTable",TypeTable ),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitId)
                });

                if (list == null)
                {
                    list = new List<DashboardProject_ViewModels>();
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

        #region Get All Workload
        public JsonResult GetAllWorkLoad(string UnitIdSearch, string Periode)
        {
            lastSession.Update();
           
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            string TanggalAwal = "";
            string TanggalAkhir = "";
            if (Periode != null)
            {
                var splitTanggal = Periode.Replace(" ", "").Split("-");

                TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            List<Dashboard_Workload_ViewModels> data = new List<Dashboard_Workload_ViewModels>();

            data = StoredProcedureExecutor.ExecuteSPList<Dashboard_Workload_ViewModels>(_context, "[sp_Dashboard_TopWorkLoad]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });

            return Json(data);
        }
        #endregion

        #region Get All Workload
        public JsonResult GetAllTopUser(string UnitIdSearch, string Periode)
        {
            lastSession.Update();
            if (UnitIdSearch == null)
            {
                UnitIdSearch = HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id);
            }
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            string TanggalAwal = "";
            string TanggalAkhir = "";
            if (Periode != null)
            {
                var splitTanggal = Periode.Replace(" ", "").Split("-");

                TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            List<Dashboard_Workload_ViewModels> data = new List<Dashboard_Workload_ViewModels>();

            data = StoredProcedureExecutor.ExecuteSPList<Dashboard_Workload_ViewModels>(_context, "[sp_Dashboard_TopUser]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });

            return Json(data);
        }
        #endregion

        #region Get All Workload
        public JsonResult GetSummaryProject(string UnitIdSearch, string Periode)
        {
            lastSession.Update();
           
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            int UnitID = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            string TanggalAwal = "";
            string TanggalAkhir = "";
            if (Periode != null)
            {
                var splitTanggal = Periode.Replace(" ", "").Split("-");

                TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            Dashboard_Summary_ViewModels data = new Dashboard_Summary_ViewModels();

            data = StoredProcedureExecutor.ExecuteSPSingle<Dashboard_Summary_ViewModels>(_context, "[sp_Dashboard_GetSumaryProject]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@UnitIdSearch", UnitIdSearch)
            });

            return Json(data);
        }
        #endregion

    }
}
