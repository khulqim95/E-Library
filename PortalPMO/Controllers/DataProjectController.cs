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
    public class DataProjectController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        public DataProjectController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                var NoProjectSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];
                var TanggalDeadlineSearchParam = dict["columns[4][search][value]"];
                var StatusProjectIdSearchParam = dict["columns[5][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<DataProject_ViewModels> list = new List<DataProject_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DataProject_ViewModels>(_context, "[sp_DataProject_View]", new SqlParameter[]{
                        new SqlParameter("@NoProject", NoProjectSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@Tanggal", TanggalDeadlineSearchParam),
                        new SqlParameter("@StatusProjectId", StatusProjectIdSearchParam),

                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_DataProject_Count]", new SqlParameter[]{
                           new SqlParameter("@NoProject", NoProjectSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@Tanggal", TanggalDeadlineSearchParam),
                        new SqlParameter("@StatusProjectId", StatusProjectIdSearchParam)});

                if (list == null)
                {
                    list = new List<DataProject_ViewModels>();
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
        public ActionResult SubmitCreate(TblMasterSistem model)
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
                    model.CreatedTime = DateTime.Now;
                    _context.TblMasterSistem.Add(model);
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
            TblMasterSistem data = _context.TblMasterSistem.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblMasterSistem();
            }

            return PartialView("_Edit", data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(DataProject_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                using (TransactionScope trx = new TransactionScope())
                {
                    TblProject dataProject = _context.TblProject.Where(m => m.Id == model.Id).FirstOrDefault();
                    if (dataProject.KategoriProjectId != model.KategoriProjectId)
                    {
                        TblMasterKategoriProject dataKategoriProject = _context.TblMasterKategoriProject.Where(m => m.Id == model.KategoriProjectId).FirstOrDefault();
                        TblMasterKategoriProject dataKategoriProjectLama = _context.TblMasterKategoriProject.Where(m => m.Id == dataProject.KategoriProjectId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Kategori Project").Replace("#NilaiAwal", dataKategoriProjectLama.Nama).Replace("#NilaiAkhir", dataKategoriProject.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.KategoriProjectId = model.KategoriProjectId;
                    if (dataProject.SubKategoriProjectId != model.SubKategoriProjectId)
                    {
                        TblMasterSubKategoriProject dataKategoriProject = _context.TblMasterSubKategoriProject.Where(m => m.Id == model.SubKategoriProjectId).FirstOrDefault();
                        TblMasterSubKategoriProject dataKategoriProjectLama = _context.TblMasterSubKategoriProject.Where(m => m.Id == dataProject.SubKategoriProjectId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Sub Kategori Project").Replace("#NilaiAwal", dataKategoriProjectLama.Nama).Replace("#NilaiAkhir", dataKategoriProject.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.SubKategoriProjectId = model.SubKategoriProjectId;

                    if (dataProject.SkorProjectId != model.SkorProjectId)
                    {
                        TblMasterSkorProject dataSkorProject = _context.TblMasterSkorProject.Where(m => m.Id == model.SkorProjectId).FirstOrDefault();
                        TblMasterSkorProject dataSkorProjectLama = _context.TblMasterSkorProject.Where(m => m.Id == dataProject.SkorProjectId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Sub Kategori Project").Replace("#NilaiAwal", dataSkorProjectLama.Nama).Replace("#NilaiAkhir", dataSkorProject.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.SkorProjectId = model.SkorProjectId;

                    if (dataProject.KompleksitasProjectId != model.KompleksitasProjectId)
                    {
                        TblMasterKompleksitasProject dataKompleksitasProject = _context.TblMasterKompleksitasProject.Where(m => m.Id == model.KompleksitasProjectId).FirstOrDefault();
                        TblMasterKompleksitasProject dataKompleksitasProjectLama = _context.TblMasterKompleksitasProject.Where(m => m.Id == dataProject.KompleksitasProjectId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Kompleksitas Project").Replace("#NilaiAwal", dataKompleksitasProjectLama.Nama).Replace("#NilaiAkhir", dataKompleksitasProject.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.KompleksitasProjectId = model.KompleksitasProjectId;

                    if (dataProject.KlasifikasiProjectId != model.KlasifikasiProjectId)
                    {
                        TblMasterKlasifikasiProject dataKlasifikasiProject = _context.TblMasterKlasifikasiProject.Where(m => m.Id == model.KlasifikasiProjectId).FirstOrDefault();
                        TblMasterKlasifikasiProject dataKlasifikasiProjectLama = _context.TblMasterKlasifikasiProject.Where(m => m.Id == dataProject.KlasifikasiProjectId).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Klasifikasi Project").Replace("#NilaiAwal", dataKlasifikasiProjectLama.Nama).Replace("#NilaiAkhir", dataKlasifikasiProject.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.KlasifikasiProjectId = model.KlasifikasiProjectId;

                    if (dataProject.PeriodeProjectId != model.PeriodeProjectId)
                    {
                        TblLookup dataPeriodeProject = _context.TblLookup.Where(m => m.Value == model.PeriodeProjectId && m.Type == "PeriodeProject" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
                        TblLookup dataPeriodeProjectLama = _context.TblLookup.Where(m => m.Value == dataProject.PeriodeProjectId && m.Type == "PeriodeProject" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
                        
                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Periode Project").Replace("#NilaiAwal", dataPeriodeProjectLama.Name).Replace("#NilaiAkhir", dataPeriodeProject.Name);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.PeriodeProjectId = model.PeriodeProjectId;

                    dataProject.NotifikasiId = model.NotifikasiId;

                    //if (dataProject.ProjectStatusId != model.ProjectStatusId)
                    //{
                    //    TblMasterStatusProject dataMasterStatusProjectLama = _context.TblMasterStatusProject.Where(m => m.Id == dataProject.ProjectStatusId).FirstOrDefault();
                    //    TblMasterStatusProject dataMasterStatusProject = _context.TblMasterStatusProject.Where(m => m.Id == model.ProjectStatusId).FirstOrDefault();

                    //    TblProjectLog dataLog = new TblProjectLog();
                    //    dataLog.ProjectId = model.Id;
                    //    dataLog.Tanggal = DateTime.Now;
                    //    dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    //    dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                    //    dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    //    dataLog.CreatedTime = DateTime.Now;
                    //    dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                    //    dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                    //    dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Status Project").Replace("#NilaiAwal", dataMasterStatusProjectLama.Nama).Replace("#NilaiAkhir", dataMasterStatusProject.Nama);
                    //    _context.TblProjectLog.Add(dataLog);
                    //    _context.SaveChanges();
                    //}
                    //dataProject.ProjectStatusId = model.ProjectStatusId;


                    if (dataProject.Nama != model.Nama )
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Nama Project").Replace("#NilaiAwal", dataProject.Nama).Replace("#NilaiAkhir", model.Nama);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.Nama = model.Nama;

                    if (dataProject.NoMemo != model.NoMemo)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah No Memo").Replace("#NilaiAwal", dataProject.NoMemo).Replace("#NilaiAkhir", model.NoMemo);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.NoMemo = model.NoMemo;

                    var TanggalMemoConvert = DateTime.ParseExact(model.TanggalMemo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dataProject.TanggalMemo != TanggalMemoConvert)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Tanggal Memo").Replace("#NilaiAwal", dataProject.TanggalMemo?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalMemo);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.TanggalMemo = TanggalMemoConvert;

                    if (dataProject.NoDrf != model.NoDrf)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah No DRF").Replace("#NilaiAwal", dataProject.NoDrf).Replace("#NilaiAkhir", model.NoDrf);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.NoDrf = model.NoDrf;

                    var TanggalDrfConvert = DateTime.ParseExact(model.TanggalDrf, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dataProject.TanggalDrf != TanggalDrfConvert)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Tanggal DRF").Replace("#NilaiAwal", dataProject.TanggalDrf?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalDrf);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.TanggalDrf = TanggalDrfConvert;

                    var TanggalDisposisiConvert = DateTime.ParseExact(model.TanggalDisposisi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dataProject.TanggalDisposisi != TanggalDisposisiConvert)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Tanggal Disposisi").Replace("#NilaiAwal", dataProject.TanggalDisposisi?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalDisposisi);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.TanggalDisposisi = TanggalDisposisiConvert;


                    var TanggalKlarifikasiConvert = DateTime.ParseExact(model.TanggalKlarifikasi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dataProject.TanggalKlarifikasi != TanggalKlarifikasiConvert)
                    {
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Ubah Tanggal Klarifikasi").Replace("#NilaiAwal", dataProject.TanggalKlarifikasi?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalKlarifikasi);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.TanggalKlarifikasi = TanggalKlarifikasiConvert;

                    if (model.TanggalEstimasiDone != null)
                    {
                        var splitTanggal = model.TanggalEstimasiDone.Replace(" ", "").Split("-");
                        DateTime TanggalEstimasiMulaiConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime TanggalEstimasiAkhirConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //Cek timeline 
                        if (dataProject.TanggalEstimasiMulai != TanggalEstimasiMulaiConvert && dataProject.TanggalEstimasiSelesai != TanggalEstimasiAkhirConvert)
                        {
                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.Id;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Timeline Project").Replace("#NilaiAwal", dataProject.TanggalEstimasiMulai?.ToString("dd/MM/yyyy") + " - " + dataProject.TanggalEstimasiSelesai?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiDone);
                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }

                        dataProject.TanggalEstimasiMulai = TanggalEstimasiMulaiConvert;
                        dataProject.TanggalEstimasiSelesai = TanggalEstimasiAkhirConvert;
                    }

                    if (model.TanggalEstimasiDevelopment != null)
                    {
                        var splitTanggal = model.TanggalEstimasiDevelopment.Replace(" ", "").Split("-");

                        DateTime TanggalEstimasiDevelopmentAwalConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime TanggalEstimasiDevelopmentAkhirConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //Cek estimasi develompent 
                        if (dataProject.TanggalEstimasiDevelopmentAwal != TanggalEstimasiDevelopmentAwalConvert && dataProject.TanggalEstimasiDevelopmentAkhir != TanggalEstimasiDevelopmentAkhirConvert)
                        {
                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.Id;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Timeline Development").Replace("#NilaiAwal", dataProject.TanggalEstimasiDevelopmentAwal?.ToString("dd/MM/yyyy") + " - " + dataProject.TanggalEstimasiDevelopmentAkhir?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiDevelopment);

                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }
                        dataProject.TanggalEstimasiDevelopmentAwal = TanggalEstimasiDevelopmentAwalConvert;
                        dataProject.TanggalEstimasiDevelopmentAkhir = TanggalEstimasiDevelopmentAkhirConvert;

                    }
                    if (model.TanggalEstimasiTesting != null)
                    {
                        var splitTanggal = model.TanggalEstimasiTesting.Replace(" ", "").Split("-");

                        DateTime TanggalEstimasiTestingAwalConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime TanggalEstimasiTestingAkhirConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //Cek estimasi develompent 
                        if (dataProject.TanggalEstimasiTestingAwal != TanggalEstimasiTestingAwalConvert && dataProject.TanggalEstimasiTestingAkhir != TanggalEstimasiTestingAkhirConvert)
                        {
                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.Id;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Timeline Testing").Replace("#NilaiAwal", dataProject.TanggalEstimasiTestingAwal?.ToString("dd/MM/yyyy") + " - " + dataProject.TanggalEstimasiTestingAkhir?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiTesting);

                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }
                        dataProject.TanggalEstimasiTestingAwal = TanggalEstimasiTestingAwalConvert;
                        dataProject.TanggalEstimasiTestingAkhir = TanggalEstimasiTestingAkhirConvert;
                    }

                    if (model.TanggalEstimasiPiloting != null)
                    {
                        var splitTanggal = model.TanggalEstimasiPiloting.Replace(" ", "").Split("-");

                        DateTime TanggalEstimasiPilotingAwalConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime TanggalEstimasiPilotingAkhirConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        dataProject.TanggalEstimasiPilotingAwal = TanggalEstimasiPilotingAwalConvert;
                        dataProject.TanggalEstimasiPilotingAkhir = TanggalEstimasiPilotingAkhirConvert;

                        //Cek estimasi timeline piloting 
                        if (dataProject.TanggalEstimasiPilotingAwal != TanggalEstimasiPilotingAwalConvert && dataProject.TanggalEstimasiPilotingAkhir != TanggalEstimasiPilotingAkhirConvert)
                        {
                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.Id;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Timeline Piloting").Replace("#NilaiAwal", dataProject.TanggalEstimasiPilotingAwal?.ToString("dd/MM/yyyy") + " - " + dataProject.TanggalEstimasiPilotingAkhir?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiPiloting);

                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }
                    }

                    if (model.TanggalEstimasiPir != null)
                    {
                        var splitTanggal = model.TanggalEstimasiPir.Replace(" ", "").Split("-");
                        DateTime TanggalEstimasiPirawalConvert = DateTime.ParseExact(splitTanggal[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime TanggalEstimasiPirakhirConvert = DateTime.ParseExact(splitTanggal[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        
                        //Cek estimasi timeline PIR 
                        if (dataProject.TanggalEstimasiPirawal != TanggalEstimasiPirawalConvert && dataProject.TanggalEstimasiPirakhir != TanggalEstimasiPirakhirConvert)
                        {
                            
                            //Masukkan ke dalam table Log
                            TblProjectLog dataLog = new TblProjectLog();
                            dataLog.ProjectId = model.Id;
                            dataLog.Tanggal = DateTime.Now;
                            dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                            dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            dataLog.CreatedTime = DateTime.Now;
                            dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                            dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Timeline PIR").Replace("#NilaiAwal", dataProject.TanggalEstimasiPirawal?.ToString("dd/MM/yyyy") + " - " + dataProject.TanggalEstimasiPirakhir?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiPir);
                            _context.TblProjectLog.Add(dataLog);
                            _context.SaveChanges();
                        }

                        dataProject.TanggalEstimasiPilotingAwal = TanggalEstimasiPirawalConvert;
                        dataProject.TanggalEstimasiPilotingAkhir = TanggalEstimasiPirakhirConvert;
                    }
                    var TanggalEstimasiProductionConvert = DateTime.ParseExact(model.TanggalEstimasiProduction, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    //Cek estimasi timeline PIR 
                    if (dataProject.TanggalEstimasiProduction != TanggalEstimasiProductionConvert)
                    {
                        
                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Estimasi Tanggal Production").Replace("#NilaiAwal", dataProject.TanggalEstimasiPirawal?.ToString("dd/MM/yyyy")).Replace("#NilaiAkhir", model.TanggalEstimasiProduction);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.TanggalEstimasiProduction = TanggalEstimasiProductionConvert;

                    if (dataProject.MandatoryId != model.MandatoryId)
                    {
                        TblLookup dataMandatroyProject = _context.TblLookup.Where(m => m.Value == model.MandatoryId && m.Type == "MandatoryKategori" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
                        TblLookup dataMandatroyProjectLama = _context.TblLookup.Where(m => m.Value == dataProject.MandatoryId && m.Type == "MandatoryKategori" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "Periode Mandatory Project").Replace("#NilaiAwal", dataMandatroyProjectLama.Name).Replace("#NilaiAkhir", dataMandatroyProject.Name);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }

                    dataProject.MandatoryId = model.MandatoryId;

                    if (dataProject.IsPir != model.isPIR)
                    {
                        TblLookup dataIsPIR = _context.TblLookup.Where(m => m.Value == model.isPIR && m.Type == "IsPIR" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();
                        TblLookup dataIsPIRLama = _context.TblLookup.Where(m => m.Value == dataProject.IsPir && m.Type == "IsPIR" && m.IsActive == true && m.IsDeleted == false).FirstOrDefault();

                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:MessageLogProject:UbahKategoriProject"].Replace("#NamaData", "PIR").Replace("#NilaiAwal", dataIsPIRLama.Name).Replace("#NilaiAkhir", dataIsPIR.Name);
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }
                    dataProject.IsPir = model.isPIR;

                    dataProject.DetailRequirment = model.DetailRequirment;
                    if(dataProject.DetailRequirment != model.DetailRequirment)
                    {
                        //Masukkan ke dalam table Log
                        TblProjectLog dataLog = new TblProjectLog();
                        dataLog.ProjectId = model.Id;
                        dataLog.Tanggal = DateTime.Now;
                        dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.UnitIdPegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));
                        dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                        dataLog.CreatedTime = DateTime.Now;
                        dataLog.LogActivityId = Utility.ActivityUpdateKategoriPerusahaan;
                        dataLog.LogActivityName = Utility.SelectLookupName("Activity", dataLog.LogActivityId, _context);
                        dataLog.Keterangan = "Ubah Detail Requirment";
                        dataLog.Komentar = model.DetailRequirment;
                        _context.TblProjectLog.Add(dataLog);
                        _context.SaveChanges();
                    }

                    dataProject.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataProject.UpdatedTime = DateTime.Now;
                    _context.Entry(dataProject).State = EntityState.Modified;
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

        #region Update Project
        public ActionResult UpdateProject(int ProjectId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            TblProject dataProject = _context.TblProject.Where(m => m.Id == ProjectId).FirstOrDefault();

            ViewBag.ProjectOpenCloseId = new SelectList(Utility.SelectLookup("ProjectStatus", _context), "Value", "Name",dataProject.CloseOpenId);


            return PartialView("_AssigmentForm");
        }

        [HttpPost]
        public ActionResult SubmitUpdateProject(string CatatanUser, int? ProjectStatusIdValue, int? ProjectId, int? ProjectOpenCloseId)
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
                    TblProject dataProject = _context.TblProject.Where(m => m.Id == ProjectId).FirstOrDefault();
                    dataProject.ProjectStatusId = ProjectStatusIdValue;
                    dataProject.CloseOpenId = ProjectOpenCloseId;
                    if (ProjectOpenCloseId == Utility.StatusProjectClosed)
                    {
                        //Cek apakah semua pegawai yang di assign tasknya sudah menyelesaikan tasknya atau belum
                        string ListNama = StoredProcedureExecutor.ExecuteScalarString(_context, "[Utility_CekProject_PendinganMember_ByNama]", new SqlParameter[]{
                        new SqlParameter("@ProjectId", dataProject.Id)});
                        if (ListNama != null)
                        {
                            string Res = GetConfig.AppSetting["AppSettings:Messages:GagalCloseProject"].Replace("#NamaMember",ListNama);
                            return Content("");

                        }
                        dataProject.TanggalSelesaiProject = DateTime.Now;
                    }
                    dataProject.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataProject.UpdatedTime = DateTime.Now;
                    _context.TblProject.Update(dataProject);
                    _context.SaveChanges();

                    
                    TblProjectLog dataLog = new TblProjectLog();
                    dataLog.LogActivityId = Utility.ActivityUbahStatusProject;
                    TblLookup dataLookup = _context.TblLookup.Where(m => m.Value == dataLog.LogActivityId && m.Type == "Activity").FirstOrDefault();
                    dataLog.Keterangan = dataLookup.Name;
                    dataLog.ProjectId = ProjectId;
                    dataLog.Komentar = CatatanUser;
                    dataLog.Tanggal = DateTime.Now;
                    dataLog.ProjectStatusForm = dataProject.ProjectStatusId;
                    dataLog.ProjectStatusTo = ProjectStatusIdValue;
                    dataLog.PegawaiIdFrom = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLog.CreatedTime = DateTime.Now;
                    _context.TblProjectLog.Add(dataLog);
                    _context.SaveChanges();
                    
                    //Masukkan ke dalam table log project status
                    TblProjectLogStatus dataLogStatus = new TblProjectLogStatus();
                    dataLogStatus.Projectid = dataProject.Id;
                    dataLogStatus.ProjectStatusForm = dataProject.ProjectStatusId;
                    dataLogStatus.ProjectStatusTo = ProjectStatusIdValue;
                    dataLogStatus.ProjectStatusFormValue = Utility.SelectProjectStatusName(dataLogStatus.ProjectStatusForm, _context);
                    dataLogStatus.ProjectStatusToValue = Utility.SelectProjectStatusName(dataLogStatus.ProjectStatusTo, _context);
                    dataLogStatus.Tanggal = DateTime.Now;
                    dataLogStatus.CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    dataLogStatus.CreatedTime = DateTime.Now;
                    _context.TblProjectLogStatus.Add(dataLogStatus);
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

        #region View
        public ActionResult View(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            TblMasterSistem data = _context.TblMasterSistem.Where(m => m.Id == id).FirstOrDefault();
            if (data == null)
            {
                data = new TblMasterSistem();
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

                List<TblMasterSistem> Transaksis = _context.TblMasterSistem.Where(x => confirmedDeleteId.Contains(x.Id)).ToList(); //Ambil data sesuai dengan ID
                for (int i = 0; i < confirmedDeleteId.Length; i++)
                {
                    TblMasterSistem data = _context.TblMasterSistem.Find(Transaksis[i].Id);
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
