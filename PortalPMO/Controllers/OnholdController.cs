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
using TwoTierTemplate.Models;
using Microsoft.EntityFrameworkCore;
using TwoTierTemplate.Component;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoTierTemplate.Controllers
{
    public class OnholdController : Controller
    {
        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLogPipeline lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public OnholdController(IConfiguration config, dbPipelineContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _accessor = accessor;
            lastSession = new LastSessionLogPipeline(accessor, context, config);
        }
        public IActionResult Index()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }


            return View();
        }

        #region LoadData
        [HttpPost]
        public IActionResult LoadData()
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
                var NamaSearchParam = dict["columns[2][search][value]"];
                var KeteranganSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<InboxPipeline_ViewModels> list = new List<InboxPipeline_ViewModels>();
                var role = HttpContext.Session.GetString(SessionConstan.Session_Role_Id);

                list = StoredProcedureExecutor.ExecuteSPList<InboxPipeline_ViewModels>(_context, "sp_OnHold_View", new SqlParameter[]{
                        new SqlParameter("@NamaDebitur", NamaSearchParam),
                        new SqlParameter("@Cif", KeteranganSearchParam),
                        new SqlParameter("@RoleId", HttpContext.Session.GetString(SessionConstan.Session_Role_Id)),
                        new SqlParameter("@UserId", HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_OnHold_Count]", new SqlParameter[]{
                          new SqlParameter("@NamaDebitur", NamaSearchParam),
                        new SqlParameter("@Cif", KeteranganSearchParam),
                        new SqlParameter("@RoleId", HttpContext.Session.GetString(SessionConstan.Session_Role_Id)),
                        new SqlParameter("@UserId", HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id))});

                if (list == null)
                {
                    list = new List<InboxPipeline_ViewModels>();
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

        #region LoadDataTableFasilitas
        [HttpPost]
        public IActionResult LoadDataTableFasilitasKredit(string Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                if (Id == null)
                {
                    Id = "0";
                }

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

                list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.TblFasilitasKreditDashboard_ViewModels>(_context, "[sp_FasilitasKreditPipeline_View]", new SqlParameter[]{
                        new SqlParameter("@Id", Id),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});


                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FasilitasKredit_Count]", new SqlParameter[]{
                //        new SqlParameter("@Id", Id)});

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

        #region LoadDataChecking
        [HttpPost]
        public IActionResult LoadDataChecking(int Id)
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

                List<TwoTierTemplate.ViewModels.Checking_ViewModels> list = new List<TwoTierTemplate.ViewModels.Checking_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.Checking_ViewModels>(_context, "[sp_CekListChecking_GetDataById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});


                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FasilitasKredit_Count]", new SqlParameter[]{
                //        new SqlParameter("@Id", Id)});

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.Checking_ViewModels>();
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

        #region LoadDataDisclaimer
        [HttpPost]
        public IActionResult LoadDataDisclaimer(int Id)
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

                List<TwoTierTemplate.ViewModels.Disclaimer_ViewModels> list = new List<TwoTierTemplate.ViewModels.Disclaimer_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.Disclaimer_ViewModels>(_context, "[sp_CekListDisclaimer_GetDataById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});


                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FasilitasKredit_Count]", new SqlParameter[]{
                //        new SqlParameter("@Id", Id)});

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.Disclaimer_ViewModels>();
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

        #region LoadDataDisclaimer
        [HttpPost]
        public IActionResult LoadDataTableFile(int Id)
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
                //var sortColumn = dict["columns[" + dict["order[0][column]"] + "][data]"];
                //var sortColumnDir = dict["order[0][dir]"];
                //var KodeSearchParam = dict["columns[2][search][value]"];
                //var NamaSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<TwoTierTemplate.ViewModels.File_ViewModels> list = new List<TwoTierTemplate.ViewModels.File_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<TwoTierTemplate.ViewModels.File_ViewModels>(_context, "[sp_FileDisclaimer_GetList]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});


                //recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_FasilitasKredit_Count]", new SqlParameter[]{
                //        new SqlParameter("@Id", Id)});

                if (list == null)
                {
                    list = new List<TwoTierTemplate.ViewModels.File_ViewModels>();
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

        #region LoadDataRiwayatKomentar
        [HttpPost]
        public IActionResult LoadDataRiwayatKomentar(int Id)
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
                var NamaSearchParam = dict["columns[2][search][value]"];
                var KeteranganSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<RiwayatKomentarr_ViewModels> list = new List<RiwayatKomentarr_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<RiwayatKomentarr_ViewModels>(_context, "sp_LoadHistoryPipeline_View", new SqlParameter[]{
                        new SqlParameter("@PipelineId", Id),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_LoadHistoryPipeline_Count]", new SqlParameter[]{
                          new SqlParameter("@PipelineId", Id)});

                if (list == null)
                {
                    list = new List<RiwayatKomentarr_ViewModels>();
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

        #region Tracking
        [HttpPost]
        //public IEnumerable<Tracking_ViewModels> Tracking(int id)
        public IActionResult Tracking(int id)
        {

            try
            {
                var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                var draw = dict["draw"];

                List<Tracking_ViewModels> list = new List<Tracking_ViewModels>();
                List<Tracking_ViewModels> list2 = new List<Tracking_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<Tracking_ViewModels>(_context, "sp_FlowPipeline_View", new SqlParameter[]{
                        new SqlParameter("@ProspekId", id.ToString()),
                        new SqlParameter("@sortColumn", ""),
                        new SqlParameter("@sortColumnDir", ""),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 100)});

                list2 = StoredProcedureExecutor.ExecuteSPList<Tracking_ViewModels>(_context, "sp_LoadHistoryPipeline_View", new SqlParameter[]{
                        new SqlParameter("@PipelineId", id.ToString()),
                        new SqlParameter("@sortColumn", ""),
                        new SqlParameter("@sortColumnDir", ""),
                        new SqlParameter("@PageNumber", 1),
                        new SqlParameter("@RowsPage", 100)});

                if (list == null)
                {
                    list = new List<Tracking_ViewModels>();
                }


                if (list2 == null)
                {
                    list2 = new List<Tracking_ViewModels>();
                }

                var listFinal = list.Union(list2);


                if (listFinal == null)
                {
                    listFinal = new List<TwoTierTemplate.ViewModels.Tracking_ViewModels>();
                }

                //return listFinal;
                return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = listFinal.Count(), data = listFinal });
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        #endregion

        #region Edit Fasilitas Kredit
        public IActionResult EditFasilitasKredit(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            TblFasilitasKredit_ViewModels data = new TblFasilitasKredit_ViewModels();

            data = StoredProcedureExecutor.ExecuteSPSingle<TblFasilitasKredit_ViewModels>(_context, "sp_FasilitasKredit_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)});


            return PartialView("EditFasilitasKredit", data);

            //if (!lastSession.Update())
            //{
            //    return RedirectToAction("Login", "Login", new { a = true });

            //}

            //return PartialView("EditFasilitasKredit");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(TblFasilitasKredit_ViewModels model, int idprospek)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                TblFasilitasKredit data = _context.TblFasilitasKredit.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID

                data.MaksimumKredit = Decimal.Parse(model.MaksimumKredit.Replace(".", ""));
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedTime = DateTime.Now;


                TblFasilitasKreditLog log = new TblFasilitasKreditLog();
                TblAssignPrescreening ap = _context.TblAssignPrescreening.Where(m => m.ProspekId == idprospek).FirstOrDefault();


                if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusBisnis)
                {
                    data.FlowStatusId = 4;
                    data.FlowStatusName = _context.TblLookup.Where(m => m.Value == log.FlowStatusId && m.Type == "FlowDataFK").Select(m => m.Name).FirstOrDefault();
                    log.FlowStatusId = 4;
                    log.FlowStatusName = _context.TblLookup.Where(m => m.Value == log.FlowStatusId && m.Type == "FlowDataFK").Select(m => m.Name).FirstOrDefault();
                }
                else if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusResiko)
                {
                    data.FlowStatusId = 5;
                    data.FlowStatusName = _context.TblLookup.Where(m => m.Value == log.FlowStatusId && m.Type == "FlowDataFK").Select(m => m.Name).FirstOrDefault();
                    log.FlowStatusId = 5;
                    log.FlowStatusName = _context.TblLookup.Where(m => m.Value == log.FlowStatusId && m.Type == "FlowDataFK").Select(m => m.Name).FirstOrDefault();
                }
                log.IdDebitur = data.IdDebitur;
                log.FasilkreditId = data.Id;
                log.IdSolicite = data.IdSolicite;
                log.KategoriLeadsId = data.KategoriLeadsId;
                log.DeskripsiKreditId = data.DeskripsiKreditId;
                log.JenisPengajuanId = data.JenisPengajuanId;
                log.TypeKreditId = data.TypeKreditId;
                log.TypeKreditName = data.TypeKreditName;
                log.IdTingkatKomite = data.IdTingkatKomite;
                log.TingkatKomite = data.TingkatKomite;
                log.IdJenisFasilitas = data.IdJenisFasilitas;
                log.JenisFasilitas = data.JenisFasilitas;
                log.IdValuta = data.IdValuta;
                log.Valuta = data.Valuta;
                log.MaksimumKredit = data.MaksimumKredit;
                log.MaksimumKreditIdr = data.MaksimumKreditIdr;
                log.Tujuan = data.Tujuan;
                log.CreatedById = data.CreatedById;
                log.UpdatedById = data.UpdatedById;
                log.CreatedTime = data.CreatedTime;
                log.UpdatedTime = data.UpdatedTime;
                log.IsActive = data.IsActive;
                //log.FlowStatusId = data.FlowStatusId;
                //log.FlowStatusName = data.FlowStatusName;
                log.ActionId = 2;
                log.ActionName = "Update";

                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                _context.TblFasilitasKreditLog.Add(log);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception e)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        [HttpPost]
        public ActionResult Approve(String catatan, int id)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                TblPipeline data = _context.TblPipeline.Where(m => m.Id == id).FirstOrDefault(); // Ambil data sesuai dengan ID
                TblAssignPrescreening ap = _context.TblAssignPrescreening.Where(m => m.ProspekId == data.ProspekId).FirstOrDefault();
                TblPipelineActivityLog al = _context.TblPipelineActivityLog.Where(m => m.PkStep == data.ProspekId && m.Step == 3).FirstOrDefault();
                TblFlowPipeline log = new TblFlowPipeline();

                if (al != null)
                {
                    al.EndStepTime = DateTime.Now;
                    _context.Entry(al).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                var role = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var pemutus = ap.IdPemutusBisnis;
                if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusBisnis)
                {
                    data.IsApproveUnitBisnis = true;
                    log.ActionName = "Approve Unit Bisnis";
                    log.ActionId = 1;
                }
                else if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusResiko)
                {
                    data.IsApproveUnitResiko = true;
                    log.ActionName = "Approve Unit Resiko";
                    log.ActionId = 2;
                }
                else
                {
                    data.IsApproveUnitResiko = true;
                    log.ActionName = "Approve Unit Resiko";
                    log.ActionId = 2;
                }
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedByName = HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai);
                data.UpdatedTime = DateTime.Now;
                data.StatusId = 10;
                data.StatusName = _context.TblLookup.Where(m => m.Value == data.StatusId && m.Type == "StatusData").Select(m => m.Name).FirstOrDefault();


                log.IdPipeline = data.Id;
                log.IdProspek = data.ProspekId;

                log.IdRolePengirim = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                log.Komentar = catatan;
                log.CreatedTime = data.CreatedTime;
                log.UpdatedTime = data.UpdatedTime;
                log.CreatedById = data.CreatedById;
                log.UpdatedById = data.UpdatedById;
                log.IsActive = true;

                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                _context.TblFlowPipeline.Add(log);
                _context.SaveChanges();


                return Content("");
            }
            catch (Exception e)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        [HttpPost]
        public ActionResult Reject(String catatan, int id)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                TblPipeline data = _context.TblPipeline.Where(m => m.Id == id).FirstOrDefault(); // Ambil data sesuai dengan ID
                TblAssignPrescreening ap = _context.TblAssignPrescreening.Where(m => m.ProspekId == data.ProspekId).FirstOrDefault();
                TblPipelineActivityLog al = _context.TblPipelineActivityLog.Where(m => m.PkStep == data.ProspekId && m.Step == 3).FirstOrDefault();
                TblFlowPipeline log = new TblFlowPipeline();

                if (al != null)
                {
                    al.EndStepTime = DateTime.Now;
                    _context.Entry(al).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                var role = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var pemutus = ap.IdPemutusBisnis;
                if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusBisnis)
                {
                    data.IsApproveUnitBisnis = false;
                    log.ActionName = "Reject Unit Bisnis";
                    log.ActionId = 3;
                }
                else if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusResiko)
                {
                    data.IsApproveUnitResiko = false;
                    log.ActionName = "Reject Unit Resiko";
                    log.ActionId = 4;
                }
                else
                {
                    data.IsApproveUnitResiko = false;
                    log.ActionName = "Reject Unit Resiko";
                    log.ActionId = 4;
                }
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedByName = HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai);
                data.UpdatedTime = DateTime.Now;
                data.StatusId = 11;
                data.StatusName = _context.TblLookup.Where(m => m.Value == data.StatusId && m.Type == "StatusData").Select(m => m.Name).FirstOrDefault();


                log.IdPipeline = data.Id;
                log.IdProspek = data.ProspekId;

                log.IdRolePengirim = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                log.Komentar = catatan;
                log.CreatedTime = data.CreatedTime;
                log.UpdatedTime = data.UpdatedTime;
                log.CreatedById = data.CreatedById;
                log.UpdatedById = data.UpdatedById;
                log.IsActive = true;

                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                _context.TblFlowPipeline.Add(log);
                _context.SaveChanges();


                return Content("");
            }
            catch (Exception e)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        [HttpPost]
        public ActionResult Pending(String catatan, int id)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                TblPipeline data = _context.TblPipeline.Where(m => m.Id == id).FirstOrDefault(); // Ambil data sesuai dengan ID
                TblAssignPrescreening ap = _context.TblAssignPrescreening.Where(m => m.ProspekId == data.ProspekId).FirstOrDefault();
                TblPipelineActivityLog al = _context.TblPipelineActivityLog.Where(m => m.PkStep == data.ProspekId && m.Step == 3).FirstOrDefault();
                TblFlowPipeline log = new TblFlowPipeline();

                if (al != null)
                {
                    al.EndStepTime = DateTime.Now;
                    _context.Entry(al).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                var role = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var pemutus = ap.IdPemutusBisnis;
                if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusBisnis)
                {
                    data.IsApproveUnitBisnis = null;
                    log.ActionName = "Pending Unit Bisnis";
                }
                if (int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)) == ap.IdPemutusResiko)
                {
                    data.IsApproveUnitResiko = null;
                    log.ActionName = "Pending Unit Resiko";
                }
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedByName = HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai);
                data.UpdatedTime = DateTime.Now;
                data.StatusId = 12;
                data.StatusName = _context.TblLookup.Where(m => m.Value == data.StatusId && m.Type == "StatusData").Select(m => m.Name).FirstOrDefault();


                log.IdPipeline = data.Id;
                log.IdProspek = data.ProspekId;
                log.ActionId = 5;
                log.IdRolePengirim = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                log.Komentar = catatan;
                log.CreatedTime = data.CreatedTime;
                log.UpdatedTime = data.UpdatedTime;
                log.CreatedById = data.CreatedById;
                log.UpdatedById = data.UpdatedById;
                log.IsActive = true;

                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                _context.TblFlowPipeline.Add(log);
                _context.SaveChanges();


                return Content("");
            }
            catch (Exception e)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        #region View
        public ActionResult View(int LeadsId, int KategoriLeadsId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DetailPipeline_ViewModels data = new DetailPipeline_ViewModels();

            data = StoredProcedureExecutor.ExecuteSPSingle<DetailPipeline_ViewModels>(_context, "sp_InfoDebitur_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", LeadsId),
                        new SqlParameter("@Kategori", KategoriLeadsId)});


            return PartialView("_View", data);
        }

        #endregion

        #region ViewDisclaimer
        public ActionResult ViewDisclaimer()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }



            return PartialView("_ViewDisclaimer");
        }

        #endregion

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
    }
}
