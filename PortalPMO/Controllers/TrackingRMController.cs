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
    public class TrackingRMController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public TrackingRMController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
            //var WilayahId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id));


            ViewBag.IdDropdwonDefault = PegawaiId;
            ViewBag.TextDropdwonDefault = HttpContext.Session.GetString(SessionConstan.Session_Role_Nama_Unit) + " - " + HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai) + " (" + HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai) + ")";

            ////ViewBag.DropwdownPegawai = new SelectList(Utility.SelectDataPegawai(PegawaiId, _context).ToList(), "id", "text", PegawaiId);

            //ViewBag.DropwdownUnit = new SelectList(Utility.SelectDataUnitByWilayahId(WilayahId, _context).ToList(), "id", "text", WilayahId);
            //ViewBag.DropwdownRM = new SelectList(Utility.SelectDataRM(UnitId, _context).ToList(), "id", "text", UnitId);

            return View();
        }

        #region Get All Data Beranda
        public JsonResult GetAllDataPendingTask(string userId)
        {
            lastSession.Update();
            Dashboard_ViewModels data = new Dashboard_ViewModels();
            var PegawaiId = HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id);

            if (userId == null)
            {
                // Card Summary
                List<ExecutiveSummary> listExSummary = _context.ExecuteSPList<ExecutiveSummary>("[sp_Dashboard_GapByRM]", new SqlParameter[]{
                    new SqlParameter("@RmId", PegawaiId)
                });

                // Card Per Detail
                List<Tbl_Solicite> list1 = _context.ExecuteSPList<Tbl_Solicite>("[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", PegawaiId)
                });

                List<Tbl_Prospek> list2 = _context.ExecuteSPList<Tbl_Prospek>("[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", PegawaiId)
                });

                List<Tbl_Pipeline> list3 = _context.ExecuteSPList<Tbl_Pipeline>("[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", PegawaiId)
                });
                data.ExecutiveSummary = listExSummary;
                data.tblSolicite = list1;
                data.tblProspek = list2;
                data.tblPipeline = list3;
            }
            else { 
                // Card Summary
                List<ExecutiveSummary> listExSummary = _context.ExecuteSPList<ExecutiveSummary>("[sp_Dashboard_GapByRM]", new SqlParameter[]{
                    new SqlParameter("@RmId", userId)
                });

                // Card Per Detail
                List<Tbl_Solicite> list1 = _context.ExecuteSPList<Tbl_Solicite>("[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", userId)
                });

                List<Tbl_Prospek> list2 = _context.ExecuteSPList<Tbl_Prospek>("[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", userId)
                });

                List<Tbl_Pipeline> list3 = _context.ExecuteSPList<Tbl_Pipeline>("[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                    new SqlParameter("@RmId", userId)
                });
                data.ExecutiveSummary = listExSummary;
                data.tblSolicite = list1;
                data.tblProspek = list2;
                data.tblPipeline = list3;
            }

            return Json(data);
        }
        #endregion

        #region LoadDataTableSolicite
        [HttpPost]
        public IActionResult LoadDataTableSolicite(string UserId)
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

                var PegawaiId= HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id);
                List<TblSolicite_ViewModels> list = new List<TblSolicite_ViewModels>();

                if (UserId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });
                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId)
                    });
                }
                else {
                     list = StoredProcedureExecutor.ExecuteSPList<TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId)
                    });
                }
                

                if (list == null)
                {
                    list = new List<TblSolicite_ViewModels>();
                    recordsTotal = 0;
                }
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = list });
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region LoadDataTableProspek
        [HttpPost]
        public IActionResult LoadDataTableProspek(string UserId)
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

                var PegawaiId = HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id);

                List<TblProspek_ViewModels> list = new List<TblProspek_ViewModels>();
                if (UserId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblProspek_ViewModels>(_context, "[sp_Dashboard_ProspekByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId)
                    });
                }
                else {
                    list = StoredProcedureExecutor.ExecuteSPList<TblProspek_ViewModels>(_context, "[sp_Dashboard_ProspekByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId)
                    });
                }
                

                if (list == null)
                {
                    list = new List<TblProspek_ViewModels>();
                    recordsTotal = 0;
                }
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = list });
             
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region LoadDataTablePipeline
        [HttpPost]
        public IActionResult LoadDataTablePipeline(string UserId)
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

                var PegawaiId = HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id);

                List<TblPipeline_ViewModels> list = new List<TblPipeline_ViewModels>();

                if (UserId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TblPipeline_ViewModels>(_context, "[sp_Dashboard_PipelineByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", PegawaiId)
                    });
                }
                else {
                    list = StoredProcedureExecutor.ExecuteSPList<TblPipeline_ViewModels>(_context, "[sp_Dashboard_PipelineByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", UserId)
                    });
                }
                

                if (list == null)
                {
                    list = new List<TblPipeline_ViewModels>();
                    recordsTotal = 0;
                }
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = list });
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

                if (list == null)
                {
                    list = new List<TblFasilitasKreditDashboard_ViewModels>();
                    recordsTotal = 0;
                }
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = list });
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion
    }
}
