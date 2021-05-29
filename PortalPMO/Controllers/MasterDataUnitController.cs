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
using PortalPMO.Component;
using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;


namespace PortalPMO.Controllers
{
    public class MasterDataUnitController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        public MasterDataUnitController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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

                var TypeUnitSearchParam = dict["columns[2][search][value]"];
                var KodeUnitSearchParam = dict["columns[3][search][value]"];
                var NamaUnitSearchParam = dict["columns[4][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<MasterDataUnit_ViewModels> list = new List<MasterDataUnit_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<MasterDataUnit_ViewModels>(_context, "[sp_MasterDataUnit_View]", new SqlParameter[]{
                       new SqlParameter("@TypeUnit", TypeUnitSearchParam),
                        new SqlParameter("@KodeUnit", KodeUnitSearchParam),
                        new SqlParameter("@NamaUnit", NamaUnitSearchParam),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_MasterDataUnit_Count]", new SqlParameter[]{
                          new SqlParameter("@TypeUnit", TypeUnitSearchParam),
                        new SqlParameter("@KodeUnit", KodeUnitSearchParam),
                        new SqlParameter("@NamaUnit", NamaUnitSearchParam)});

                if (list == null)
                {
                    list = new List<MasterDataUnit_ViewModels>();
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
            ViewBag.TypeUnit = new SelectList(Utility.SelectLookup("TypeUnit", _context), "Value", "Name");


            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(TblUnit model)
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
                    model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    model.IsDelete = false;
                    model.CreatedTime = DateTime.Now;
                    _context.TblUnit.Add(model);
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
            TblUnit data = _context.TblUnit.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblUnit();
            }

            ViewBag.TypeUnit = new SelectList(Utility.SelectLookup("TypeUnit", _context).ToList(), "Value", "Name", data.Type);
            if (data.ParentId != null)
            {
                ViewBag.ParentUnit = new SelectList(Utility.SelectDataUnit(data.ParentId, _context).ToList(), "id", "text", data.ParentId);
            }
            else
            {
                ViewBag.ParentUnit = new SelectList("", "");
            }

            if (data.WilayahId != null)
            {
                ViewBag.Wilayah = new SelectList(Utility.SelectDataUnit(data.WilayahId, _context).ToList(), "id", "text", data.WilayahId);
            }
            else
            {
                ViewBag.Wilayah = new SelectList("", "");
            }

            if (data.DivisiId != null)
            {
                ViewBag.Divisi = new SelectList(Utility.SelectDataUnit(data.DivisiId, _context).ToList(), "id", "text", data.DivisiId);
            }
            else
            {
                ViewBag.Divisi = new SelectList("", "");
            }

            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(TblUnit model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                TblUnit data = _context.TblUnit.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID
                data.Type = model.Type;
                data.Code = model.Code;
                data.Name = model.Name;
                data.ShortName = model.ShortName;
                data.ParentId = model.ParentId;
                data.WilayahId = model.WilayahId;
                data.DivisiId = model.DivisiId;
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
            TblUnit data = _context.TblUnit.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblUnit();
            }

            ViewBag.TypeUnit = new SelectList(Utility.SelectLookup("TypeUnit", _context).ToList(), "Value", "Name", data.Type);
            if (data.ParentId != null)
            {
                ViewBag.ParentUnit = new SelectList(Utility.SelectDataUnit(data.ParentId, _context).ToList(), "id", "text", data.ParentId);
            }
            else
            {
                ViewBag.ParentUnit = new SelectList("", "");
            }

            if (data.WilayahId != null)
            {
                ViewBag.Wilayah = new SelectList(Utility.SelectDataUnit(data.WilayahId, _context).ToList(), "id", "text", data.WilayahId);
            }
            else
            {
                ViewBag.Wilayah = new SelectList("", "");
            }

            if (data.DivisiId != null)
            {
                ViewBag.Divisi = new SelectList(Utility.SelectDataUnit(data.DivisiId, _context).ToList(), "id", "text", data.DivisiId);
            }
            else
            {
                ViewBag.Divisi = new SelectList("", "");
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

                List<TblUnit> Transaksis = _context.TblUnit.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblUnit data = _context.TblUnit.Find(Transaksis[i].Id);
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
