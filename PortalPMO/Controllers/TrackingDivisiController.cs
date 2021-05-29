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

namespace TwoTierTemplate.Controllers
{
    public class TrackingDivisiController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";

        public TrackingDivisiController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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


            ViewBag.IdDropdwonDefault = PegawaiId;
            ViewBag.TextDropdwonDefault = HttpContext.Session.GetString(SessionConstan.Session_Role_Nama_Unit) + " - " + HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai) + " (" + HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai) + ")";

            //ViewBag.DropwdownPegawai = new SelectList(Utility.SelectDataPegawai(PegawaiId, _context).ToList(), "id", "text", PegawaiId);

            ViewBag.DropwdownUnit = new SelectList(Utility.SelectDataUnitByWilayahId(WilayahId, _context).ToList(), "id", "text", WilayahId);
            ViewBag.DropwdownRM = new SelectList(Utility.SelectDataRM(UnitId, _context).ToList(), "id", "text", UnitId);

            return View();
        }

        #region Get All Data Beranda
        public JsonResult GetAllDataPendingTask(string WilayahId)
        {
            lastSession.Update();
            TwoTierTemplate.ViewModels.Dashboard_ViewModels data = new TwoTierTemplate.ViewModels.Dashboard_ViewModels();
            List<Tbl_Solicite> list1 = new List<Tbl_Solicite>();
            List<Tbl_Prospek> list2 = new List<Tbl_Prospek>();
            List<Tbl_Pipeline> list3 = new List<Tbl_Pipeline>();

            var Wilayahid = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);

            if (WilayahId == null)
            {
                list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", Wilayahid)
            });

                list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", Wilayahid)
            });

                list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", Wilayahid)
            });

                data.tblSolicite = list1;
                data.tblProspek = list2;
                data.tblPipeline = list3;
            }
            else {
                list1 = StoredProcedureExecutor.ExecuteSPList<Tbl_Solicite>(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", WilayahId)
            });

                list2 = StoredProcedureExecutor.ExecuteSPList<Tbl_Prospek>(_context, "[sp_Dashboard_ProspekByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", WilayahId)
            });

                list3 = StoredProcedureExecutor.ExecuteSPList<Tbl_Pipeline>(_context, "[sp_Dashboard_PipelineByRM_Count]", new SqlParameter[]{
                new SqlParameter("@RmId", WilayahId)
            });

                data.tblSolicite = list1;
                data.tblProspek = list2;
                data.tblPipeline = list3;
            }
            
            return Json(data);
        }
        #endregion

        #region LoadDataTableSolicite
        [HttpPost]
        public IActionResult LoadDataTableSolicite(string WilayahId)
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

                var Wilayahid = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);
                List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
                if (WilayahId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId)
                    });
                }

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
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
        public IActionResult LoadDataTableProspek(string WilayahId)
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

                var Wilayahid = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);
                List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
                if (WilayahId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId)
                    });
                }

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
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
        public IActionResult LoadDataTablePipeline(string WilayahId)
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


                var Wilayahid = HttpContext.Session.GetString(SessionConstan.Session_Wilayah_Id);
                List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
                if (WilayahId == null)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", Wilayahid)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>(_context, "[sp_Dashboard_SoliciteByRM_View]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                    });

                    recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_Dashboard_SoliciteByRM_Count]", new SqlParameter[]{
                        new SqlParameter("@RmId", WilayahId)
                    });
                }

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.TblSolicite_ViewModels>();
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

                List<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels> list = new List<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>();

                if (Flag == 1)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Solicite_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 10)
                    });
                }
                else if (Flag == 2)
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Prospek_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 10)
                    });
                }
                else
                {
                    list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_Dashboard_Pipeline_GetDataFasilitasKreditByRMId]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", "Id"),
                        new SqlParameter("@sortColumnDir", "desc"),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 10)
                    });
                }
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
