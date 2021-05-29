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
    public class IconsController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public static string Global_Email = "";
        public static int IsPopup = 0;
        public static string Message = "";

        public static string NoHPvalidasiMember = "";


        public IconsController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
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
                return RedirectToAction("Login", "Login");
            }

            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");
            string Path = location.AbsolutePath;
            ViewBag.CurrentPath = Path;
            return View();
        }


      

        #region Random Code
        public string GenerateKode()
        {
            Random generator = new Random();
            return generator.Next(0, 999999).ToString("D6");
        }
        #endregion


        public IActionResult RoleChanged(string id)
        {
            lastSession.Update();
            var data = StoredProcedureExecutor.ExecuteSPSingle<DetailLogin_ViewModels>(_context, "sp_Change_Roles", new SqlParameter[]{
                        new SqlParameter("@id", id)
            });
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Pegawai);
            HttpContext.Session.Remove(SessionConstan.Session_Pegawai_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Unit_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Unit);
            //HttpContext.Session.Remove(SessionConstan.Session_User_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Unit_Id);
            HttpContext.Session.Remove(SessionConstan.Session_Role_Nama_Unit);
            HttpContext.Session.Remove(SessionConstan.Session_Nama_Role);
            HttpContext.Session.Remove(SessionConstan.Session_Images_User);
            HttpContext.Session.Remove(SessionConstan.Session_Status_Role);
            HttpContext.Session.Remove(SessionConstan.Session_User_Role_Id);


            HttpContext.Session.SetString(SessionConstan.Session_Nama_Pegawai, data.Nama_Pegawai == null ? "-" : data.Nama_Pegawai);
            HttpContext.Session.SetString(SessionConstan.Session_Pegawai_Id, data.Pegawai_Id == null ? "" : data.Pegawai_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Unit_Id, data.Unit_Id == null ? "" : data.Unit_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Nama_Unit, data.Nama_Unit == null ? "-" : data.Nama_Unit);
            //HttpContext.Session.SetString(SessionConstan.Session_User_Id, data.User_Id == null ? "" : data.User_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Id, data.Role_Id == null ? "" : data.Role_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Unit_Id, data.Role_Unit_Id == null ? "" : data.Role_Unit_Id);
            HttpContext.Session.SetString(SessionConstan.Session_Role_Nama_Unit, data.Role_Nama_Unit == null ? "-" : data.Role_Nama_Unit);
            HttpContext.Session.SetString(SessionConstan.Session_Nama_Role, data.Nama_Role == null ? "-" : data.Nama_Role);
            HttpContext.Session.SetString(SessionConstan.Session_Images_User, data.Images_User == null ? GetConfig.AppSetting["AppSettings:GlobalSettings:DefaultImageUser"] : data.Images_User);
            HttpContext.Session.SetString(SessionConstan.Session_Status_Role, data.Status_Role == null ? "-" : data.Status_Role);
            HttpContext.Session.SetString(SessionConstan.Session_User_Role_Id, data.User_Role_Id == null ? "-" : data.User_Role_Id);

            // Get the menus Assigment
            var menuAss = _context.NavigationAssignment.Where(na => na.RoleId == int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id))).ToList();
            List<NavigationVM> ListNav = new List<NavigationVM>();
            foreach (var item in menuAss)
            {
                var nav = _context.Navigation.Where(nv => nv.Id == item.NavigationId && nv.Visible == 1 && nv.IsDeleted != true).FirstOrDefault();
                if (nav != null)
                {
                    NavigationVM navres = new NavigationVM
                    {
                        CreatedById = nav.CreatedById,
                        CreatedTime = nav.CreatedTime,
                        DeletedById = nav.DeletedById,
                        DeletedTime = nav.DeletedTime,
                        IconClass = nav.IconClass,
                        Id = nav.Id,
                        IsDeleted = nav.IsDeleted,
                        Name = nav.Name,
                        Order = nav.Order,
                        ParentNavigationId = nav.ParentNavigationId,
                        Route = nav.Route,
                        Type = nav.Type,
                        UpdatedById = nav.UpdatedById,
                        UpdatedTime = nav.UpdatedTime,
                        Visible = nav.Visible
                    };
                    ListNav.Add(navres);
                }
            }

            HttpContext.Session.SetObject("AllMenu", ListNav);

            //HttpContext.Session.SetString(SessionConstan.Session_Role_Id, Result.Role_Id+"");
            //HttpContext.Session.SetString(SessionConstan.Session_Unit_Id, Result.Unit_Id + "");

            //var RoleId = HttpContext.Session.GetString(SessionConstan.Session_User_Role_Id);
            if (ListNav.Count == 0)
            {
                return Content("EmptyAccess");
            }


            return Content("Home");
        }
    }
}
