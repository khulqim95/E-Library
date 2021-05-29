using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PortalPMO.Component;
using PortalPMO.Models.dbPortalPMO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using TwoTierTemplate.ViewModels;

namespace TwoTierTemplate.Controllers
{
    public class TrackingUnitController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public TrackingUnitController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
            var Wilayah = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);
            int? WilayahId = 0;
            if (Wilayah != "-")
            {
                WilayahId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id));
            }
            //int? WilayahId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id));


            ViewBag.IdDropdwonDefault = PegawaiId;
            ViewBag.TextDropdwonDefault = HttpContext.Session.GetString(SessionConstan.Session_Role_Nama_Unit) + " - " + HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai) + " (" + HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai) + ")";

            //ViewBag.DropwdownPegawai = new SelectList(Utility.SelectDataPegawai(PegawaiId, _context).ToList(), "id", "text", PegawaiId);

            ViewBag.DropwdownUnit = new SelectList(Utility.SelectDataUnitByWilayahId(WilayahId, _context).ToList(), "id", "text", WilayahId);
            ViewBag.DropwdownRM = new SelectList(Utility.SelectDataRM(PegawaiId, _context).ToList(), "id", "text", PegawaiId);

            return View();
        }

        #region Get All Data Beranda
        public JsonResult GetAllDataPendingTask(string UnitId)
        {
            try 
            { 
                lastSession.Update();
                Dashboard_ViewModels data = new Dashboard_ViewModels();
                var Unitid = HttpContext.Session.GetString(SessionConstan.Session_Unit_Id);

                if (UnitId == null)
                {
                    // Card Summary
                    var listExSummary = StoredProcedureExecutor.ExecuteSPList<ExecutiveSummary>(_context, "[sp_Dashboard_GapByUnit]", new SqlParameter[]{
                    new SqlParameter("@Unitid", Unitid)
                });

                    // Card Per Detail
                    var list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_SoliciteByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", Unitid)
                });

                    var list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_ProspekByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", Unitid)
                });

                    var list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_PipelineByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", Unitid)
                });

                    data.ExecutiveSummary = listExSummary;
                    data.tblSolicite = list1;
                    data.tblProspek = list2;
                    data.tblPipeline = list3;
                }
                else
                {
                    // Card Summary
                    var listExSummary = StoredProcedureExecutor.ExecuteSPList<ExecutiveSummary>(_context, "[sp_Dashboard_GapByUnit]", new SqlParameter[]{
                    new SqlParameter("@Unitid", UnitId)
                });

                    // Card Per Detail
                    var list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_SoliciteByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", UnitId)
                });

                    var list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_ProspekByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", UnitId)
                });

                    var list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_PipelineByUnit_Count]", new SqlParameter[]{
                    new SqlParameter("@Unitid", UnitId)
                });

                    data.ExecutiveSummary = listExSummary;
                    data.tblSolicite = list1;
                    data.tblProspek = list2;
                    data.tblPipeline = list3;
                }

                
                return Json(data);
            }

            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region LoadDataTableSolicite
        [HttpPost]
        public IActionResult LoadDataTableSolicite(string UnitId)
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

                var Unitid = HttpContext.Session.GetString(SessionConstan.Session_Unit_Id);
                List<TblSolicite_ViewModels> list = new List<TblSolicite_ViewModels>();

                if (UnitId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid)
                    });
                }
                else {
                    list = StoredProcedureExecutor.ExecuteSPList<TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId)
                    });
                }
                

                if (list == null)
                {
                    list = new List<TblSolicite_ViewModels>();
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

        #region LoadDataTableProspek
        [HttpPost]
        public IActionResult LoadDataTableProspek(string UnitId)
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

                var Unitid = HttpContext.Session.GetString(SessionConstan.Session_Unit_Id);
                List<TblProspek_ViewModels> list = new List<TblProspek_ViewModels>();

                if (UnitId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblProspek_ViewModels>(_context, "[sp_Dashboard_ProspekByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_ProspekByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblProspek_ViewModels>(_context, "[sp_Dashboard_ProspekByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_ProspekByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId)
                    });
                }

                if (list == null)
                {
                    list = new List<TblProspek_ViewModels>();
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

        #region LoadDataTablePipeline
        [HttpPost]
        public IActionResult LoadDataTablePipeline(string UnitId)
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

                var Unitid = HttpContext.Session.GetString(SessionConstan.Session_Unit_Id);
                List<TblPipeline_ViewModels> list = new List<TblPipeline_ViewModels>();

                if (UnitId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblPipeline_ViewModels>(_context, "[sp_Dashboard_PipelineByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_PipelineByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", Unitid)
                    });
                }
                else {
                    list = StoredProcedureExecutor.ExecuteSPList<TblPipeline_ViewModels>(_context, "[sp_Dashboard_PipelineByUnit_View]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_PipelineByUnit_Count]", new SqlParameter[]{
                        new SqlParameter("@Unitid", UnitId)
                    });
                }
                

                if (list == null)
                {
                    list = new List<TblPipeline_ViewModels>();
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
        public IActionResult LoadDataTableFasilitasKredit(int Id, int Flag)
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

                List<TblFasilitasKreditDashboard_ViewModels> list = new List<TblFasilitasKreditDashboard_ViewModels>();
                if (Flag == 1)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Solicite_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });
                }
                else if (Flag == 2)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Prospek_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Pipeline_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });
                }

                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FAQ_Count]", new SqlParameter[]{
                //        new SqlParameter("@Nama", NamaSearchParam)});

                if (list == null)
                {
                    list = new List<TblFasilitasKreditDashboard_ViewModels>();
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
