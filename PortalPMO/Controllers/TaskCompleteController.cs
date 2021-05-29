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

namespace PortalPMO.Controllers
{
    public class TaskCompleteController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public TaskCompleteController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                var ProjectNoSearchParam = dict["columns[2][search][value]"];
                var NamaProjectSearchParam = dict["columns[3][search][value]"];

                //Untuk mengetahui info jumlah page dan total skip data
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                var PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                var UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Unit_Id));


                List<DetailProjectMember_ViewModels> list = new List<DetailProjectMember_ViewModels>();

                list = StoredProcedureExecutor.ExecuteSPList<DetailProjectMember_ViewModels>(_context, "[sp_ProjectMember_View]", new SqlParameter[]{
                        new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@IsDone", 1),
                        new SqlParameter("@RoleIdLogin", RoleId),
                        new SqlParameter("@UnitIdLogin", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId),
                        new SqlParameter("@sortColumn", sortColumn),
                        new SqlParameter("@sortColumnDir", sortColumnDir),
                        new SqlParameter("@PageNumber", pageNumber),
                        new SqlParameter("@RowsPage", pageSize)});

                recordsTotal = StoredProcedureExecutor.ExecuteScalarInt(_context, "[sp_ProjectMember_Count]", new SqlParameter[]{
                       new SqlParameter("@ProjectNo", ProjectNoSearchParam),
                        new SqlParameter("@NamaProject", NamaProjectSearchParam),
                        new SqlParameter("@IsDone", 1),
                         new SqlParameter("@RoleIdLogin", RoleId),
                        new SqlParameter("@UnitIdLogin", UnitId),
                        new SqlParameter("@PegawaiIdLogin", PegawaiId)});

                if (list == null)
                {
                    list = new List<DetailProjectMember_ViewModels>();
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
        public ActionResult UpdateProgress()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }


            return PartialView("_UpdateProgress");
        }

        #endregion

        #region View Progress Pekerjaan
        public ActionResult ViewUpdateProgressPekerjaan(int id)
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login");
            }
            ProgressKerjaMember_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<ProgressKerjaMember_ViewModels>(_context, "[sp_ProjectMember_ProgressKerja_GetDataById]", new SqlParameter[]{
                       new SqlParameter("@Id", id)});

            return PartialView("_ModalDetailProgress", data);
        }


        #endregion
    }
}
