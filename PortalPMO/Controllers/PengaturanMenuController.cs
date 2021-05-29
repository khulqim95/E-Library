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
    public class PengaturanMenuController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        public PengaturanMenuController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                var TypeSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];
                var ParentSearchParam = dict["columns[4][search][value]"];
                var RoleSearchParam = dict["columns[7][search][value]"];


                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var Role_Id = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));


                List<PengaturanMenu_ViewModels> list = new List<PengaturanMenu_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<PengaturanMenu_ViewModels>(_context, "[sp_PengaturanMenu_View]", new SqlParameter[]{
                        new SqlParameter("@Name", NamaSearchParam),
                        new SqlParameter("@Type", TypeSearchParam),
                        new SqlParameter("@Role", RoleSearchParam),
                        new SqlParameter("@Parent", ParentSearchParam),


                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_PengaturanMenu_Count]", new SqlParameter[]{
                         new SqlParameter("@Name", NamaSearchParam),
                        new SqlParameter("@Type", TypeSearchParam),
                        new SqlParameter("@Role", RoleSearchParam),
                        new SqlParameter("@Parent", ParentSearchParam)
                });

                if (list == null)
                {
                    list = new List<PengaturanMenu_ViewModels>();
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

            ViewBag.TypeMenu = new SelectList(Utility.SelectLookup("TypeMenu", _context), "Value", "Name");
            ViewBag.RolePegawai = new SelectList(Utility.SelectDataMasterRole(_context), "id", "text");

            ViewBag.ParentMenu = new SelectList(Utility.SelectDataMenu(_context), "id", "text");



            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(PengaturanMenu_ViewModels model, string Roles)
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

                if (Roles == null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:PilihRolesNavigation:BelumPilihRoles"]);
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    Navigation data = new Navigation();
                    data.Name = model.Nama;
                    data.Type = model.TipeId;
                    data.ParentNavigationId = model.ParentId;
                    data.Route = model.Route;
                    data.IconClass = model.Icon;
                    data.Order = model.OrderBy;
                    data.Visible = model.Visible;
                    data.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.CreatedTime = DateTime.Now;
                    _context.Navigation.Add(data);
                    _context.SaveChanges();

                    string[] ArrayRoles = Roles.Split(',');
                    foreach (var item in ArrayRoles)
                    {
                        NavigationAssignment dataAssign = new NavigationAssignment();
                        dataAssign.NavigationId = data.Id;
                        dataAssign.RoleId = int.Parse(item);
                        dataAssign.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataAssign.CreatedTime = DateTime.Now;
                        dataAssign.IsActive = true;
                        _context.NavigationAssignment.Add(dataAssign);
                        _context.SaveChanges();
                    }

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
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            PengaturanMenu_ViewModels data = new PengaturanMenu_ViewModels();
            data = StoredProcedureExecutor.ExecuteSPSingle<PengaturanMenu_ViewModels>(_context, "sp_PengaturanMenu_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)
                    });
            if (data != null)
            {
                ViewBag.ParentMenu = new SelectList(Utility.SelectDataMenu(_context), "id", "text", data.ParentId);

                ViewBag.TypeMenu = new SelectList(Utility.SelectLookup("TypeMenu", _context), "Value", "Name", data.TipeId);

                ViewBag.RolePegawai = new SelectList(Utility.SelectDataMasterRole(_context), "id", "text");

                var dataAssignment = _context.NavigationAssignment.Where(m => m.NavigationId == data.Id).Select(m => m.RoleId).ToList();
                ViewBag.NavigationAssignment = String.Join(",", dataAssignment.ToArray());
            }


            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(PengaturanMenu_ViewModels model, string Roles)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    Navigation data = _context.Navigation.Where(m => m.Id == model.Id).FirstOrDefault(); // Ambil data sesuai dengan ID
                    data.Type = model.TipeId;
                    data.ParentNavigationId = model.ParentId;
                    data.Name = model.Nama;
                    data.Order = model.OrderBy;
                    data.Route = model.Route;
                    data.IconClass = model.Icon;
                    data.Visible = model.Visible;
                    data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.UpdatedTime = DateTime.Now;
                    _context.Entry(data).State = EntityState.Modified;
                    _context.SaveChanges();

                    //Ambil semua data assigment Menu
                    var AssignMenu = _context.NavigationAssignment.Where(m => m.NavigationId == data.Id).Select(m => m.RoleId.ToString()).ToList();
                    string[] ArrayAssignMenu = AssignMenu.ToArray();
                    string[] ArrayRoles = Roles.Split(',');

                    var TambahData = ArrayRoles.Except(ArrayAssignMenu);
                    var DeleteData = ArrayAssignMenu.Except(ArrayRoles);

                    if (DeleteData != null)
                    {
                        foreach (var item in DeleteData)
                        {
                            int IdRole = int.Parse(item);
                            NavigationAssignment dataNA = _context.NavigationAssignment.Where(m => m.NavigationId == data.Id && m.RoleId == IdRole).FirstOrDefault();
                            _context.NavigationAssignment.Remove(dataNA);
                        }
                    }

                    //Tambahkan Data Sasaran Unit
                    if (TambahData != null)
                    {
                        foreach (var item in TambahData)
                        {
                            NavigationAssignment dataAssigment = new NavigationAssignment();
                            dataAssigment.NavigationId = model.Id;
                            dataAssigment.RoleId = int.Parse(item);
                            dataAssigment.CreatedById = 1;
                            dataAssigment.CreatedTime = DateTime.Now;
                            dataAssigment.IsActive = true;
                            _context.NavigationAssignment.Add(dataAssigment);
                        }
                    }

                    _context.SaveChanges();
                    trx.Complete();
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
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            PengaturanMenu_ViewModels data = new PengaturanMenu_ViewModels();
            data = StoredProcedureExecutor.ExecuteSPSingle<PengaturanMenu_ViewModels>(_context, "sp_PengaturanMenu_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)
                    });
            if (data != null)
            {
                ViewBag.ParentMenu = new SelectList(Utility.SelectDataMenu(_context), "id", "text", data.ParentId);

                ViewBag.TypeMenu = new SelectList(Utility.SelectLookup("TypeMenu", _context), "Value", "Name", data.TipeId);

                ViewBag.RolePegawai = new SelectList(Utility.SelectDataMasterRole(_context), "id", "text");

                var dataAssignment = _context.NavigationAssignment.Where(m => m.NavigationId == data.Id).Select(m => m.RoleId).ToList();
                ViewBag.NavigationAssignment = String.Join(",", dataAssignment.ToArray());
            }


            return PartialView("_View", data);
        }

        #endregion

        #region Delete
        public ActionResult Delete(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<Navigation> Transaksis = _context.Navigation.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    Navigation data = _context.Navigation.Find(Transaksis[i].Id);
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
