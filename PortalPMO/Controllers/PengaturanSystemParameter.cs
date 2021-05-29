using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortalPMO.Component;
using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;

namespace PortalPMO.Controllers
{
    public class PengaturanSystemParameter : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        public PengaturanSystemParameter(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            lastSession = new LastSessionLog(accessor, context, config);
            accessSecurity = new AccessSecurity(accessor, context, config);
        }

        public IActionResult Index()
        {
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
                var KeySearchParam = dict["columns[1][search][value]"];
                var ValueSearchParam = dict["columns[2][search][value]"];
                var KeteranganSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<TblSystemParameter_ViewModels> list = new List<TblSystemParameter_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<TblSystemParameter_ViewModels>(_context, "[sp_MasterSystemParameter_View]", new SqlParameter[]{
                        new SqlParameter("@Key", KeySearchParam),
                        new SqlParameter("@Value", ValueSearchParam),
                        new SqlParameter("@Keterangan", KeteranganSearchParam),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                });

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_MasterSystemParameter_Count]", new SqlParameter[]{
                        new SqlParameter("@Key", KeySearchParam),
                        new SqlParameter("@Value", ValueSearchParam),
                        new SqlParameter("@Keterangan", KeteranganSearchParam)
                
                });

                if (list == null)
                {
                    list = new List<TblSystemParameter_ViewModels>();
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
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(TblSystemParameter model)
        {
            try
            {
                //var url = GetConfig.AppSetting["baseApi"] + GetConfig.AppSetting["urlapi:MasterSystemParameter:create"];
                //(bool resultApi, string result) = RequestToAPI.PostRequestToWebApi(url, model, HttpContext.Session.GetString(SessionConstan.jwt_Token));
                //if (resultApi && !string.IsNullOrEmpty(result))
                //{
                //    var data = JsonConvert.DeserializeObject<ServiceResult<object>>(result);
                //    if (data.Code == 1)
                //        return Content("");
                //    else
                //        return Content(data.Code + ": " + data.Message);
                //}
                //else
                //{
                //    return Content("Failed Save");
                //}

                using (TransactionScope trx = new TransactionScope())
                {
                    model.IsDelete = false;
                    model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    model.CreatedTime = DateTime.Now;
                    _context.TblSystemParameter.Add(model);
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

        #region Edit
        public ActionResult Edit(int id)
        {
            TblSystemParameter data = _context.TblSystemParameter.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblSystemParameter();
            }

            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(TblSystemParameter model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                TblSystemParameter data = _context.TblSystemParameter.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID
                data.Value = model.Value;
                data.Key = model.Key;
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
            TblSystemParameter data = _context.TblSystemParameter.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblSystemParameter();
            }

            return PartialView("_View", data);
        }

        #endregion

        #region Delete
        public ActionResult Delete(string Ids)
        {
            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblSystemParameter> Transaksis = _context.TblSystemParameter.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblSystemParameter data = _context.TblSystemParameter.Find(Transaksis[i].Id);
                    data.IsDelete = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
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
