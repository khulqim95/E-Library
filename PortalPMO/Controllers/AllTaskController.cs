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
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PortalPMO.Controllers
{
    public class AllTaskController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public AllTaskController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                var ProjectNoSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));


                List<DetailProjectMember_ViewModels> list = new List<DetailProjectMember_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DetailProjectMember_ViewModels>(_context, "[sp_ProjectMember_View]", new SqlParameter[]{
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@RoleIdLogin", RoleId),
                        new SqlParameter("@UnitIdLogin", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_ProjectMember_Count]", new SqlParameter[]{
                       new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                         new SqlParameter("@RoleIdLogin", RoleId),
                        new SqlParameter("@UnitIdLogin", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId)});

                if (list == null)
                {
                    list = new List<DetailProjectMember_ViewModels>();
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

        #region Update Progress
        public ActionResult UpdateProgress(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            DetailProjectMember_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<DetailProjectMember_ViewModels>(_context, "[sp_ProjectMember_GetDataById]", new SqlParameter[]{
                       new SqlParameter("@Id", Id)});

            return PartialView("_UpdateProgress", data);
        }

        #endregion

        #region Create Progress Pekerjaan
        public ActionResult UpdateProgressPekerjaan()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_ModalDetailProgress");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreateProgressPekerjaan(ProgressKerjaMember_ViewModels model)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    TblProjectMemberProgressKerja data = new TblProjectMemberProgressKerja();


                    if (model.Tanggal != null)
                    {
                        var splitTanggal = model.Tanggal.Replace(" ", "").Split("-");

                        data.TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        data.TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    data.ProjectMemberId = model.ProjectMemberId;
                    data.Judul = model.Judul;
                    data.Deskripsi = model.Deskripsi;
                    data.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.CreatedTime = DateTime.Now;
                    _context.TblProjectMemberProgressKerja.Add(data);
                    _context.SaveChanges();

                    trx.Complete();
                }
                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        #endregion

        #region Update Progress Pekerjaan
        public ActionResult EditUpdateProgressPekerjaan(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ProgressKerjaMember_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<ProgressKerjaMember_ViewModels>(_context, "[sp_ProjectMember_ProgressKerja_GetDataById]", new SqlParameter[]{
                       new SqlParameter("@Id", id)});

            return PartialView("_ModalDetailProgress", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEditProgressPekerjaan(ProgressKerjaMember_ViewModels model)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    TblProjectMemberProgressKerja data = _context.TblProjectMemberProgressKerja.Where(m => m.Id == model.Id).FirstOrDefault();


                    if (model.Tanggal != null)
                    {
                        var splitTanggal = model.Tanggal.Replace(" ", "").Split("-");

                        data.TanggalAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        data.TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    data.ProjectMemberId = model.ProjectMemberId;
                    data.Judul = model.Judul;
                    data.Deskripsi = model.Deskripsi;
                    data.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.CreatedTime = DateTime.Now;
                    _context.TblProjectMemberProgressKerja.Update(data);
                    _context.SaveChanges();

                    trx.Complete();
                }
                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        #endregion

        #region View Progress Pekerjaan
        public ActionResult ViewUpdateProgressPekerjaan(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ProgressKerjaMember_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<ProgressKerjaMember_ViewModels>(_context, "[sp_ProjectMember_ProgressKerja_GetDataById]", new SqlParameter[]{
                       new SqlParameter("@Id", id)});

            return PartialView("_ModalDetailProgress", data);
        }


        #endregion

        #region Delete Progress
        public ActionResult DeleteProgress(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectMemberProgressKerja> Transaksis = _context.TblProjectMemberProgressKerja.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblProjectMemberProgressKerja data = _context.TblProjectMemberProgressKerja.Find(Transaksis[i].Id);
                    data.IsDeleted = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
                    data.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.DeletedTime = System.DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }
        #endregion

        #region Update Task Done
        public ActionResult UpdateTaskDone(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }

            DetailProjectMember_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<DetailProjectMember_ViewModels>(_context, "[sp_ProjectMember_GetDataById]", new SqlParameter[]{
                       new SqlParameter("@Id", Id)});

            return PartialView("_ModalTaskDone", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitUpdateTaskDone(DetailProjectMember_ViewModels model)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    TblProjectMember data = _context.TblProjectMember.Where(m => m.Id == model.Id).FirstOrDefault();
                    data.TanggalPenyelesaian = DateTime.ParseExact(model.TanggalPenyelesaian, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    data.KeteranganPenyelesaian = model.KeteranganPenyelesaian;
                    data.IsDone = true;
                    data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.UpdatedTime = DateTime.Now;
                    _context.TblProjectMember.Update(data);
                    _context.SaveChanges();

                    trx.Complete();
                }
                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        #endregion
    }
}
