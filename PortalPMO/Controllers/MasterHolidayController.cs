using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PortalPMO.Component;
using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;


namespace PortalPMO.Controllers
{
    public class MasterHolidayController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        public MasterHolidayController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            lastSession = new LastSessionLog(accessor, context, config);
            accessSecurity = new AccessSecurity(accessor, context, config);
        }



        public IActionResult Index()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");
            string Path = location.AbsolutePath;

            if (!accessSecurity.IsGetAccess(".." + Path))
            {
                return RedirectToAction("NotAccess", "Error");
            }

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
                var NamaSearchParam = dict["columns[2][search][value]"];
                var TanggalSearchParam = dict["columns[3][search][value]"];
                var TanggalAwal = "";
                var TanggalAkhir = "";

                if (TanggalSearchParam != null && TanggalSearchParam != "")
                {
                    var splitTanggal = TanggalSearchParam.Replace(" ", "").Split("-");

                    TanggalAwal  = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    TanggalAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<DataMasterHoliday_ViewModels> list = new List<DataMasterHoliday_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DataMasterHoliday_ViewModels>(_context, "[sp_MasterHoliday_View]", new SqlParameter[]{
                        new SqlParameter("@Nama", NamaSearchParam),
                        new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir),

                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_MasterHoliday_Count]", new SqlParameter[]{
                       new SqlParameter("@TanggalAwal", TanggalAwal),
                        new SqlParameter("@TanggalAkhir", TanggalAkhir)
                });

                if (list == null)
                {
                    list = new List<DataMasterHoliday_ViewModels>();
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

        #region Create
        public ActionResult Create()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            

            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(DataMasterHoliday_ViewModels model)
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
                    TblHoliday data = new TblHoliday();


                    if (model.Tanggal != null)
                    {
                        data.Tanggal = DateTime.ParseExact(model.Tanggal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    data.Nama = model.Nama;
                    data.Keterangan = model.Keterangan;
                    data.IsActive = model.IsActive;
                    data.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.CreatedTime = DateTime.Now;
                    _context.TblHoliday.Add(data);
                    _context.SaveChanges();

                    trx.Complete();
                }

                

                return Content("");
            }
            catch(Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataMasterHoliday_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<DataMasterHoliday_ViewModels>(_context, "sp_MasterHoliday_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)});
            if (data == null)
            {
                data = new DataMasterHoliday_ViewModels();
            }

            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(DataMasterHoliday_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                TblHoliday data = _context.TblHoliday.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID


                if (model.Tanggal != null)
                {
                    data.Tanggal = DateTime.ParseExact(model.Tanggal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                data.Nama = model.Nama;
                data.Keterangan = model.Keterangan;
                data.IsActive = model.IsActive;
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedTime = DateTime.Now;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion


        #region View
        public ActionResult View(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataMasterHoliday_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<DataMasterHoliday_ViewModels>(_context, "sp_MasterHoliday_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)});
            if (data == null)
            {
                data = new DataMasterHoliday_ViewModels();
            }


            return PartialView("_View", data);
        }

        #endregion

        #region Delete
        public ActionResult Delete(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblHoliday> Transaksis = _context.TblHoliday.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblHoliday data = _context.TblHoliday.Find(Transaksis[i].Id);
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
    }
}
