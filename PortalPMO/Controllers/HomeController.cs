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
using Microsoft.AspNetCore.Mvc.Rendering;
using TwoTierTemplate.ViewModels;

namespace PortalPMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public HomeController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
            var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
            var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));
            var WilayahId = 1;


            ViewBag.IdDropdwonDefault = PegawaiId;
            ViewBag.TextDropdwonDefault = HttpContext.Session.GetString(SessionConstan.Session_Role_Nama_Unit) + " - " + HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai) + " (" + HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai) + ")";

            //ViewBag.DropwdownPegawai = new SelectList(Utility.SelectDataPegawai(PegawaiId, _context).ToList(), "id", "text", PegawaiId);

            ViewBag.DropwdownUnit = new SelectList(Utility.SelectDataUnitByWilayahId(WilayahId, _context).ToList(), "id", "text", WilayahId);
            ViewBag.DropwdownRM = new SelectList(Utility.SelectDataRM(PegawaiId, _context).ToList(), "id", "text", PegawaiId);


            //TwoTierTemplate.ViewModels.Dashboard_ViewModels data = new TwoTierTemplate.ViewModels.Dashboard_ViewModels();
            //List<Tbl_Solicite> list1 = new List<Tbl_Solicite>();
            //List<Tbl_Prospek> list2 = new List<Tbl_Prospek>();
            //List<Tbl_Pipeline> list3 = new List<Tbl_Pipeline>();

            //list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_Solicite_Count]", new SqlParameter[]{
            //    new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
            //    new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id)))
            //});

            //list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_Prospek_Count]", new SqlParameter[]{
            //    new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
            //    new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id)))
            //});

            //list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_Pipeline_Count]", new SqlParameter[]{
            //    new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)))
            //});

            //data.tblSolicite = list1;
            //data.tblProspek = list2;
            //data.tblPipeline = list3;

            return View();
        }




        #region Random Code
        public string GenerateKode()
        {
            Random generator = new Random();
            return generator.Next(0, 999999).ToString("D6");
        }
        #endregion


        public IActionResult RoleChanged(string id)
        {
            lastSession.Update();
            var data = StoredProcedureExecutor.ExecuteSPSingle<DetailLogin_ViewModels>(_context, "sp_Change_Roles", new SqlParameter[]{
                        new SqlParameter("@id", id)
            });
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Pegawai);
            HttpContext.Session.Remove(SessionConstan.Session_Pegawai_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Unit_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Unit);
            //HttpContext.Session.Remove(SessionConstan.Session_User_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Unit_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Nama_Unit);
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Role);
            HttpContext.Session.Remove(SessionConstan.Session_Images_User);
            HttpContext.Session.Remove(SessionConstan.Session_Status_Role);
            HttpContext.Session.Remove(SessionConstan.Session_User_Role_Id);


            HttpContext.Session.SetString(SessionConstan.Session_Nama_Pegawai, data.Nama_Pegawai == null ? "-" : data.Nama_Pegawai);
            HttpContext.Session.SetString(SessionConstan.Session_Pegawai_Id, data.Pegawai_Id == null ? "" : data.Pegawai_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Unit_Id, data.Unit_Id == null ? "" : data.Unit_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Nama_Unit, data.Nama_Unit == null ? "-" : data.Nama_Unit);
            //HttpContext.Session.SetString(SessionConstan.Session_User_Id, data.User_Id == null ? "" : data.User_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Id, data.Role_Id == null ? "" : data.Role_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Unit_Id, data.Role_Unit_Id == null ? "" : data.Role_Unit_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Nama_Unit, data.Role_Nama_Unit == null ? "-" : data.Role_Nama_Unit);
            HttpContext.Session.SetString(SessionConstan.Session_Nama_Role, data.Nama_Role == null ? "-" : data.Nama_Role);
            HttpContext.Session.SetString(SessionConstan.Session_Images_User, data.Images_User == null ? GetConfig.AppSetting["AppSettings:GlobalSettings:DefaultImageUser"] : data.Images_User);
            HttpContext.Session.SetString(SessionConstan.Session_Status_Role, data.Status_Role == null ? "-" : data.Status_Role);
            HttpContext.Session.SetString(SessionConstan.Session_User_Role_Id, data.User_Role_Id == null ? "-" : data.User_Role_Id);

            // Get the menus Assigment
            var menuAss = _context.NavigationAssignment.Where(na => na.RoleId == int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id))).ToList();
            List<NavigationVM> ListNav = new List<NavigationVM>();
            foreach (var item in menuAss)
            {
                var nav = _context.Navigation.Where(nv => nv.Id == item.NavigationId && nv.Visible == 1 && nv.IsDeleted != true).FirstOrDefault();
                if (nav != null)
                {
                    NavigationVM navres = new NavigationVM
                    {
                        CreatedById = nav.CreatedById,
                        CreatedTime = nav.CreatedTime,
                        DeletedById = nav.DeletedById,
                        DeletedTime = nav.DeletedTime,
                        IconClass = nav.IconClass,
                        Id = nav.Id,
                        IsDeleted = nav.IsDeleted,
                        Name = nav.Name,
                        Order = nav.Order,
                        ParentNavigationId = nav.ParentNavigationId,
                        Route = nav.Route,
                        Type = nav.Type,
                        UpdatedById = nav.UpdatedById,
                        UpdatedTime = nav.UpdatedTime,
                        Visible = nav.Visible
                    };
                    ListNav.Add(navres);
                }
            }

            HttpContext.Session.SetObject("AllMenu", ListNav);

            //HttpContext.Session.SetString(SessionConstan.Session_Role_Id, Result.Role_Id+"");
            //HttpContext.Session.SetString(SessionConstan.Session_Unit_Id, Result.Unit_Id + "");

            //var RoleId = HttpContext.Session.GetString(SessionConstan.Session_User_Role_Id);
            if (ListNav.Count == 0)
            {
                return Content("EmptyAccess");
            }


            return Content("Home");
        }


        #region Get All Data Beranda Pending Task
        public JsonResult GetAllDataBeranda(int? PegawaiIdSearch, string Periode)
        {
            lastSession.Update();
            if (PegawaiIdSearch == null)
            {
                PegawaiIdSearch = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
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

            Home_ViewModels data = new Home_ViewModels();
            data.ProjectTotal = new HomeTotalProject_ViewModels();

            data.ProjectTotal = StoredProcedureExecutor.ExecuteSPSingle<HomeTotalProject_ViewModels>(_context, "sp_Beranda_GetTotalProject", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@PegawaiId", PegawaiId),
                        new SqlParameter("@PegawaiIdSearchParam", PegawaiIdSearch),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitID)

            });

            return Json(data);
        }
        #endregion



        #region Get All Data Beranda
        public JsonResult GetAllDataPendingTask(string UnitId, string UserId)
        {
            lastSession.Update();
            //if (PegawaiIdSearch == null)
            //{
            //    PegawaiIdSearch = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
            //}
            //var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
            //var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
            //var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
            //string TanggalAwal = "";
            //string TanggalAkhir = "";
            //if (Periode != null)
            //{
            //    var splitTanggal = Periode.Replace(" ", "").Split("-");

            //    TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //}

            //List<BerandaDetailProjectMember_ViewModels> data = new List<BerandaDetailProjectMember_ViewModels>();

            //data = StoredProcedureExecutor.ExecuteSPList<BerandaDetailProjectMember_ViewModels>(_context, "[sp_Beranda_LoadDataProject_AllPendingTask]", new SqlParameter[]{
            //            new SqlParameter("@UnitId", UnitId),
            //            new SqlParameter("@RoleId", RoleId)

            //});

            TwoTierTemplate.ViewModels.Dashboard_ViewModels data = new TwoTierTemplate.ViewModels.Dashboard_ViewModels();
            List<Tbl_Solicite> list1 = new List<Tbl_Solicite>();
            List<Tbl_Prospek> list2 = new List<Tbl_Prospek>();
            List<Tbl_Pipeline> list3 = new List<Tbl_Pipeline>();

            list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_SoliciteBeranda_Count]", new SqlParameter[]{
                new SqlParameter("@Unitid", UnitId),
                new SqlParameter("@Userid", UserId)
            });

            list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_ProspekBeranda_Count]", new SqlParameter[]{
                new SqlParameter("@Unitid", UnitId),
                new SqlParameter("@Userid", UserId)
            });

            list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_PipelineBeranda_Count]", new SqlParameter[]{
                new SqlParameter("@Unitid", UnitId),
                new SqlParameter("@Userid", UserId)
            });

            data.tblSolicite = list1;
            data.tblProspek = list2;
            data.tblPipeline = list3;


            return Json(data);
        }
        #endregion



        #region LoadData Table Project
        [HttpPost]
        public IActionResult LoadDataProjectByTimeline(int? TypeTable, int? PegawaiIdSearchParam, string Periode)
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
                string TanggalAwal = "";
                string TanggalAkhir = "";
                if (Periode != null)
                {
                    var splitTanggal = Periode.Replace(" ", "").Split("-");

                    TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                List<BerandaDetailProjectMember_ViewModels> list = new List<BerandaDetailProjectMember_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<BerandaDetailProjectMember_ViewModels>(_context, "[sp_Beranda_LoadDataProject_View]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@PegawaiIdSearchParam",PegawaiIdSearchParam),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@TypeTable",TypeTable ),
                        new SqlParameter("@UnitLoginId", UnitId),
                        new SqlParameter("@PegawaiLoginId", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Beranda_LoadDataProject_Count]", new SqlParameter[]{
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@PegawaiIdSearchParam",PegawaiIdSearchParam),
                        new SqlParameter("@TypeTable",TypeTable ),
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitLoginId", UnitId),
                        new SqlParameter("@PegawaiLoginId", PegawaiId)});

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



        #region LoadDataTableSolicite
        [HttpPost]
        public IActionResult LoadDataTableSolicite(string UnitId, string UserId)
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
                var KodeSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                //if ((UnitId == null && UserId == null) || (UnitId == "-" && UserId == "-"))
                if (UnitId == null && UserId == null)

                    {
                        List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_Solicite_View]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
                        new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id))),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_Solicite_Count]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
                        new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id)))
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
                }
                else {
                    List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteBeranda_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteBeranda_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId)
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region LoadDataTableProspek
        [HttpPost]
        public IActionResult LoadDataTableProspek(string UnitId, string UserId)
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
                var KodeSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;


                if (UserId == null && UnitId == null)
                {
                    List<TwoTierTemplate.ViewModels.TblProspek_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblProspek_ViewModels>(_context, "[sp_Dashboard_Prospek_View]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
                        new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id))),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_Prospek_Count]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
                        new SqlParameter("@Roleid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id)))
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
                }
                else {
                    List<TwoTierTemplate.ViewModels.TblProspek_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblProspek_ViewModels>(_context, "[sp_Dashboard_ProspekBeranda_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_ProspekBeranda_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId),
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region LoadDataTablePipeline
        [HttpPost]
        public IActionResult LoadDataTablePipeline(string UnitId, string UserId)
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
                var KodeSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;


                if (UnitId == null && UserId == null)
                {
                    List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>(_context, "[sp_Dashboard_Pipeline_View]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_Pipeline_Count]", new SqlParameter[]{
                        new SqlParameter("@Userid", int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)))
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
                }
                else {
                    List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();

                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>(_context, "[sp_Dashboard_PipelineBeranda_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_PipelineBeranda_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@Userid", UserId),
                    });

                    if (list == null)
                    {
                        list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();
                        recordsTotal = 0;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });

                }
                
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        //#region LoadDataTableSoliciteById
        //[HttpPost]
        //public IActionResult LoadDataTableSoliciteById(string UnitId, string UserId)
        //{
        //    if (!lastSession.Update())
        //    {
        //        return RedirectToAction("Login", "Login", new { a = true });
        //    }

        //    try
        //    {
        //        var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

        //        var draw = dict["draw"];

        //        //Untuk mengetahui info paging dari datatable
        //        var start = dict["start"];
        //        var length = dict["length"];

        //        //Server side datatable hanya support untuk mendapatkan data mulai ke berapa, untuk mengirim row ke berapa
        //        //Kita perlu membuat logika sendiri
        //        var pageNumber = (int.Parse(start) / int.Parse(length)) + 1;

        //        //Untuk mengetahui info order column datatable
        //        var sortColumn = dict["columns[" + dict["order[0][column]"] + "][data]"];
        //        var sortColumnDir = dict["order[0][dir]"];
        //        var KodeSearchParam = dict["columns[2][search][value]"];
        //        var NamaSearchParam = dict["columns[3][search][value]"];

        //        //Untuk mengetahui info jumlah page dan total skip data
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;

        //        List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();

        //        list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByUnit_View]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId),
        //                new SqlParameter("@sortColumn", "Id"),
        //                new SqlParameter("@sortColumnDir", "desc"),
        //                new SqlParameter("@PageNumber", pageNumber),
        //                new SqlParameter("@RowsPage", pageSize)
        //        });

        //        recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByUnit_Count]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId)
        //        });

        //        if (list == null)
        //        {
        //            list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
        //            recordsTotal = 0;
        //        }
        //        return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        //#region LoadDataTableProspekById
        //[HttpPost]
        //public IActionResult LoadDataTableProspekById(string UnitId, string UserId)
        //{
        //    if (!lastSession.Update())
        //    {
        //        return RedirectToAction("Login", "Login", new { a = true });
        //    }

        //    try
        //    {
        //        var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

        //        var draw = dict["draw"];

        //        //Untuk mengetahui info paging dari datatable
        //        var start = dict["start"];
        //        var length = dict["length"];

        //        //Server side datatable hanya support untuk mendapatkan data mulai ke berapa, untuk mengirim row ke berapa
        //        //Kita perlu membuat logika sendiri
        //        var pageNumber = (int.Parse(start) / int.Parse(length)) + 1;

        //        //Untuk mengetahui info order column datatable
        //        var sortColumn = dict["columns[" + dict["order[0][column]"] + "][data]"];
        //        var sortColumnDir = dict["order[0][dir]"];
        //        var KodeSearchParam = dict["columns[2][search][value]"];
        //        var NamaSearchParam = dict["columns[3][search][value]"];

        //        //Untuk mengetahui info jumlah page dan total skip data
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;

        //        List<TwoTierTemplate.ViewModels.TblProspek_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();

        //        list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblProspek_ViewModels>(_context, "[sp_Dashboard_Prospek_View]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId),
        //                new SqlParameter("@sortColumn", "Id"),
        //                new SqlParameter("@sortColumnDir", "desc"),
        //                new SqlParameter("@PageNumber", pageNumber),
        //                new SqlParameter("@RowsPage", pageSize)
        //        });

        //        recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_Prospek_Count]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId),
        //        });

        //        if (list == null)
        //        {
        //            list = new List<TwoTierTemplate.ViewModels.TblProspek_ViewModels>();
        //            recordsTotal = 0;
        //        }
        //        return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        //#region LoadDataTablePipelineById
        //[HttpPost]
        //public IActionResult LoadDataTablePipelineById(string UnitId, string UserId)
        //{
        //    if (!lastSession.Update())
        //    {
        //        return RedirectToAction("Login", "Login", new { a = true });
        //    }

        //    try
        //    {
        //        var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

        //        var draw = dict["draw"];

        //        //Untuk mengetahui info paging dari datatable
        //        var start = dict["start"];
        //        var length = dict["length"];

        //        //Server side datatable hanya support untuk mendapatkan data mulai ke berapa, untuk mengirim row ke berapa
        //        //Kita perlu membuat logika sendiri
        //        var pageNumber = (int.Parse(start) / int.Parse(length)) + 1;

        //        //Untuk mengetahui info order column datatable
        //        var sortColumn = dict["columns[" + dict["order[0][column]"] + "][data]"];
        //        var sortColumnDir = dict["order[0][dir]"];
        //        var KodeSearchParam = dict["columns[2][search][value]"];
        //        var NamaSearchParam = dict["columns[3][search][value]"];

        //        //Untuk mengetahui info jumlah page dan total skip data
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;

        //        List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();

        //        list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>(_context, "[sp_Dashboard_Pipeline_View]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId),
        //                new SqlParameter("@sortColumn", "Id"),
        //                new SqlParameter("@sortColumnDir", "desc"),
        //                new SqlParameter("@PageNumber", pageNumber),
        //                new SqlParameter("@RowsPage", pageSize)
        //        });

        //        recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_Pipeline_Count]", new SqlParameter[]{
        //                new SqlParameter("@Unitid", UnitId),
        //                new SqlParameter("@Userid", UserId),
        //        });

        //        if (list == null)
        //        {
        //            list = new List<TwoTierTemplate.ViewModels.TblPipeline_ViewModels>();
        //            recordsTotal = 0;
        //        }
        //        return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        #region View Fasilitas Kredit
        public IActionResult ViewFasilitasKredit()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }

            return PartialView("ViewFasilitasKredit");
        }
        #endregion

        #region LoadDataTableFasilitas
        [HttpPost]
        public IActionResult LoadDataTableFasilitasKredit(int Id)
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
                var KodeSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_GetDataFasilitasKreditById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 10)
                });

                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FAQ_Count]", new SqlParameter[]{
                //        new SqlParameter("@Nama", NamaSearchParam)});

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>();
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
    }
}
