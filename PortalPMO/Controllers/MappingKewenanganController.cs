using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace PortalPMO.Controllers
{
    public class MappingKewenanganController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        private readonly LastSessionLog lastSession;
        private readonly AccessSecurity accessSecurity;
        IHttpContextAccessor _accessor;
        private readonly HttpClient _client;
        public MappingKewenanganController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                var jabatanName = dict["columns[2][search][value]"];
                var unitName = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<MappingKewenangan_VM> list = new List<MappingKewenangan_VM>();

                list = StoredProcedureExecutor.ExecuteSPList<MappingKewenangan_VM>(_context, "[Sp_LoadKewenanganJabatan_View]", new SqlParameter[]{
                        new SqlParameter("@JabatanName", jabatanName),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)
                });

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[Sp_LoadKewenanganJabatan_Count]", new SqlParameter[]{
                        new SqlParameter("@UnitName", unitName),
                        new SqlParameter("@JabatanName", jabatanName)
                });

                if (list == null)
                {
                    list = new List<MappingKewenangan_VM>();
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
        public async Task<ActionResult> Create()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.RolePegawai = new SelectList(Utility.SelectDataMasterRole(_context), "id", "text"); 
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> SubmitCreate(MappingKewenangan_VM model, string RolesId)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }

            try
            {
                var cekDataMapping = await _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == model.Jabatan_Id
                    && x.IsDeleted == false).ToListAsync();

                if (cekDataMapping.Count != 0)
                {
                    return Content("Data Sudah Pernah diinput sebelumnya, silahkan edit Role data tersebut");
                }
                using (TransactionScope trx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //TblMasterJabatan getDataJabatan = await _context.TblMasterJabatan.Where(x => x.Id == req.Jabatan_Id).FirstOrDefaultAsync().ConfigureAwait(false);
                    var getReqRole = RolesId.Split(',').Select(int.Parse).ToList();
                    //var getLookup = await _context.TblLookup.Where(x => x.Value == req.StatusJabatanId 
                    //    && x.Type == "StatusRole").Select(x => x.Name).FirstOrDefaultAsync().ConfigureAwait(false);

                    foreach (var dataRole in getReqRole)
                    {
                        TblMappingKewenanganJabatan inputData = new TblMappingKewenanganJabatan
                        {
                            IsActive = model.IsActive,
                            CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)),
                            CreatedTime = DateTime.Now,
                            IsDeleted = false,
                            JabatanId = model.Jabatan_Id,
                            Keterangan = model.Keterangan,
                            RoleId = dataRole
                        };

                        await _context.TblMappingKewenanganJabatan.AddAsync(inputData).ConfigureAwait(false);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
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
            //TblMappingKewenanganJabatan data = _context.TblMappingKewenanganJabatan.Where(m => m.Id == id).FirstOrDefault();

            var _ = _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == id
                && x.IsDeleted == false).ToList();

            string roles = "";
            for (int i = 0; i < _.Count(); i++)
            {
                //roles = roles + _[i].RoleId.ToString() + ",";
                int cek = i + 1;
                if (cek != _.Count())
                {
                    roles = roles + _[i].RoleId.ToString() + ",";
                }
                else
                {
                    roles = roles + _[i].RoleId.ToString();
                }
            }

            var getDataJabatanName = _context.TblMasterJabatan.Where(x => x.Id == _[0].JabatanId
                && x.IsDeleted == false).Select(x => x.Nama).FirstOrDefault();

            var dataRole = new SelectList(_context.TblMasterRole.Where(x => x.IsDeleted == false).ToList(), "Id", "Nama");

            MappingKewenangan_VM outputData = new MappingKewenangan_VM
            {
                IsActive = Convert.ToBoolean(_[0].IsActive),
                Jabatan_Id = _[0].JabatanId,
                Keterangan = _[0].Keterangan,
                Roles = roles,
                listRole = dataRole,
                JabatanName = getDataJabatanName
            };

            ViewBag.RolePegawai = outputData.listRole;
            ViewBag.NavigationAssignment = outputData.Roles;

            if (outputData == null)
            {
                outputData = new MappingKewenangan_VM();
            }

            return PartialView("_Edit", outputData);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitEdit(MappingKewenangan_VM model, string RolesId)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                using (TransactionScope trx = new TransactionScope())
                {
                    var listDelData = _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == model.Jabatan_Id
                        && x.IsDeleted == false).ToList();
                    if (listDelData != null)
                    {
                        foreach (var delData in listDelData)
                        {
                            delData.IsDeleted = true;
                            delData.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                            delData.DeletedTime = DateTime.Now;

                            _context.TblMappingKewenanganJabatan.Update(delData);
                            _context.SaveChanges();
                        }
                    }

                    var cekDataMapping = _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == model.Jabatan_Id && x.IsDeleted == false).ToList();

                    if (cekDataMapping.Count != 0)
                    {
                        return Content("data already exist");
                    }

                    TblMappingKewenanganJabatan dataMapping = new TblMappingKewenanganJabatan();
                    var getReqRole = RolesId.Split(',').Select(int.Parse).ToList();

                    foreach (var dataRole in getReqRole)
                    {
                        TblMappingKewenanganJabatan inputData = new TblMappingKewenanganJabatan
                        {
                            IsActive = model.IsActive,
                            CreatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id)),
                            CreatedTime = DateTime.Now,
                            IsDeleted = false,
                            JabatanId = model.Jabatan_Id,
                            Keterangan = model.Keterangan,
                            RoleId = dataRole
                        };

                        _context.TblMappingKewenanganJabatan.Add(inputData);
                        _context.SaveChanges();
                    }
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

        public async Task<ActionResult> View(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });

            }
            //TblMappingKewenanganJabatan data = _context.TblMappingKewenanganJabatan.Where(m => m.Id == id).FirstOrDefault();

            var _ = _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == id
                && x.IsDeleted == false).ToList();

            string roles = "";
            for (int i = 0; i < _.Count(); i++)
            {
                //roles = roles + _[i].RoleId.ToString() + ",";
                int cek = i + 1;
                if (cek != _.Count())
                {
                    roles = roles + _[i].RoleId.ToString() + ",";
                }
                else
                {
                    roles = roles + _[i].RoleId.ToString();
                }
            }

            var getDataJabatanName = _context.TblMasterJabatan.Where(x => x.Id == _[0].JabatanId
                && x.IsDeleted == false).Select(x => x.Nama).FirstOrDefault();

            var dataRole = new SelectList(_context.TblMasterRole.Where(x => x.IsDeleted == false).ToList(), "Id", "Nama");

            MappingKewenangan_VM outputData = new MappingKewenangan_VM
            {
                IsActive = Convert.ToBoolean(_[0].IsActive),
                Jabatan_Id = _[0].JabatanId,
                Keterangan = _[0].Keterangan,
                Roles = roles,
                listRole = dataRole,
                JabatanName = getDataJabatanName
            };

            ViewBag.RolePegawai = outputData.listRole;
            ViewBag.NavigationAssignment = outputData.Roles;

            if (outputData == null)
            {
                outputData = new MappingKewenangan_VM();
            }

            return PartialView("_View", outputData);
        }

        #endregion

        #region Delete
        public ActionResult Delete(int Jabatan_Id, int statusJabatanId, int unit_Id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            try
            {
                var _data = _context.TblMappingKewenanganJabatan.Where(x => x.JabatanId == Jabatan_Id
                               && x.IsDeleted == false).ToList();

                if (_data == null)
                {
                    return Content("Tidak Bisa Dihapus");
                }
                foreach (var data in _data)
                {
                    data.IsDeleted = true;
                    data.DeletedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.DeletedTime = DateTime.Now;

                    _context.TblMappingKewenanganJabatan.Update(data);
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