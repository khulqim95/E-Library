using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortalPMO.Component;
using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;

namespace PortalPMO.Controllers
{
    public class BookShelfController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        IHttpContextAccessor _accessor;
        private readonly HttpClient _client;
        public BookShelfController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _client = new HttpClient();
            _accessor = accessor;
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
        public async Task<IActionResult> LoadData()
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
                var KodeSearchParam = dict["columns[3][search][value]"];
                var KeteranganSearchParam = dict["columns[4][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<MasterBook_ViewModels> list = new List<MasterBook_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<MasterBook_ViewModels>(_context, "[sp_BookShelf_View]", new SqlParameter[]{
                        new SqlParameter("@Kode", KodeSearchParam),
                        new SqlParameter("@Nama", NamaSearchParam),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_BookShelf_Count]", new SqlParameter[]{
                        new SqlParameter("@Kode", KodeSearchParam),
                        new SqlParameter("@NamA", NamaSearchParam)});

                if (list == null)
                {
                    list = new List<MasterBook_ViewModels>();
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
        public async Task<ActionResult> SubmitCreate(TblMasterBook model, IFormFile file)
        {
            string BaseDirectory = System.IO.Directory.GetCurrentDirectory() + "\\" + "FileRepository";
            string FileName = Path.GetFileName(file.FileName);
            string FileExt = Path.GetExtension(FileName);

            try
            {
                using (TransactionScope trx = new TransactionScope())
                {
                    model.IsActive = true;
                    //model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    model.CreatedTime = DateTime.Now;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        model.Picture = "data:image/png;base64, " + Convert.ToBase64String(fileBytes);

                        // act on the Base64 data
                    }
                    _context.TblMasterBook.Add(model);
                    _context.SaveChanges();

                    trx.Complete();
                }

                if (!Directory.Exists(BaseDirectory))
                {
                    DirectoryInfo di = Directory.CreateDirectory(BaseDirectory);
                }

                //using (var stream = new FileStream(model.Picture, FileMode.Create)) //ini untuk menyimpan file foto saat edit dan create

                //    if (!Directory.Exists(model.Picture))
                //    {
                //        try
                //        {
                //            await file.CopyToAsync(stream);
                //            //Save Physical File
                //            //System.IO.File.Copy(FileName, uploadfolder);
                //        }
                //        catch (IOException) { }
                //    }
                //    else
                //    {
                //        System.IO.File.Delete(model.Picture);
                //        await file.CopyToAsync(stream);
                //        //Save Physical File
                //        //System.IO.File.Copy(FileName, uploadfolder);
                //    }                

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
            TblMasterBook data = _context.TblMasterBook.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblMasterBook();
            }

            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> SubmitBorrowed(TblMasterBook model, DateTime BorrowDate, DateTime FinishDate)
        {       
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                var userId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                TblBorrowedBook databook = new TblBorrowedBook();
                TblMasterBook data = new TblMasterBook();
                databook = _context.TblBorrowedBook.Where(m => m.IdBook == model.Id && m.IdUser == userId).FirstOrDefault(); // Ambil data sesuai dengan ID

                if (databook == null)
                {
                    data = _context.TblMasterBook.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID                
                    databook = new TblBorrowedBook();

                    databook.IdBook = data.Id;
                    databook.IdUser = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    databook.BorrowDate = BorrowDate;
                    databook.FinishDate = FinishDate;
                    databook.CreatedTime = DateTime.Now;
                    databook.IsActive = true;
                    _context.TblBorrowedBook.Add(databook);
                    _context.SaveChanges();

                    data.IsBorrowed = true;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else 
                {
                    data = _context.TblMasterBook.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID                

                    databook.IdBook = data.Id;
                    databook.IdUser = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    databook.BorrowDate = BorrowDate;
                    databook.FinishDate = FinishDate;
                    databook.CreatedTime = DateTime.Now;
                    databook.IsActive = true;
                    _context.Entry(databook).State = EntityState.Modified;
                    _context.SaveChanges();

                    data.IsBorrowed = true;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                

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
            TblMasterBook data = _context.TblMasterBook.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblMasterBook();
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

                List<TblMasterBook> Transaksis = _context.TblMasterBook.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblMasterBook data = _context.TblMasterBook.Find(Transaksis[i].Id);
                    data.IsActive = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
                    //data.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.UpdatedTime = System.DateTime.Now;
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