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
//using PortalPMO.Models.dbPortalPMO;
using PortalPMO.ViewModels;
using TwoTierTemplate.Component;
using TwoTierTemplate.Models;

namespace PortalPMO.Controllers
{
    public class DataPenggunaController : Controller
    {
        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLogPipeline lastSession;
        private readonly AccessSecurityPipeline accessSecurity;
        public DataPenggunaController(IConfiguration config, dbPipelineContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            lastSession = new LastSessionLogPipeline(accessor, context, config);
            accessSecurity = new AccessSecurityPipeline(accessor, context, config);
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
                var NppSearchParam = dict["columns[2][search][value]"];
                var NamaSearchParam = dict["columns[3][search][value]"];
                var UnitSearchParam = dict["columns[4][search][value]"];

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));


                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<MasterDataPengguna_ViewModels> list = new List<MasterDataPengguna_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<MasterDataPengguna_ViewModels>(_context, "[sp_MasterDataPengguna_View]", new SqlParameter[]{
                        new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitId),
                        new SqlParameter("@Nama", NamaSearchParam),
                        new SqlParameter("@Unit", UnitSearchParam),
                        new SqlParameter("@Npp", NppSearchParam),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_MasterDataPengguna_Count]", new SqlParameter[]{
                         new SqlParameter("@RoleId", RoleId),
                        new SqlParameter("@UnitId", UnitId),
                        new SqlParameter("@Nama", NamaSearchParam),
                        new SqlParameter("@Unit", UnitSearchParam),
                        new SqlParameter("@Npp", NppSearchParam)});

                if (list == null)
                {
                    list = new List<MasterDataPengguna_ViewModels>();
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

            ViewBag.RolePegawai = new SelectList(UtilityPipeline.SelectDataMasterRole(_context), "id", "text");

            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(MasterDataPengguna_ViewModels model)
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

                //Cek terlebih dahulu apakah pegawai sudah terdaftar atau belum
                TblPegawai CekDataPegawai = _context.TblPegawai.Where(m => m.Npp == model.Npp && m.IsDeleted != true).FirstOrDefault();

                if (CekDataPegawai != null)
                {
                    if (CekDataPegawai.IsActive == false)
                    {

                        return Content(GetConfig.AppSetting["AppSettings:Modul:MasterPengguna:PegawaiSudahAdaTidakAktif"]);
                    }

                    if (CekDataPegawai.IsActive == true)
                    {
                        return Content(GetConfig.AppSetting["AppSettings:Modul:MasterPengguna:PegawaiSudahAda"]);
                    }
                }
                using (var trx = new TransactionScope())
                {
                    //Save data Pegawai
                    TblPegawai dataPegawai = new TblPegawai();
                    dataPegawai.Npp = model.Npp;
                    dataPegawai.Nama = model.Nama;
                    dataPegawai.UnitId = model.UnitId;
                    dataPegawai.JabatanId = model.Jabatan_Id;
                    dataPegawai.Email = model.Email;
                    dataPegawai.Ldaplogin = model.IsLDAP;
                    dataPegawai.IsDeleted = false;
                    dataPegawai.CreatedDate = DateTime.Now;
                    dataPegawai.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataPegawai.IsActive = true;
                    _context.TblPegawai.Add(dataPegawai);
                    _context.SaveChanges();

                    //Save data user
                    TblUser dataUser = new TblUser();
                    dataUser.PegawaiId = dataPegawai.Id;
                    dataUser.Username = dataPegawai.Npp;
                    dataUser.Password = dataPegawai.Npp;
                    dataUser.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataUser.CreatedTime = DateTime.Now;
                    _context.TblUser.Add(dataUser);
                    //_context.SaveChanges();

                    //Masukkan Role PJB Kedalam Database Role
                    TblRolePegawai dataRolePJB = new TblRolePegawai();
                    dataRolePJB.PegawaiId = dataPegawai.Id;
                    dataRolePJB.RoleId = model.Jabatan_Id;
                    dataRolePJB.UnitId = model.UnitId;
                    dataRolePJB.StatusRole = 1; //Status PJB
                    dataRolePJB.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataRolePJB.CreatedTime = DateTime.Now;
                    _context.TblRolePegawai.Add(dataRolePJB);


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
            try
            {
                MasterDataPengguna_ViewModels data = new MasterDataPengguna_ViewModels();
                data = StoredProcedureExecutor.ExecuteSPSingle<MasterDataPengguna_ViewModels>(_context, "sp_MasterDataPengguna_GetDataById", new SqlParameter[]{
                            new SqlParameter("@Id", id)
                        });

                if (data == null)
                {
                    data = new MasterDataPengguna_ViewModels();
                }

                if (data.UnitId != null)
                {
                    ViewBag.Unit = new SelectList(UtilityPipeline.SelectDataUnit(data.UnitId,_context), "id", "text", data.UnitId);
                }
                else
                {
                    ViewBag.Unit = new SelectList("", "");
                }

                ViewBag.Jabatan = new SelectList(UtilityPipeline.SelectDataJabatan(_context), "id", "text", data.Jabatan_Id);

                return PartialView("_Edit", data);
            }

            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }        
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(MasterDataPengguna_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                using (var trx = new TransactionScope())
                {
                    bool isUpdate = false;


                    TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Id == model.Id).FirstOrDefault();
                    //Cek apakah Role PJB Pegawai diganti atau tidak
                    //if (dataPegawai.RoleId != model.RoleId)
                    //{
                    //    isUpdate = true;
                    //}
                    if (dataPegawai.UnitId != model.UnitId)
                    {
                        isUpdate = true;
                    }

                    if (isUpdate)
                    {
                        TblRolePegawai dataRolePegawai = _context.TblRolePegawai.Where(m => m.PegawaiId == dataPegawai.Id && m.IsDeleted != true).FirstOrDefault();
                        dataRolePegawai.IsDeleted = true;
                        dataRolePegawai.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataRolePegawai.UpdatedTime = DateTime.Now;
                        _context.Entry(dataRolePegawai).State = EntityState.Modified;
                        _context.SaveChanges();

                        //Insert Role PJB yang baru
                        TblRolePegawai dataRolePegawaiNEw = new TblRolePegawai();
                        dataRolePegawai.PegawaiId = dataPegawai.Id;
                        dataRolePegawai.RoleId = model.Jabatan_Id;
                        dataRolePegawai.StatusRole = 1; //By Pass PJB
                        dataRolePegawai.IsDeleted = false;
                        dataRolePegawai.UnitId = model.UnitId;
                        dataRolePegawai.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataRolePegawai.CreatedTime = DateTime.Now;
                        _context.TblRolePegawai.Add(dataRolePegawaiNEw);
                        _context.SaveChanges();
                    }


                    dataPegawai.Npp = model.Npp;
                    dataPegawai.Nama = model.Nama;
                    dataPegawai.UnitId = model.UnitId;
                    dataPegawai.Email = model.Email;
                    dataPegawai.IsActive = model.IsActive;
                    dataPegawai.JabatanId = model.Jabatan_Id;
                    dataPegawai.RoleId = model.Jabatan_Id;
                    dataPegawai.Ldaplogin = model.IsLDAP;
                    dataPegawai.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataPegawai.UpdatedDate = DateTime.Now;
                    _context.Entry(dataPegawai).State = EntityState.Modified;
                    _context.SaveChanges();

                    TblUser dataUser = _context.TblUser.Where(m => m.PegawaiId == dataPegawai.Id).FirstOrDefault();
                    dataUser.Username = dataPegawai.Npp;
                    //dataUser.Password = dataPegawai.Npp;
                    dataUser.UpdatedTime = DateTime.Now;
                    dataUser.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
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
            try
            {
                MasterDataPengguna_ViewModels data = new MasterDataPengguna_ViewModels();
                data = StoredProcedureExecutor.ExecuteSPSingle<MasterDataPengguna_ViewModels>(_context, "sp_MasterDataPengguna_GetDataById", new SqlParameter[]{
                            new SqlParameter("@Id", id)
                        });

                if (data == null)
                {
                    data = new MasterDataPengguna_ViewModels();
                }

                if (data.UnitId != null)
                {
                    ViewBag.Unit = new SelectList(UtilityPipeline.SelectDataUnit(data.UnitId, _context), "id", "text", data.UnitId);
                }
                else
                {
                    ViewBag.Unit = new SelectList("", "");
                }

                ViewBag.Jabatan = new SelectList(UtilityPipeline.SelectDataJabatan(_context), "id", "text", data.Jabatan_Id);

                return PartialView("_View", data);
            }

            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        #endregion


        #region Ubah Password
        public ActionResult UbahPassword(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            MasterDataPengguna_ViewModels data = new MasterDataPengguna_ViewModels();
            data = StoredProcedureExecutor.ExecuteSPSingle<MasterDataPengguna_ViewModels>(_context, "sp_MasterDataPengguna_GetDataById", new SqlParameter[]{
                        new SqlParameter("@Id", id)
                    });

            if (data == null)
            {
                data = new MasterDataPengguna_ViewModels();
            }

            if (data.UnitId != null)
            {
                ViewBag.Unit = new SelectList(UtilityPipeline.SelectDataUnit(data.UnitId, _context), "id", "text", data.UnitId);
            }
            else
            {
                ViewBag.Unit = new SelectList("", "");
            }

            if (data.Jabatan_Id != null)
            {
                ViewBag.Jabatan = new SelectList(UtilityPipeline.SelectDataJabatan( _context), "id", "text", data.Jabatan_Id);
            }
            else
            {
                ViewBag.Jabatan = new SelectList("", "");
            }


            return PartialView("_UbahPassword", data);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitUbahPassword(MasterDataPengguna_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                using (var trx = new TransactionScope())
                {
                    bool isUpdate = false;


                    TblUser dataUser = _context.TblUser.Where(m => m.Id == model.Id).FirstOrDefault();
                    dataUser.Password = model.PasswordBaru;
                    dataUser.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataUser.UpdatedTime = DateTime.Now;
                    _context.Entry(dataUser).State = EntityState.Modified;
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

                List<TblPegawai> Transaksis = _context.TblPegawai.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblPegawai data = _context.TblPegawai.Find(Transaksis[i].Id);
                    data.IsDeleted = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
                    data.DeleteById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.DeleteDate = System.DateTime.Now;
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
