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
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PortalPMO.Controllers
{
    public class CreateProjectController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;
        public static List<DataProjectUser_ViewModels> listDataProjectUser;
        public static List<DataProjectRelasi_ViewModels> listDataProjectRelasi;
        public static List<DataProjectAnggotaTim_ViewModels> listDataAnggotaTim;

        public CreateProjectController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
            listDataProjectUser = new List<DataProjectUser_ViewModels>();
            listDataProjectRelasi = new List<DataProjectRelasi_ViewModels>();
            listDataAnggotaTim = new List<DataProjectAnggotaTim_ViewModels>();
            ViewBag.Mandatory = new SelectList(Utility.SelectLookup("MandatoryKategori", _context), "Value", "Name");
            ViewBag.isPIR = new SelectList(Utility.SelectLookup("IsPIR", _context), "Value", "Name");
            ViewBag.ProjectPeriode = new SelectList(Utility.SelectLookup("PeriodeProject", _context), "Value", "Name");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitCreate(DataProject_ViewModels model)
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
                    TblSystemParameter ConfigAppsLocalPath = _context.TblSystemParameter.Where(m=>m.Key == "LocalPath").FirstOrDefault();
                    TblSystemParameter ConfigAppsTypeFile = _context.TblSystemParameter.Where(m => m.Key == "TypeFileUpload").FirstOrDefault();
                    TblSystemParameter ConfigAppsMaxFile = _context.TblSystemParameter.Where(m => m.Key == "MaxFileSize").FirstOrDefault();

                    TblProject dataProject = new TblProject();
                    dataProject.ProjectNo = Utility.GenerateNoProject(_context);
                    dataProject.KategoriProjectId = model.KategoriProjectId;
                    dataProject.SubKategoriProjectId = model.SubKategoriProjectId;
                    dataProject.SkorProjectId = model.SkorProjectId;
                    dataProject.KompleksitasProjectId = model.KompleksitasProjectId;
                    dataProject.KlasifikasiProjectId = model.KlasifikasiProjectId;
                    dataProject.PeriodeProjectId = model.PeriodeProjectId;
                    dataProject.NotifikasiId = model.NotifikasiId;
                    dataProject.ProjectStatusId = model.ProjectStatusId;
                    dataProject.Kode = model.Kode;
                    dataProject.Nama = model.Nama;
                    dataProject.NoMemo = model.NoMemo;
                    dataProject.TanggalMemo = DateTime.ParseExact(model.TanggalMemo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dataProject.NoDrf = model.NoDrf;
                    dataProject.TanggalDrf = DateTime.ParseExact(model.TanggalDrf, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dataProject.TanggalDisposisi = DateTime.ParseExact(model.TanggalDisposisi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dataProject.TanggalKlarifikasi = DateTime.ParseExact(model.TanggalKlarifikasi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (model.TanggalEstimasiDone != null)
                    {
                        var splitTanggal = model.TanggalEstimasiDone.Replace(" ", "").Split("-");

                        dataProject.TanggalEstimasiMulai = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataProject.TanggalEstimasiSelesai = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.TanggalEstimasiDevelopment != null)
                    {
                        var splitTanggal = model.TanggalEstimasiDevelopment.Replace(" ", "").Split("-");

                        dataProject.TanggalEstimasiDevelopmentAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataProject.TanggalEstimasiDevelopmentAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.TanggalEstimasiTesting != null)
                    {
                        var splitTanggal = model.TanggalEstimasiTesting.Replace(" ", "").Split("-");

                        dataProject.TanggalEstimasiTestingAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataProject.TanggalEstimasiTestingAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.TanggalEstimasiPiloting != null)
                    {
                        var splitTanggal = model.TanggalEstimasiPiloting.Replace(" ", "").Split("-");

                        dataProject.TanggalEstimasiPilotingAwal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataProject.TanggalEstimasiPilotingAkhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.TanggalEstimasiPir != null)
                    {
                        var splitTanggal = model.TanggalEstimasiPir.Replace(" ", "").Split("-");

                        dataProject.TanggalEstimasiPirawal = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dataProject.TanggalEstimasiPirakhir = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    dataProject.TanggalEstimasiProduction = DateTime.ParseExact(model.TanggalEstimasiProduction, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dataProject.KompleksitasProjectId = model.KompleksitasProjectId;
                    dataProject.KlasifikasiProjectId = model.KlasifikasiProjectId;
                    dataProject.PeriodeProjectId = model.PeriodeProjectId;
                    dataProject.KategoriProjectId = model.KategoriProjectId;
                    dataProject.SubKategoriProjectId = model.SubKategoriProjectId;
                    dataProject.SkorProjectId = model.SkorProjectId;
                    dataProject.MandatoryId = model.MandatoryId;
                    dataProject.ProjectStatusId = model.ProjectStatusId;
                    dataProject.IsPir = model.isPIR;
                    dataProject.DetailRequirment = model.DetailRequirment;
                    dataProject.DetailRequirment = model.DetailRequirment;
                    dataProject.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataProject.CreatedTime = DateTime.Now;
                    _context.TblProject.Add(dataProject);
                    _context.SaveChanges();

                    //Masukkan ke dalam table log project status
                    TblProjectLogStatus dataLogStatus = new TblProjectLogStatus();
                    dataLogStatus.Projectid = dataProject.Id;
                    dataLogStatus.ProjectStatusTo = dataProject.ProjectStatusId;
                    dataLogStatus.ProjectStatusToValue = Utility.SelectProjectStatusName(dataLogStatus.ProjectStatusTo, _context);
                    dataLogStatus.Tanggal = DateTime.Now;
                    dataLogStatus.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLogStatus.CreatedTime = DateTime.Now;
                    _context.TblProjectLogStatus.Add(dataLogStatus);
                    _context.SaveChanges();

                    //Masukkan ke dalam table Log
                    TblProjectLog dataLog = new TblProjectLog();
                    dataLog.ProjectId = dataProject.Id;
                    dataLog.Tanggal = DateTime.Now;
                    dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                    dataLog.ProjectStatusTo = model.ProjectStatusId;
                    dataLog.ProjectStatusToValue = Utility.SelectProjectStatusName(dataLog.ProjectStatusTo, _context);
                    dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.CreatedTime = DateTime.Now;
                    dataLog.LogActivityId = Utility.ActivityCreateProject;
                    dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                    dataLog.Komentar = model.CatatanUser;
                    dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:CreateProject"].Replace("#NoProject",dataProject.ProjectNo);
                    TblLookup dataLookup = _context.TblLookup.Where(m => m.Value == dataLog.LogActivityId && m.Type == "Activity").FirstOrDefault();
                    _context.TblProjectLog.Add(dataLog);
                    _context.SaveChanges();

                    //Masukkan File DRF
                    if (model.FileDRF != null)
                    {
                        TblProjectFile FileAttachment = new TblProjectFile();
                        var FileDRF = model.FileDRF;
                        string AllowedFileUploadType = ConfigAppsTypeFile.Value;
                        decimal SizeFile = FileDRF.Length / 1000000;

                        string Ext = Path.GetExtension(FileDRF.FileName);

                        //Validate Upload
                        if (!AllowedFileUploadType.Contains(Ext))
                        {
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadTypeFile"]);
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
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadMaxSize"]);
                        }

                        //create path directory
                        var PathFolder = ConfigAppsLocalPath.Value;

                        if (!Directory.Exists(PathFolder))
                        {
                            Directory.CreateDirectory(PathFolder);
                        }

                        var fileNameReplaceSpace = FileDRF.FileName.Replace(" ", "_");
                        var path = Path.Combine(PathFolder, fileNameReplaceSpace);
                        using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                        {
                            FileDRF.CopyTo(stream);
                        }

                        TblProjectFile DataProjectFile = new TblProjectFile();
                        DataProjectFile.ProjectId = dataProject.Id;
                        DataProjectFile.Size = FileDRF.Length / 1000;
                        DataProjectFile.Keterangan = model.KeteranganDrf;
                        DataProjectFile.TypeDokumenId = Utility.TypeDokumen_DRF;
                        DataProjectFile.NamaFile = Path.GetFileNameWithoutExtension(fileNameReplaceSpace);
                        DataProjectFile.FileType = FileDRF.ContentType;
                        DataProjectFile.FileExt = Ext;
                        DataProjectFile.FullPath = path;
                        DataProjectFile.Path = fileNameReplaceSpace;
                        DataProjectFile.UploadById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        DataProjectFile.UploadTime = DateTime.Now;
                        _context.TblProjectFile.Add(DataProjectFile);
                        _context.SaveChanges();

                    }

                    //Masukkan File Memo
                    if (model.FileMemo != null)
                    {
                        TblProjectFile FileAttachment = new TblProjectFile();
                        var FileMemo = model.FileMemo;
                        string AllowedFileUploadType = ConfigAppsTypeFile.Value;
                        decimal SizeFile = FileMemo.Length / 1000000;

                        string Ext = Path.GetExtension(FileMemo.FileName);

                        //Validate Upload
                        if (!AllowedFileUploadType.Contains(Ext))
                        {
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadTypeFile"]);
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
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadMaxSize"]);
                        }

                        //create path directory
                        var PathFolder = ConfigAppsLocalPath.Value;

                        if (!Directory.Exists(PathFolder))
                        {
                            Directory.CreateDirectory(PathFolder);
                        }

                        var fileNameReplaceSpace = FileMemo.FileName.Replace(" ", "_");
                        var path = Path.Combine(PathFolder, fileNameReplaceSpace);
                        using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                        {
                            FileMemo.CopyTo(stream);
                        }

                        TblProjectFile DataProjectFile = new TblProjectFile();
                        DataProjectFile.ProjectId = dataProject.Id;
                        DataProjectFile.Keterangan = model.KeteranganMemo;
                        DataProjectFile.Size = FileMemo.Length / 1000;
                        DataProjectFile.TypeDokumenId = Utility.TypeDokumen_Memo;
                        DataProjectFile.NamaFile = Path.GetFileNameWithoutExtension(fileNameReplaceSpace);
                        DataProjectFile.FileType = FileMemo.ContentType;
                        DataProjectFile.FileExt = Ext;
                        DataProjectFile.FullPath = path;
                        DataProjectFile.Path = fileNameReplaceSpace;
                        DataProjectFile.UploadById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        DataProjectFile.UploadTime = DateTime.Now;
                        _context.TblProjectFile.Add(DataProjectFile);
                        _context.SaveChanges();

                    }

                    //Masukkan File Notulen
                    if (model.FileNotulen != null)
                    {
                        TblProjectFile FileAttachment = new TblProjectFile();
                        var FileNotulen = model.FileNotulen;
                        string AllowedFileUploadType = ConfigAppsTypeFile.Value;
                        decimal SizeFile = FileNotulen.Length / 1000000;

                        string Ext = Path.GetExtension(FileNotulen.FileName);

                        //Validate Upload
                        if (!AllowedFileUploadType.Contains(Ext))
                        {
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadTypeFile"]);
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
                            return Content(GetConfig.AppSetting["AppSettings:PathFolder:UploadMaxSize"]);
                        }

                        //create path directory
                        var PathFolder = ConfigAppsLocalPath.Value;

                        if (!Directory.Exists(PathFolder))
                        {
                            Directory.CreateDirectory(PathFolder);
                        }

                        var fileNameReplaceSpace = FileNotulen.FileName.Replace(" ", "_");
                        var path = Path.Combine(PathFolder, fileNameReplaceSpace);
                        using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                        {
                            FileNotulen.CopyTo(stream);
                        }

                        TblProjectFile DataProjectFile = new TblProjectFile();
                        DataProjectFile.ProjectId = dataProject.Id;
                        DataProjectFile.Keterangan = model.KeteranganNotulen;
                        DataProjectFile.Size = FileNotulen.Length / 1000;
                        DataProjectFile.TypeDokumenId = Utility.TypeDokumen_Notulen;
                        DataProjectFile.NamaFile = Path.GetFileNameWithoutExtension(fileNameReplaceSpace);
                        DataProjectFile.FileType = FileNotulen.ContentType;
                        DataProjectFile.FileExt = Ext;
                        DataProjectFile.FullPath = path;
                        DataProjectFile.Path = fileNameReplaceSpace;
                        DataProjectFile.UploadById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        DataProjectFile.UploadTime = DateTime.Now;
                        _context.TblProjectFile.Add(DataProjectFile);
                        _context.SaveChanges();

                    }

                    //Masukkan table client
                    foreach (var item in listDataProjectUser)
                    {
                        TblProjectUser dataClientNew = new TblProjectUser();
                        dataClientNew.ProjectId = dataProject.Id;
                        dataClientNew.ClientId = item.ClientId;
                        dataClientNew.NppPic = item.NppPic;
                        dataClientNew.NamaPic = item.NamaPic;
                        dataClientNew.NoHp = item.NoHp;
                        dataClientNew.Email = item.Email;
                        dataClientNew.Keterangan = item.Keterangan;
                        dataClientNew.CreatedById = item.CreatedById;
                        dataClientNew.CreatedTime = item.CreatedTime;
                        dataClientNew.UpdatedById = item.UpdatedById;
                        dataClientNew.UpdatedTime = item.UpdatedTime;
                        _context.TblProjectUser.Add(dataClientNew);
                    }

                    //Masukkan table relasi project
                    foreach (var item in listDataProjectRelasi)
                    {
                        TblProjectRelasi dataRelasi = new TblProjectRelasi();
                        dataRelasi.ProjectId = dataProject.Id;
                        dataRelasi.RelasiProjectId = item.RelasiProjectId;
                        dataRelasi.Keterangan = item.Keterangan;
                        dataRelasi.CreatedById = item.CreatedById;
                        dataRelasi.CreatedTime = item.CreatedTime;
                        dataRelasi.UpdatedById = item.UpdatedById;
                        dataRelasi.UpdatedTime = item.UpdatedTime;
                        _context.TblProjectRelasi.Add(dataRelasi);
                    }

                    //Masukkan table Anggota tim
                    foreach (var item in listDataAnggotaTim)
                    {
                        TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Id == item.PegawaiId).FirstOrDefault();
                        TblProjectMember dataMember = new TblProjectMember();
                        dataMember.ProjectId = dataProject.Id;
                        if (dataPegawai != null)
                        {
                            dataMember.UnitPegawaiId = dataPegawai.UnitId;
                        }

                        dataMember.PegawaiId = item.PegawaiId;
                        dataMember.UnitPegawaiId = _context.TblPegawai.Where(m => m.Id == item.PegawaiId).Select(m => m.UnitId).FirstOrDefault();
                        dataMember.JobPositionId = item.JobPositionId;
                        dataMember.SendAsTask = item.SendAsTask;
                        dataMember.Keterangan = item.Keterangan;
                        if (item.TanggalTargetPenyelesaianAnggotaTim != null)
                        {
                            var splitTanggal = item.TanggalTargetPenyelesaianAnggotaTim.Replace(" ", "").Split("-");

                            dataMember.StartDate = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dataMember.EndDate = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        dataMember.CreatedById = item.CreatedById;
                        dataMember.CreatedTime = item.CreatedTime;
                        dataMember.UpdatedById = item.UpdatedById;
                        dataMember.UpdatedTime = item.UpdatedTime;
                        _context.TblProjectMember.Add(dataMember);
                    }
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

        #region DATA CLIENT TEMP
        #region Data Client
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
        public ActionResult SubmitCreateClientTemp(DataProjectUser_ViewModels model)
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

                DataProjectUser_ViewModels CekUser = listDataProjectUser.Where(m => m.NppPic == model.NppPic).FirstOrDefault();

                if (CekUser != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:ClientSudahAda"]);
                }
                model.Id = int.Parse(DateTime.Now.ToString("HHmmss"));
                model.Client = _context.TblMasterClient.Where(m => m.Id == model.ClientId).Select(m => m.Nama).FirstOrDefault();
                model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                model.CreatedTime = DateTime.Now;
                listDataProjectUser.Add(model);

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Load Data Client
        public ActionResult LoadDataClientCreate()
        {

            return Json(new { data = listDataProjectUser });
        }
        #endregion

        #region Edit Client Temp
        public ActionResult EditClientTemp(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectUser_ViewModels DataUser = listDataProjectUser.Where(m => m.Id == Id).FirstOrDefault();

            if (DataUser == null)
            {
                DataUser = new DataProjectUser_ViewModels();
            }
            ViewBag.Client = new SelectList(Utility.SelectDataClient(DataUser.ClientId, _context).ToList(), "id", "text", DataUser.ClientId);

            return PartialView("_ModalClient", DataUser);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEditClientTemp(DataProjectUser_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }


                DataProjectUser_ViewModels DataProjectUser = listDataProjectUser.Where(m => m.Id == model.Id).FirstOrDefault();

                if (DataProjectUser != null)
                {
                    if (DataProjectUser.ClientId != model.ClientId)
                    {
                        DataProjectUser_ViewModels CekData = listDataProjectUser.Where(m => m.ClientId == model.ClientId).FirstOrDefault();

                        if (CekData != null)
                        {
                            return Content(GetConfig.AppSetting["AppSettings:Messages:ClientSudahAda"]);
                        }
                    }
                }

                //TblProjectUser DataUser = listDataProjectUser.Where(m => m.Id == model.Id).FirstOrDefault();
                model.Id = int.Parse(DateTime.Now.ToString("HHmmss"));
                model.NppPic = model.NppPic;
                model.NamaPic = model.NamaPic;
                model.NoHp = model.NoHp;
                model.Keterangan = model.Keterangan;
                model.ClientId = model.ClientId;
                model.Client = _context.TblMasterClient.Where(m => m.Id == model.ClientId).Select(m => m.Nama).FirstOrDefault();
                model.Email = model.Email;
                model.CreatedById = DataProjectUser.CreatedById;
                model.CreatedTime = DataProjectUser.CreatedTime;
                model.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                model.UpdatedTime = DateTime.Now;
                listDataProjectUser.Remove(DataProjectUser);
                listDataProjectUser.Add(model);

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Delete Client Temp
        public ActionResult DeleteClientTemp(int Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                DataProjectUser_ViewModels Transaksis = listDataProjectUser.Where(m => m.Id == Ids).FirstOrDefault(); //Ambil data sesuai dengan ID
                listDataProjectUser.Remove(Transaksis);

                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }
        #endregion
        #endregion

        #region DATA LINK PROJECT TEMP
        #region Data Link Project
        public ActionResult CreateRelasiProject()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Project = new SelectList("", "");


            return PartialView("_ModalRelasiProject");
        }

        [HttpPost]
        public ActionResult SubmitCreateRelasiProjectTemp(DataProjectRelasi_ViewModels model)
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

                DataProjectRelasi_ViewModels CekData = listDataProjectRelasi.Where(m => m.RelasiProjectId == model.RelasiProjectId).FirstOrDefault();

                if (CekData != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:RelasiProjectSudahAda"]);
                }
                TblProject dataProject = _context.TblProject.Where(m => m.Id == model.RelasiProjectId).FirstOrDefault();
                model.ProjectNo = dataProject.ProjectNo;
                model.NamaProject = dataProject.Nama;
                model.Id = int.Parse(DateTime.Now.ToString("HHmmss"));
                model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                model.CreatedTime = DateTime.Now;
                listDataProjectRelasi.Add(model);

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Load Data Relasi Project
        public ActionResult LoadDataRelasiProjectTemp()
        {
            return Json(new { data = listDataProjectRelasi });
        }
        #endregion

        #region Edit Relasi Project Temp
        public ActionResult EditRelasiProjectTemp(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectRelasi_ViewModels DataRelasi = listDataProjectRelasi.Where(m => m.Id == Id).FirstOrDefault();

            if (DataRelasi == null)
            {
                DataRelasi = new DataProjectRelasi_ViewModels();
            }
            ViewBag.Project = new SelectList(Utility.SelectDataProject(DataRelasi.RelasiProjectId, _context).ToList(), "id", "text", DataRelasi.RelasiProjectId);

            return PartialView("_ModalRelasiProject", DataRelasi);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEditRelasiProjectTemp(DataProjectRelasi_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }

                DataProjectRelasi_ViewModels DataRelasi = listDataProjectRelasi.Where(m => m.Id == model.Id).FirstOrDefault();

                if (DataRelasi != null)
                {
                    if (DataRelasi.RelasiProjectId != model.RelasiProjectId)
                    {
                        DataProjectRelasi_ViewModels CekData = listDataProjectRelasi.Where(m => m.RelasiProjectId == model.RelasiProjectId).FirstOrDefault();

                        if (CekData != null)
                        {
                            return Content(GetConfig.AppSetting["AppSettings:Messages:RelasiProjectSudahAda"]);
                        }
                    }
                }
                model.Id = int.Parse(DateTime.Now.ToString("HHmmss"));
                model.RelasiProjectId = model.RelasiProjectId;
                TblProject dataProject = _context.TblProject.Where(m => m.Id == model.RelasiProjectId).FirstOrDefault();
                model.ProjectNo = dataProject.ProjectNo;
                model.NamaProject = dataProject.Nama;
                model.CreatedById = DataRelasi.CreatedById;
                model.CreatedTime = DataRelasi.CreatedTime;
                model.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                model.UpdatedTime = DateTime.Now;
                listDataProjectRelasi.Remove(DataRelasi);
                listDataProjectRelasi.Add(model);

                _context.SaveChanges();

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Delete Relasi Project
        public ActionResult DeleteRelasiProjectTemp(int Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                DataProjectRelasi_ViewModels Transaksis = listDataProjectRelasi.Where(m => m.Id == Ids).FirstOrDefault(); //Ambil data sesuai dengan ID
                listDataProjectRelasi.Remove(Transaksis);

                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }
        #endregion
        #endregion

        #region DATA ANGGOTA TIM
        #region Data AnggotaTim
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
        public ActionResult SubmitCreateAnggotaTimTemp(DataProjectAnggotaTim_ViewModels model)
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

                DataProjectAnggotaTim_ViewModels CekData = listDataAnggotaTim.Where(m => m.PegawaiId == model.PegawaiId).FirstOrDefault();

                if (CekData != null)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Messages:AnggotaTimSudahAda"]);
                }
                //Get Data Pegawai
                TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Id == model.PegawaiId).FirstOrDefault();
                model.Npp = dataPegawai.Npp;
                model.Nama = dataPegawai.Nama;
                model.Email = dataPegawai.Email;
                model.Telepon = dataPegawai.NoHp;
                model.SendAsTask = model.SendAsTask;
                model.Unit = _context.TblUnit.Where(m => m.Id == dataPegawai.UnitId).Select(m => m.Name).FirstOrDefault();
                model.JobPosisi = _context.TblMasterJobPosition.Where(m => m.Id == dataPegawai.UnitId).Select(m => m.Nama).FirstOrDefault();
                model.TanggalTargetPenyelesaianAnggotaTim = model.TanggalTargetPenyelesaianAnggotaTim;
                model.Id = int.Parse(DateTime.Now.ToString("HHmmss"));
                model.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                model.CreatedTime = DateTime.Now;
                listDataAnggotaTim.Add(model);

                return Content("");
            }
            catch (Exception Ex)
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Load Data Anggota Tim
        public ActionResult LoadDataAnggotaTimTemp()
        {
            return Json(new { data = listDataAnggotaTim });
        }
        #endregion

        #region Edit Anggota Tim Temp
        public ActionResult EditAnggotaTimTemp(int Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            DataProjectAnggotaTim_ViewModels DataAnggotaTim = listDataAnggotaTim.Where(m => m.Id == Id).FirstOrDefault();

            if (DataAnggotaTim == null)
            {
                DataAnggotaTim = new DataProjectAnggotaTim_ViewModels();
            }
            ViewBag.Pegawai = new SelectList(Utility.SelectDataPegawai(DataAnggotaTim.PegawaiId, _context).ToList(), "id", "text", DataAnggotaTim.PegawaiId);
            ViewBag.JobPosition = new SelectList(Utility.SelectDataJobPosition(DataAnggotaTim.JobPositionId, _context).ToList(), "id", "text", DataAnggotaTim.JobPositionId);

            return PartialView("_ModalAnggotaTim", DataAnggotaTim);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEditAnggotaTimTemp(DataProjectAnggotaTim_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {

                    return RedirectToAction("Login", "Login", new { a = true });
                }
                DataProjectAnggotaTim_ViewModels NewData = new DataProjectAnggotaTim_ViewModels();
                DataProjectAnggotaTim_ViewModels DataAnggotaTim = listDataAnggotaTim.Where(m => m.Id == model.Id).FirstOrDefault();
                if (DataAnggotaTim != null)
                {
                    if (DataAnggotaTim.PegawaiId != model.PegawaiId)
                    {
                        DataProjectAnggotaTim_ViewModels CekData = listDataAnggotaTim.Where(m => m.PegawaiId == model.PegawaiId).FirstOrDefault();

                        if (CekData != null)
                        {
                            return Content(GetConfig.AppSetting["AppSettings:Messages:AnggotaTimSudahAda"]);
                        }
                    }

                }
                TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Id == model.PegawaiId).FirstOrDefault();
                NewData.PegawaiId = model.PegawaiId;
                NewData.JobPositionId = model.JobPositionId;
                NewData.Npp = dataPegawai.Npp;
                NewData.Nama = dataPegawai.Nama;
                NewData.Email = dataPegawai.Email;
                NewData.Telepon = dataPegawai.NoHp;
                NewData.SendAsTask = model.SendAsTask;
                NewData.Keterangan = model.Keterangan;
                NewData.Unit = _context.TblUnit.Where(m => m.Id == dataPegawai.UnitId).Select(m => m.Name).FirstOrDefault();
                NewData.JobPosisi = _context.TblMasterJobPosition.Where(m => m.Id == dataPegawai.UnitId).Select(m => m.Nama).FirstOrDefault();
                NewData.TanggalTargetPenyelesaianAnggotaTim = model.TanggalTargetPenyelesaianAnggotaTim;
                NewData.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                NewData.UpdatedTime = DateTime.Now;
                listDataAnggotaTim.Remove(DataAnggotaTim);
                listDataAnggotaTim.Add(NewData);
                //listDataAnggotaTim.
                //_context.Entry(DataAnggotaTim).State = EntityState.Modified;
                //_context.SaveChanges();

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }
        #endregion

        #region Delete Client Temp
        public ActionResult DeleteAnggotaTimTemp(int Ids)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                DataProjectAnggotaTim_ViewModels Transaksis = listDataAnggotaTim.Where(m => m.Id == Ids).FirstOrDefault(); //Ambil data sesuai dengan ID
                listDataAnggotaTim.Remove(Transaksis);

                return Content("");
            }
            catch
            {
                return Content("gagal");
            }
        }
        #endregion
        #endregion

        #region Cek Validasi Inputan
        [HttpPost]
        public ActionResult CekJumlahUser()
        {
            int Total = listDataProjectUser.Count();
            if (Total == 0)
            {
                return Content(GetConfig.AppSetting["AppSettings:Modul:CreateProject:EmptyUser"]);

            }
            return Content("");

        }

        [HttpPost]
        public ActionResult CekJumlahAnggotaTim()
        {
            int Total = listDataAnggotaTim.Count();
            if (Total == 0)
            {
                return Content(GetConfig.AppSetting["AppSettings:Modul:CreateProject:EmptyMember"]);

            }
            return Content("");

        }
        #endregion


    }
}
