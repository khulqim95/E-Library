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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using Rotativa.AspNetCore.Options;
using Rotativa.AspNetCore;

namespace PortalPMO.Controllers
{
    public class DetailProjectController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public DetailProjectController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _accessor = accessor;
            lastSession = new LastSessionLog(accessor, context, config);
        }
        public IActionResult View(int? ProjectId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            DataProject_ViewModels DataProject = StoredProcedureExecutor.ExecuteSPSingle<DataProject_ViewModels>(_context, "[sp_DataProject_GetDataById]", new SqlParameter[]{
                        new SqlParameter("@Id", ProjectId)});


            if (DataProject == null)
            {
                return RedirectToAction("HttpStatusErrorLayout", "Error", new { statusCode = 404 });
            }
            if (DataProject.KompleksitasProjectId != null)
            {
                ViewBag.KompleksitasProject = new SelectList(Utility.SelectDataKompleksitasProject(DataProject.KompleksitasProjectId, _context), "id", "text", DataProject.KompleksitasProjectId);
            }
            else
            {
                ViewBag.KompleksitasProject = new SelectList("", "");
            }

            if (DataProject.KlasifikasiProjectId != null)
            {
                ViewBag.KlasifikasiProject = new SelectList(Utility.SelectDataKlasifikasiProject(DataProject.KlasifikasiProjectId, _context), "id", "text", DataProject.KlasifikasiProjectId);
            }
            else
            {
                ViewBag.KlasifikasiProject = new SelectList("", "");
            }

            if (DataProject.KategoriProjectId != null)
            {
                ViewBag.KategoriProject = new SelectList(Utility.SelectDataKategoriProject(DataProject.KategoriProjectId, _context), "id", "text", DataProject.KategoriProjectId);
            }
            else
            {
                ViewBag.KategoriProject = new SelectList("", "");
            }


            if (DataProject.SubKategoriProjectId != null)
            {
                ViewBag.SubKategoriProject = new SelectList(Utility.SelectDataSubKategoriProject(DataProject.SubKategoriProjectId, _context), "id", "text", DataProject.SubKategoriProjectId);
            }
            else
            {
                ViewBag.SubKategoriProject = new SelectList("", "");
            }

            if (DataProject.SkorProjectId != null)
            {
                ViewBag.SkorProject = new SelectList(Utility.SelectDataSkorProject(DataProject.SkorProjectId, _context), "id", "text", DataProject.SkorProjectId);
            }
            else
            {
                ViewBag.SkorProject = new SelectList("", "");
            }

            if (DataProject.MandatoryId != null)
            {
                ViewBag.Mandatory = new SelectList(Utility.SelectLookup("MandatoryKategori", _context), "Value", "Name",DataProject.MandatoryId);
            }
            else
            {
                ViewBag.Mandatory = new SelectList(Utility.SelectLookup("MandatoryKategori", _context), "Value", "Name");
            }

            if (DataProject.isPIR != null)
            {
                ViewBag.isPIR = new SelectList(Utility.SelectLookup("IsPIR", _context), "Value", "Name", DataProject.MandatoryId);
            }
            else
            {
                ViewBag.isPIR = new SelectList(Utility.SelectLookup("IsPIR", _context), "Value", "Name");
            }


            if (DataProject.PeriodeProjectId != null)
            {
                ViewBag.PeriodeProject = new SelectList(Utility.SelectLookup("PeriodeProject", _context), "Value", "Name", DataProject.PeriodeProjectId);
            }
            else
            {
                ViewBag.PeriodeProject = new SelectList(Utility.SelectLookup("PeriodeProject", _context), "Value", "Name");

            }
            ViewBag.ProjectId = DataProject.Id;
            return View(DataProject);
        }

        public IActionResult AssignProject(int? ProjectId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            if (ProjectId == null)
            {
                return RedirectToAction("HttpStatusErrorLayout", "Error", new { statusCode = 204 });
            }

            return View();
        }

        #region Load Content Client
        public ActionResult LoadContentClient()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataClient");
        }


        public ActionResult LoadDataClientCreate(int? ProjectId)
        {
            List<DataProjectUser_ViewModels> listDataProjectUser = new List<DataProjectUser_ViewModels>();
            try
            {
                listDataProjectUser = StoredProcedureExecutor.ExecuteSPList<DataProjectUser_ViewModels>(_context, "[SP_DetailProject_GetDataClient]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (listDataProjectUser == null)
                {
                    listDataProjectUser = new List<DataProjectUser_ViewModels>();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
          

            return Json(new { data = listDataProjectUser });
        }

        public ActionResult CreateClient()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Client = new SelectList("", "");

            return PartialView("_ModalClient");
        }

        [HttpPost]
        public ActionResult SubmitCreateClient(DataProjectUser_ViewModels model)
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

                TblProjectUser CekUser = _context.TblProjectUser.Where(m => m.ProjectId == model.ProjectId && m.NppPic == model.NppPic && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                if (CekUser != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:ClientSudahAda"]);
                }
                TblProjectUser dataUser = new TblProjectUser();
                dataUser.ProjectId = model.ProjectId;
                dataUser.ClientId = model.ClientId;
                dataUser.NppPic = model.NppPic;
                dataUser.NamaPic = model.NamaPic;
                dataUser.Email = model.Email;
                dataUser.NoHp = model.NoHp;
                dataUser.Keterangan = model.Keterangan;
                dataUser.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataUser.CreatedTime = DateTime.Now;
                _context.TblProjectUser.Add(dataUser);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        public ActionResult EditClient(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }

            DataProjectUser_ViewModels DataUser = new DataProjectUser_ViewModels();

            try
            {
                DataUser = StoredProcedureExecutor.ExecuteSPSingle<DataProjectUser_ViewModels>(_context, "[SP_DetailProject_GetDataClientById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});

                if (DataUser == null)
                {
                    DataUser = new DataProjectUser_ViewModels();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
           
            ViewBag.Client = new SelectList(Utility.SelectDataClient(DataUser.ClientId, _context).ToList(), "id", "text", DataUser.ClientId);

            return PartialView("_ModalClient", DataUser);
        }

        [HttpPost]
        public ActionResult SubmitEditClient(DataProjectUser_ViewModels model)
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

                TblProjectUser CekUser = _context.TblProjectUser.Where(m => m.ProjectId == model.ProjectId && m.NppPic == model.NppPic).FirstOrDefault();

                if (CekUser != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:ClientSudahAda"]);
                }
                TblProjectUser dataUser = _context.TblProjectUser.Where(m => m.Id == model.Id).FirstOrDefault();
                dataUser.ProjectId = model.ProjectId;
                dataUser.ClientId = model.ClientId;
                dataUser.NppPic = model.NppPic;
                dataUser.NamaPic = model.NamaPic;
                dataUser.Email = model.Email;
                dataUser.NoHp = model.NoHp;
                dataUser.Keterangan = model.Keterangan;
                dataUser.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataUser.UpdatedTime = DateTime.Now;
                _context.TblProjectUser.Update(dataUser);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        public ActionResult ViewClient(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }

            DataProjectUser_ViewModels DataUser = new DataProjectUser_ViewModels();

            try
            {
                DataUser = StoredProcedureExecutor.ExecuteSPSingle<DataProjectUser_ViewModels>(_context, "[SP_DetailProject_GetDataClientById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});

                if (DataUser == null)
                {
                    DataUser = new DataProjectUser_ViewModels();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

            ViewBag.Client = new SelectList(Utility.SelectDataClient(DataUser.ClientId, _context).ToList(), "id", "text", DataUser.ClientId);

            return PartialView("_ModalClient", DataUser);
        }

        public ActionResult DeleteClient(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectUser> Transaksis = _context.TblProjectUser.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblProjectUser data = _context.TblProjectUser.Find(Transaksis[i].Id);
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

        #region Load Content Data Depedency Project
        public ActionResult LoadContentDepedencyProject()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataDepedencyProject");
        }

        public ActionResult LoadDataDepedencyProject(int? ProjectId)
        {
            List<DataProjectRelasi_ViewModels> listDataProjectRelasi = new List<DataProjectRelasi_ViewModels>();
            try
            {
                listDataProjectRelasi = StoredProcedureExecutor.ExecuteSPList<DataProjectRelasi_ViewModels>(_context, "[SP_DetailProject_GetDataRelasiProject]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (listDataProjectRelasi == null)
                {
                    listDataProjectRelasi = new List<DataProjectRelasi_ViewModels>();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
            

            return Json(new { data = listDataProjectRelasi });
        }

        public ActionResult CreateDepedencyProject()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Project = new SelectList("", "");

            return PartialView("_ModalDepedencyProject");
        }

        [HttpPost]
        public ActionResult SubmitCreateDepedencyProject(DataProjectRelasi_ViewModels model)
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

                TblProjectRelasi CekData = _context.TblProjectRelasi.Where(m => m.ProjectId == model.ProjectId && m.RelasiProjectId == model.RelasiProjectId && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                if (CekData != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:RelasiProjectSudahAda"]);
                }
                TblProjectRelasi dataProject = new TblProjectRelasi();
                dataProject.ProjectId = model.ProjectId;
                dataProject.RelasiProjectId = model.RelasiProjectId;
                dataProject.Keterangan = model.Keterangan;
                dataProject.Keterangan = model.Keterangan;
                dataProject.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataProject.CreatedTime = DateTime.Now;
                _context.TblProjectRelasi.Add(dataProject);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        public ActionResult EditDepedencyProject(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectRelasi_ViewModels DataRelasi = StoredProcedureExecutor.ExecuteSPSingle<DataProjectRelasi_ViewModels>(_context, "[SP_DetailProject_GetDataRelasiProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataRelasi == null)
            {
                DataRelasi = new DataProjectRelasi_ViewModels();
            }
            ViewBag.Project = new SelectList(Utility.SelectDataProject(DataRelasi.RelasiProjectId, _context).ToList(), "id", "text", DataRelasi.RelasiProjectId);

            return PartialView("_ModalDepedencyProject", DataRelasi);
        }

        [HttpPost]
        public ActionResult SubmitEditDepedencyProject(DataProjectRelasi_ViewModels model)
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

                TblProjectRelasi dataProjectLama = _context.TblProjectRelasi.Where(m => model.Id == model.Id).FirstOrDefault();
                if (dataProjectLama.RelasiProjectId != model.RelasiProjectId)
                {
                    TblProjectRelasi CekData = _context.TblProjectRelasi.Where(m => m.ProjectId == model.ProjectId && m.RelasiProjectId == model.RelasiProjectId).FirstOrDefault();

                    if (CekData != null)
                    {
                        return Content(GetConfig.AppSetting["AppSettings:Messages:RelasiProjectSudahAda"]);
                    }
                }

                TblProjectRelasi dataProject = _context.TblProjectRelasi.Where(m => model.Id == model.Id).FirstOrDefault();
                dataProject.ProjectId = model.ProjectId;
                dataProject.RelasiProjectId = model.RelasiProjectId;
                dataProject.Keterangan = model.Keterangan;
                dataProject.Keterangan = model.Keterangan;
                dataProject.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataProject.CreatedTime = DateTime.Now;
                _context.TblProjectRelasi.Update(dataProject);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        public ActionResult ViewDepedencyProject(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectRelasi_ViewModels DataRelasi = StoredProcedureExecutor.ExecuteSPSingle<DataProjectRelasi_ViewModels>(_context, "[SP_DetailProject_GetDataRelasiProjectById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataRelasi == null)
            {
                DataRelasi = new DataProjectRelasi_ViewModels();
            }
            ViewBag.Project = new SelectList(Utility.SelectDataProject(DataRelasi.RelasiProjectId, _context).ToList(), "id", "text", DataRelasi.RelasiProjectId);

            return PartialView("_ModalDepedencyProject", DataRelasi);
        }


        public ActionResult DeleteDepedencyProject(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectRelasi> Transaksis = _context.TblProjectRelasi.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblProjectRelasi data = _context.TblProjectRelasi.Find(Transaksis[i].Id);
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

        #region Load Content Anggota Tim
        public ActionResult LoadContentAnggotaTim()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataAnggotaTim");
        }

        public ActionResult LoadDataAnggotaTim(int? ProjectId)
        {
            List<DataProjectAnggotaTim_ViewModels> listDataProjectAnggotaTim = new List<DataProjectAnggotaTim_ViewModels>();
            try
            {
                listDataProjectAnggotaTim = StoredProcedureExecutor.ExecuteSPList<DataProjectAnggotaTim_ViewModels>(_context, "[SP_DetailProject_GetDataProjectMember]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (listDataProjectAnggotaTim == null)
                {
                    listDataProjectAnggotaTim = new List<DataProjectAnggotaTim_ViewModels>();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }
            

            return Json(new { data = listDataProjectAnggotaTim });
        }

        public ActionResult CreateAnggotaTim()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Pegawai = new SelectList("", "");
            ViewBag.JobPosition = new SelectList("", "");

            return PartialView("_ModalAnggotaTim");
        }

        [HttpPost]
        public ActionResult SubmitCreateAnggotaTim(DataProjectAnggotaTim_ViewModels model)
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
                TblProjectMember CekData = _context.TblProjectMember.Where(m => m.ProjectId == model.ProjectId && m.PegawaiId == model.PegawaiId && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();


                if (CekData != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:AnggotaTimSudahAda"]);
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    //Get Data Pegawai
                    TblProjectMember dataPegawai = new TblProjectMember();
                    dataPegawai.ProjectId = model.ProjectId;
                    dataPegawai.JobPositionId = model.JobPositionId;
                    dataPegawai.PegawaiId = model.PegawaiId;
                    dataPegawai.UnitPegawaiId = _context.TblPegawai.Where(m=>m.Id == model.PegawaiId).Select(m=>m.UnitId).FirstOrDefault();

                    dataPegawai.Keterangan = model.Keterangan;
                    dataPegawai.SendAsTask = model.SendAsTask;
                    if (model.TanggalTargetPenyelesaianAnggotaTim != null)
                    {
                        var splitTanggal = model.TanggalTargetPenyelesaianAnggotaTim.Replace(" ", "").Split("-");

                        dataPegawai.StartDate = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataPegawai.EndDate = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    dataPegawai.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataPegawai.CreatedTime = DateTime.Now;
                    _context.TblProjectMember.Add(dataPegawai);
                    _context.SaveChanges();

                    //Masukkan ke dalam table Log
                    TblProjectLog dataLog = new TblProjectLog();
                    dataLog.ProjectId = model.ProjectId;
                    dataLog.Tanggal = DateTime.Now;
                    dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                    dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.CreatedTime = DateTime.Now;
                    dataLog.LogActivityId = Utility.ActivityTambahAnggotTeamProject;
                    dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                    dataLog.Komentar = model.Keterangan;
                    dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:TambahAnggotaTeamProject"].Replace("#Nama", model.Nama).Replace("#posisi", model.JobPosisi);
                    //TblLookup dataLookup = _context.TblLookup.Where(m => m.Value == dataLog.LogActivityId && m.Type == "Activity").FirstOrDefault();

                    _context.TblProjectLog.Add(dataLog);
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


        public ActionResult EditAnggotaTim(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectAnggotaTim_ViewModels DataAnggotaTim = StoredProcedureExecutor.ExecuteSPSingle<DataProjectAnggotaTim_ViewModels>(_context, "[SP_DetailProject_GetDataProjectMemberById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataAnggotaTim == null)
            {
                DataAnggotaTim = new DataProjectAnggotaTim_ViewModels();
            }
            ViewBag.Pegawai = new SelectList(Utility.SelectDataPegawai(DataAnggotaTim.PegawaiId, _context).ToList(), "id", "text", DataAnggotaTim.PegawaiId);
            ViewBag.JobPosition = new SelectList(Utility.SelectDataJobPosition(DataAnggotaTim.JobPositionId, _context).ToList(), "id", "text", DataAnggotaTim.JobPositionId);

            return PartialView("_ModalAnggotaTim", DataAnggotaTim);
        }

        public ActionResult ViewAnggotaTim(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectAnggotaTim_ViewModels DataAnggotaTim = StoredProcedureExecutor.ExecuteSPSingle<DataProjectAnggotaTim_ViewModels>(_context, "[SP_DetailProject_GetDataProjectMemberById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataAnggotaTim == null)
            {
                DataAnggotaTim = new DataProjectAnggotaTim_ViewModels();
            }
            ViewBag.Pegawai = new SelectList(Utility.SelectDataPegawai(DataAnggotaTim.PegawaiId, _context).ToList(), "id", "text", DataAnggotaTim.PegawaiId);
            ViewBag.JobPosition = new SelectList(Utility.SelectDataJobPosition(DataAnggotaTim.JobPositionId, _context).ToList(), "id", "text", DataAnggotaTim.JobPositionId);

            return PartialView("_ModalAnggotaTim", DataAnggotaTim);
        }

        [HttpPost]
        public ActionResult SubmitEditAnggotaTim(DataProjectAnggotaTim_ViewModels model)
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
                TblProjectMember dataPegawaiLama = _context.TblProjectMember.Where(m => m.Id == model.Id).FirstOrDefault();
                if (dataPegawaiLama.PegawaiId != model.PegawaiId)
                {
                    TblProjectMember CekData = _context.TblProjectMember.Where(m => m.ProjectId == model.ProjectId && m.PegawaiId == model.PegawaiId && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();


                    if (CekData != null)
                    {
                        return Content(GetConfig.AppSetting["AppSettings:Messages:AnggotaTimSudahAda"]);
                    }
                }

                using (TransactionScope trx = new TransactionScope())
                {
                    TblProjectMember dataPegawai = _context.TblProjectMember.Where(m => m.Id == model.Id).FirstOrDefault();
                    string DataNamaPegawai = _context.TblPegawai.Where(m => m.Id == dataPegawai.PegawaiId).Select(m => m.Nama).FirstOrDefault();
                    //Get Data Pegawai
                    dataPegawai.ProjectId = model.ProjectId;
                    if (dataPegawai.JobPositionId != model.JobPositionId)
                    {
                        TblMasterJobPosition dataJobPositionLama = _context.TblMasterJobPosition.Where(m => m.Id == dataPegawai.JobPositionId).FirstOrDefault();
                        TblMasterJobPosition dataJobPosition = _context.TblMasterJobPosition.Where(m => m.Id == model.JobPositionId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahMemberProject"].Replace("#NamaPegawai", DataNamaPegawai).Replace("#NamaData", "Job Position").Replace("#NilaiAwal", dataJobPositionLama.Nama).Replace("#NilaiAkhir", dataJobPosition.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataPegawai.JobPositionId = model.JobPositionId;

                    if (dataPegawai.PegawaiId != model.PegawaiId)
                    {
                        TblPegawai CekDataPegawaiLama = _context.TblPegawai.Where(m => m.Id == dataPegawai.PegawaiId).FirstOrDefault();
                        TblPegawai CekDataPegawai = _context.TblPegawai.Where(m => m.Id == model.PegawaiId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahMemberProject"].Replace("#NamaPegawai", DataNamaPegawai).Replace("#NamaData", "Ubah Pegawai").Replace("#NilaiAwal", CekDataPegawaiLama.Nama).Replace("#NilaiAkhir", CekDataPegawai.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataPegawai.PegawaiId = model.PegawaiId;

                    dataPegawai.UnitPegawaiId = _context.TblPegawai.Where(m => m.Id == model.PegawaiId).Select(m => m.UnitId).FirstOrDefault();

                    //Cek estimasi timeline PIR 
                    if (dataPegawai.Keterangan != model.Keterangan)
                    {

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = "Ubah Detail Job Desc ";
                        dataLog.Komentar = model.Keterangan;
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }

                    dataPegawai.Keterangan = model.Keterangan;
                    if (dataPegawai.SendAsTask != model.SendAsTask)
                    {
                        TblLookup dataSendTask = _context.TblLookup.Where(m => m.Value == model.SendAsTask && m.Type == "SendAsTask" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
                        TblLookup dataSendTaskLama = _context.TblLookup.Where(m => m.Value == dataPegawai.SendAsTask && m.Type == "SendAsTask" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahMemberProject"].Replace("#NamaPegawai", DataNamaPegawai).Replace("#NamaData", "Send Task").Replace("#NilaiAwal", dataSendTaskLama.Name).Replace("#NilaiAkhir", dataSendTask.Name);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataPegawai.SendAsTask = model.SendAsTask;

                    if (model.TanggalTargetPenyelesaianAnggotaTim != null)
                    {
                        var splitTanggal = model.TanggalTargetPenyelesaianAnggotaTim.Replace(" ", "").Split("-");

                        var StartDateConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var EndDateConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //Cek estimasi timeline PIR 
                        if (dataPegawai.StartDate != StartDateConvert && dataPegawai.EndDate != EndDateConvert)
                        {

                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.ProjectId;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahMemberProject"].Replace("#NamaPegawai", DataNamaPegawai).Replace("#NamaData", "Target Penyelesaian ").Replace("#NilaiAwal", dataPegawai.StartDate?.ToString("dd/MM/yyyy") + " - " + dataPegawai.EndDate?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir",model.TanggalTargetPenyelesaianAnggotaTim);
                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }

                        dataPegawai.StartDate = StartDateConvert;
                        dataPegawai.EndDate = EndDateConvert;

                    }
                    dataPegawai.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataPegawai.CreatedTime = DateTime.Now;
                    _context.TblProjectMember.Update(dataPegawai);
                    _context.SaveChanges();

                    ////Masukkan ke dalam table Log
                    //TblProjectLog dataLog = new TblProjectLog();
                    //dataLog.ProjectId = model.ProjectId;
                    //dataLog.Tanggal = DateTime.Now;
                    //dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    //dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                    //dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    //dataLog.CreatedTime = DateTime.Now;
                    //dataLog.LogActivityId = Utility.ActivityEditDataAnggotaTeamProject;
                    //dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                    //dataLog.Komentar = model.Keterangan;
                    //dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:EditDataAnggotaTeamProject"].Replace("#Nama", model.Nama).Replace("#posisi", model.JobPosisi);
                    //TblLookup dataLookup = _context.TblLookup.Where(m => m.Value == dataLog.LogActivityId && m.Type == "Activity").FirstOrDefault();

                    //_context.TblProjectLog.Add(dataLog);
                    //_context.SaveChanges();

                    trx.Complete();
                }

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        public ActionResult DeleteAnggotaTim(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectMember> Transaksis = _context.TblProjectMember.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    using (TransactionScope trx = new TransactionScope())
                    {
                        TblProjectMember data = _context.TblProjectMember.Find(Transaksis[i].Id);
                        TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Id == data.PegawaiId).FirstOrDefault();
                        TblMasterJobPosition dataJobPosisi = _context.TblMasterJobPosition.Where(m => m.Id == data.JobPositionId).FirstOrDefault();

                        data.IsDeleted = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
                        data.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        data.DeletedTime = System.DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = data.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityHapusAnggotaTeamProject;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Komentar = "Delete Data Anggota Team Project";
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:DeleteDataAnggotaTeamProject"].Replace("#Nama", dataPegawai.Nama).Replace("#posisi", dataJobPosisi.Nama);
                        TblLookup dataLookup = _context.TblLookup.Where(m => m.Value == dataLog.LogActivityId && m.Type == "Activity").FirstOrDefault();

                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();

                        trx.Complete();

                    }
                }
                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }


        public ActionResult DetailProgressAnggotaTim(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
           
            return PartialView("_ModalUpdateProgress");
        }

        #endregion

        #region Load Content File Repository
        public ActionResult LoadContentFileRepository()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataFileRepository");
        }

        public ActionResult LoadDataFileRepository(int? ProjectId)
        {
            List<DataProjectFile_ViewModels> listDataProjectUser = StoredProcedureExecutor.ExecuteSPList<DataProjectFile_ViewModels>(_context, "[SP_DetailProject_GetDataProjectFile]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
            if (listDataProjectUser == null)
            {
                listDataProjectUser = new List<DataProjectFile_ViewModels>();
            }

            return Json(new { data = listDataProjectUser });
        }


        public ActionResult LoadDataProjectLog(int? ProjectId)
        {
            List<DataProjectLogDetails_ViewModels> listDataProjectLog = StoredProcedureExecutor.ExecuteSPList<DataProjectLogDetails_ViewModels>(_context, "[sp_ProjectLog_GetAllData]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
            if (listDataProjectLog == null)
            {
                listDataProjectLog = new List<DataProjectLogDetails_ViewModels>();
            }

            return Json(listDataProjectLog);
        }



        public ActionResult CreateFileRepository()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.TypeFile = new SelectList(Utility.SelectTypeDokumen(_context).ToList(), "id", "text");

            return PartialView("_ModalFileRepository");
        }

        [HttpPost]
        public ActionResult SubmitCreateFileRepository(DataProjectFile_ViewModels model)
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

                //TblConfigApps ConfigApps = _context.TblConfigApps.FirstOrDefault();
                TblSystemParameter ConfigAppsLocalPath = _context.TblSystemParameter.Where(m => m.Key == "LocalPath").FirstOrDefault();
                TblSystemParameter ConfigAppsTypeFile = _context.TblSystemParameter.Where(m => m.Key == "TypeFileUpload").FirstOrDefault();
                TblSystemParameter ConfigAppsMaxFile = _context.TblSystemParameter.Where(m => m.Key == "MaxFileSize").FirstOrDefault();

                if (model.File != null)
                {
                    TblProjectFile FileAttachment = new TblProjectFile();
                    var FileUpload = model.File;
                    string AllowedFileUploadType = ConfigAppsTypeFile.Value;
                    decimal SizeFile = FileUpload.Length / 1000000;

                    string Ext = Path.GetExtension(FileUpload.FileName);

                    //Validate Upload
                    if (!AllowedFileUploadType.Contains(Ext))
                    {
                        return Content(GetConfig.AppSetting["PathFolder:UploadTypeFile"]);
                    }

                    decimal maxSizeValue = 10;
                    if (ConfigAppsMaxFile != null)
                    {
                        try
                        {
                            maxSizeValue = decimal.Parse(ConfigAppsMaxFile.Value);

                        }
                        catch (Exception Ex)
                        {
                            maxSizeValue = 10;
                        }
                    }

                    if (SizeFile > maxSizeValue)
                    {
                        string Res = GetConfig.AppSetting["PathFolder:UploadMaxSize"];
                        return Content(Res);
                    }

                    //create path directory
                    var PathFolder = ConfigAppsLocalPath.Value;

                    if (!Directory.Exists(PathFolder))
                    {
                        Directory.CreateDirectory(PathFolder);
                    }

                    var fileNameReplaceSpace = FileUpload.FileName.Replace(" ", "_");
                    var path = Path.Combine(PathFolder, fileNameReplaceSpace);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    using (TransactionScope trx = new TransactionScope())
                    {
                        TblProjectFile DataProjectFile = new TblProjectFile();
                        DataProjectFile.ProjectId = model.ProjectId;
                        DataProjectFile.Size = FileUpload.Length / 1000;
                        DataProjectFile.Keterangan = model.Keterangan;
                        DataProjectFile.TypeDokumenId = model.TypeDokumenId;
                        DataProjectFile.NamaFile = Path.GetFileNameWithoutExtension(fileNameReplaceSpace);
                        DataProjectFile.FileType = FileUpload.ContentType;
                        DataProjectFile.FileExt = Ext;
                        DataProjectFile.FullPath = path;
                        DataProjectFile.Path = fileNameReplaceSpace;
                        DataProjectFile.UploadById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        DataProjectFile.UploadTime = DateTime.Now;
                        _context.TblProjectFile.Add(DataProjectFile);
                        _context.SaveChanges();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityTambahLampiranFile;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:TambahFileLampiran"].Replace("#NamaFileLampiran", DataProjectFile.NamaFile);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();

                        trx.Complete();

                    }

                   

                }

         
                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        public ActionResult EditFileRepository(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectFile_ViewModels DataFile = StoredProcedureExecutor.ExecuteSPSingle<DataProjectFile_ViewModels>(_context, "[SP_DetailProject_GetDataProjectMemberById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataFile == null)
            {
                DataFile = new DataProjectFile_ViewModels();
            }
            ViewBag.TypeFile = new SelectList(Utility.SelectTypeDokumen(_context).ToList(), "id", "text",  DataFile.TypeDokumenId);

            return PartialView("_ModalFileRepository", DataFile);
        }

        public ActionResult ViewFileRepository(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectAnggotaTim_ViewModels DataAnggotaTim = StoredProcedureExecutor.ExecuteSPSingle<DataProjectAnggotaTim_ViewModels>(_context, "[SP_DetailProject_GetDataProjectMemberById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataAnggotaTim == null)
            {
                DataAnggotaTim = new DataProjectAnggotaTim_ViewModels();
            }
            ViewBag.Pegawai = new SelectList(Utility.SelectDataPegawai(DataAnggotaTim.PegawaiId, _context).ToList(), "id", "text", DataAnggotaTim.PegawaiId);
            ViewBag.JobPosition = new SelectList(Utility.SelectDataJobPosition(DataAnggotaTim.JobPositionId, _context).ToList(), "id", "text", DataAnggotaTim.JobPositionId);

            return PartialView("_ModalAnggotaTim", DataAnggotaTim);
        }

        [HttpPost]
        public ActionResult SubmitEditFileRepository(DataProjectAnggotaTim_ViewModels model)
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
              
                TblProjectFile dataFileRepository = _context.TblProjectFile.Where(m => m.Id == model.Id).FirstOrDefault();
                dataFileRepository.Keterangan = model.Keterangan;
                _context.TblProjectFile.Update(dataFileRepository);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        public ActionResult DeleteFileRepository(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectFile> Transaksis = _context.TblProjectFile.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    using (TransactionScope trx = new TransactionScope())
                    {
                        TblProjectFile data = _context.TblProjectFile.Find(Transaksis[i].Id);
                        data.IsDeleted = true; //Jika true data tidak akan ditampilkan dan data masih tersimpan di dalam database
                        data.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        data.DeletedTime = System.DateTime.Now;
                        _context.Entry(data).State = EntityState.Modified;
                        _context.SaveChanges();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = data.ProjectId;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityHapusLampiranFile;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:HapusFileLampiran"].Replace("#NamaFileLampiran", data.NamaFile);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();

                        trx.Complete();
                    }
                }
                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }

        public ActionResult DownloadFile(int Id)
        {
            //TblProjectFile Attachment = _context.TblProjectFile.Where(m => m.Id == Id).FirstOrDefault();
           DataProjectFile_ViewModels Attachment = StoredProcedureExecutor.ExecuteSPSingle<DataProjectFile_ViewModels>(_context, "[sp_Utility_Project_DownloadFileAttachment]", new SqlParameter[]{
                       new SqlParameter("@Id", Id)});
            //string file = System.IO.Path.GetFileName(Attachment.DownloadPath);
            //WebClient cln = new WebClient();
            //cln.DownloadFile(Attachment.DownloadPath, file);
            var memory = new MemoryStream();
            var req = System.Net.WebRequest.Create(Attachment.DownloadPath);
            using (Stream stream = req.GetResponse().GetResponseStream())
            {
                stream.CopyTo(memory);
            }
            //memory.Position = 0;
            memory.Position = 0;

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Attachment.NamaFile,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };

            //return new FileStreamResult(memory, Attachment.FileType);
            return File(memory, Attachment.FileType, Attachment.FullPath);
            //return File(Attachment.FilePath, System.Net.Mime.MediaTypeNames.Application.Octet, Attachment.FileName + System.DateTime.Now.ToString("ddMMyyyy") + Attachment.FileExt);

            //return File(Attachment.FilePath, System.Net.Mime.MediaTypeNames.Application.Octet, Attachment.FileName + System.DateTime.Now.ToString("ddMMyyyy") + Attachment.Ext);
        }
        #endregion
        #region Load Content Data Notes
        public ActionResult LoadContentNotes()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataNotes");
        }

        public ActionResult LoadDataNotes(int? ProjectId)
        {
            List<DataProjectNotes_ViewModels> listDataProjectAnggotaTim = new List<DataProjectNotes_ViewModels>();
            try
            {
                listDataProjectAnggotaTim = StoredProcedureExecutor.ExecuteSPList<DataProjectNotes_ViewModels>(_context, "[SP_DetailProject_GetDataNotes]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (listDataProjectAnggotaTim == null)
                {
                    listDataProjectAnggotaTim = new List<DataProjectNotes_ViewModels>();
                }
            }
            catch (Exception Ex)
            {

                throw;
            }


            return Json(new { data = listDataProjectAnggotaTim });
        }
        public ActionResult CreateNotes()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_ModalNotes");
        }
        public ActionResult EditNotes(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectNotes_ViewModels DataFile = StoredProcedureExecutor.ExecuteSPSingle<DataProjectNotes_ViewModels>(_context, "[SP_DetailProject_GetDataNotesById]", new SqlParameter[]{
                        new SqlParameter("@Id", Id)});
            if (DataFile == null)
            {
                DataFile = new DataProjectNotes_ViewModels();
            }

            return PartialView("_ModalNotes", DataFile);
        }

        [HttpPost]
        public ActionResult SubmitCreateNotes(DataProjectNotes_ViewModels model)
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

                TblProjectNotes dataNotes = new TblProjectNotes();
                dataNotes.Judul = model.Judul;
                dataNotes.Notes = model.Notes;
                dataNotes.ProjectId = model.ProjectId;
                dataNotes.UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));
                dataNotes.PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataNotes.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataNotes.CreatedTime = System.DateTime.Now;
                _context.TblProjectNotes.Add(dataNotes);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        [HttpPost]
        public ActionResult SubmitEditNotes(DataProjectNotes_ViewModels model)
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

                TblProjectNotes dataNotes = _context.TblProjectNotes.Where(m => m.Id == model.Id).FirstOrDefault();
                dataNotes.Judul = model.Judul;
                dataNotes.Notes = model.Notes;
                dataNotes.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                dataNotes.UpdatedTime = System.DateTime.Now;
                _context.TblProjectNotes.Update(dataNotes);
                _context.SaveChanges();

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

        public ActionResult DeleteNotes(string Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                int[] confirmedDeleteId = Ids.Split(',').Select(int.Parse).ToArray();

                List<TblProjectNotes> Transaksis = _context.TblProjectNotes.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblProjectNotes data = _context.TblProjectNotes.Find(Transaksis[i].Id);
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

        #region Load Content Data Log History
        public ActionResult LoadContentLogHistory()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_DataLogHistory");
        }
        #endregion

        #region PRINT
        public IActionResult Print(int? ProjectId)
        {
            Print_ViewModels data = new Print_ViewModels();
            data.data = new DataProject_ViewModels();
            data.listDataProject = new List<DataProjectRelasi_ViewModels>();
            data.listAnggotaTim = new List<DataProjectAnggotaTim_ViewModels>();
            data.listUser = new List<DataProjectUser_ViewModels>();

            string Npp = HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai);
            string Nama_User_Login = HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai);


            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                data.data = StoredProcedureExecutor.ExecuteSPSingle<DataProject_ViewModels>(_context, "[sp_DataProject_PrintDataById]", new SqlParameter[]{
                        new SqlParameter("@Id", ProjectId)});
                if (data.data == null)
                {
                    data.data = new DataProject_ViewModels();
                }

                data.listDataProject = StoredProcedureExecutor.ExecuteSPList<DataProjectRelasi_ViewModels>(_context, "[SP_DetailProject_GetDataRelasiProject]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (data.listDataProject == null)
                {
                    data.listDataProject = new List<DataProjectRelasi_ViewModels>();
                }

                data.listAnggotaTim = StoredProcedureExecutor.ExecuteSPList<DataProjectAnggotaTim_ViewModels>(_context, "SP_DetailProject_GetDataProjectMember", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (data.listAnggotaTim == null)
                {
                    data.listAnggotaTim = new List<DataProjectAnggotaTim_ViewModels>();
                }

                data.listUser = StoredProcedureExecutor.ExecuteSPList<DataProjectUser_ViewModels>(_context, "SP_DetailProject_GetDataClient", new SqlParameter[]{
                        new SqlParameter("@ProjectId", ProjectId)});
                if (data.listUser == null)
                {
                    data.listUser = new List<DataProjectUser_ViewModels>();
                }


                if (data == null)
                {
                    return RedirectToAction("HttpStatusErrorLayout", "Error", new { statusCode = 404 });
                }
            }
            catch (Exception)
            {

                throw;
            }


            //return View("_Print", data);
            return new ViewAsPdf("_Print", data)
            {

                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 10, Bottom = 20, Right = 10, Top = 10 },
                //PageMargins = new Margins(30, 10, 40, 10),
                FileName = "Printout_ProjectNo_"+data.data.NoProject+".pdf",
                //CustomSwitches = customSwitches
                //CustomSwitches = $"--footer-html \"{Url.Action("ReportFooter", "Utility", new { area = "" }, "http")}\" --header-html \"{Url.Action("ReportHeader", "Utility", new { area = "" }, "http")}\""
                //CustomSwitches = string.Format("--footer-left \"tes left\" --footer-rigt \"{0}\" --footer-center \"Hal: [page]/[toPage]\"  --footer-font-size \"7\" --footer-spacing 1.5",DateTime
                CustomSwitches = string.Format("--footer-left \"Dicetak oleh: " + Npp + " - " + Nama_User_Login + "\" --footer-right \"{0}\" --footer-center \"Hal: [page]/[toPage]\"  --footer-font-size \"5\" --footer-spacing \"2\"", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "WIB")

                //  "--footer-html \" Hal. [page] / [toPage]\"" +
                //"--footer-left \"  Coba Left \""
                //"--footer-right \"  Coba Right\"" +


            };
        }
        #endregion

    }
}
