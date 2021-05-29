using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PortalPMO.Component;
using PortalPMO.ViewModels;
using TwoTierTemplate.Component;
using TwoTierTemplate.Models;
using UAParser;

namespace PortalPMO.Controllers
{
    public class LoginController : Controller
    {

        //private readonly dbPortalPMOContext _context;
        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLogPipeline lastSession;

        public static bool isLogout = false;
        public LoginController(IConfiguration config, dbPipelineContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _accessor = accessor;
            lastSession = new LastSessionLogPipeline(accessor, context, config);
        }


        public IActionResult Login(bool? a)
        {
            if (HttpContext.Session.GetString(SessionConstan.Session_Nama_Pegawai) == null)
            {

                ViewBag.Tahun = DateTime.Now.Year;
                ViewBag.Islogout = a;
                return View();
            }
            else
            {
                lastSession.Update();
                return RedirectToAction("Index", "Home");
            }
        }

        public bool CekSession()
        {
            bool ret = false;
            if (_accessor.HttpContext.Session.GetString(SessionConstan.Session_User_Id) != null)
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(Login_ViewModels model)
        {
            bool LoginAllowed = false;
            DetailLogin_ViewModels data = new DetailLogin_ViewModels();
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            // get user ip info
            var ipAddress = heserver.AddressList.ToList().Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.
            InterNetwork).FirstOrDefault().ToString();

            string host = _accessor.HttpContext.Request.Host.Value;
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

            if (model.Username != null && model.Password != null)
            {
                try
                {
                    //Ambil data pegawai
                    data = StoredProcedureExecutor.ExecuteSPSingle<DetailLogin_ViewModels>(_context, "SP_Login_GetData", new SqlParameter[]{
                        new SqlParameter("@Username", model.Username)});

                    if (data == null)
                    {
                        ViewBag.ErrorMessage = GetConfig.AppSetting["AppSettings:Login:UserBelumTerdaftar"];
                        return View();
                    }
                    else if (data.IsActive == false)
                    {
                        ViewBag.ErrorMessage = GetConfig.AppSetting["AppSettings:Login:UserTidakAktif"];
                        return View();
                    }
                    else if (model.Password == GetConfig.AppSetting["AppSettings:GlobalMessage:GlobalPassword"])
                    {
                        LoginAllowed = true;
                    }
                    else if ((data.LDAPLogin == false || data.LDAPLogin == null) && data.Password == model.Password)
                    {
                        LoginAllowed = true;
                    }
                    else if (data.LDAPLogin != null && data.LDAPLogin == true) //Cek password LDAP
                    {
                        if (Utility.AuthenticationLdap(model.Username, model.Password))
                        {
                            LoginAllowed = true;

                        }
                        else
                        {
                            //Masukkan ke dalam table Log Activity
                            var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                            string uaString = Convert.ToString(userAgent[0]);
                            var uaParser = Parser.GetDefault();
                            ClientInfo c = uaParser.Parse(uaString);

                            TblLogActivity dataLog = new TblLogActivity();
                            dataLog.Npp = model.Username;
                            dataLog.Url = "../Login";
                            dataLog.ActionTime = DateTime.Now;
                            if (c != null)
                            {
                                if (c.UserAgent != null)
                                {
                                    dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                                }

                                if (c.OS != null)
                                {
                                    dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                                }
                            }
                            dataLog.Ip = ipAddress;
                            dataLog.ClientInfo = c.String;
                            dataLog.Keterangan = GetConfig.AppSetting["AppSettings:Login:SalahUserPassword"];
                            _context.TblLogActivity.Add(dataLog);
                            _context.SaveChanges();

                            ViewBag.ErrorMessage = GetConfig.AppSetting["AppSettings:Login:SalahUserPassword"];
                            return View();
                        }
                    }
                    else
                    {

                        //Masukkan ke dalam table Log Activity
                        var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                        string uaString = Convert.ToString(userAgent[0]);
                        var uaParser = Parser.GetDefault();
                        ClientInfo c = uaParser.Parse(uaString);

                        TblLogActivity dataLog = new TblLogActivity();
                        dataLog.Npp = model.Username;
                        dataLog.Url = "../Login";
                        dataLog.ActionTime = DateTime.Now;
                        if (c != null)
                        {
                            if (c.UserAgent != null)
                            {
                                dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                            }

                            if (c.OS != null)
                            {
                                dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                            }
                        }
                        dataLog.Ip = ipAddress;
                        dataLog.ClientInfo = c.String;
                        dataLog.Keterangan = GetConfig.AppSetting["AppSettings:Login:SalahUserPassword"];
                        _context.TblLogActivity.Add(dataLog);
                        _context.SaveChanges();

                        ViewBag.ErrorMessage = GetConfig.AppSetting["AppSettings:Login:SalahUserPassword"];
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage += string.Format(ex.Message);
                }
            }
            else
            {

                //Masukkan ke dalam table Log Activity
                var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                string uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(uaString);

                TblLogActivity dataLog = new TblLogActivity();
                dataLog.Npp = model.Username;
                dataLog.Url = "../Login";
                dataLog.ActionTime = DateTime.Now;
                if (c != null)
                {
                    if (c.UserAgent != null)
                    {
                        dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                    }

                    if (c.OS != null)
                    {
                        dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                    }
                }
                dataLog.Ip = ipAddress;
                dataLog.ClientInfo = c.String;
                dataLog.Keterangan = string.Format("Username dan password tidak boleh kosong!");
                _context.TblLogActivity.Add(dataLog);
                _context.SaveChanges();

                ViewBag.ErrorMessage += string.Format("Username dan password tidak boleh kosong!");
                return View();

            }

            //Cek apakah ada role PJB yang aktif
            if (data.Role_Id == null)
            {

                //Masukkan ke dalam table Log Activity
                var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                string uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(uaString);

                TblLogActivity dataLog = new TblLogActivity();
                dataLog.Npp = model.Username;
                dataLog.Url = "../Login";
                dataLog.ActionTime = DateTime.Now;
                if (c != null)
                {
                    if (c.UserAgent != null)
                    {
                        dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                    }

                    if (c.OS != null)
                    {
                        dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                    }
                }
                dataLog.Ip = ipAddress;
                dataLog.ClientInfo = c.String;
                dataLog.Keterangan = string.Format("Anda tidak mempunyai ijin akses aplikasi ini!");
                _context.TblLogActivity.Add(dataLog);
                _context.SaveChanges();

                ViewBag.ErrorMessage += string.Format("Anda tidak mempunyai ijin akses aplikasi ini!");
                return View();
            }

            var Role_Id = data.Role_Id;

            List<NavigationVM> ListNav = new List<NavigationVM>();

            ListNav = StoredProcedureExecutor.ExecuteSPList<NavigationVM>(_context, "sp_GetMenu", new SqlParameter[]{
                        new SqlParameter("@Role_Id", Role_Id)});
            if (ListNav == null)
            {
                //Masukkan ke dalam table Log Activity
                var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                string uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(uaString);

                TblLogActivity dataLog = new TblLogActivity();
                dataLog.Npp = model.Username;
                dataLog.Url = "../Login";
                dataLog.ActionTime = DateTime.Now;
                if (c != null)
                {
                    if (c.UserAgent != null)
                    {
                        dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                    }

                    if (c.OS != null)
                    {
                        dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                    }
                }
                dataLog.Ip = ipAddress;
                dataLog.ClientInfo = c.String;
                dataLog.Keterangan = string.Format("Anda tidak mempunyai ijin akses aplikasi ini!");
                _context.TblLogActivity.Add(dataLog);
                _context.SaveChanges();

                ViewBag.ErrorMessage += string.Format("Anda tidak mempunyai ijin akses aplikasi ini!");
                return View();

            }

            if (LoginAllowed)
            {
                try
                {
                    string IPAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                    HttpContext.Session.SetString(SessionConstan.Session_Nama_Pegawai, data.Nama_Pegawai == null ? "-" : data.Nama_Pegawai);
                    HttpContext.Session.SetString(SessionConstan.Session_NPP_Pegawai, data.Npp == null ? "" : data.Npp);
                    HttpContext.Session.SetString(SessionConstan.Session_Pegawai_Id, data.Pegawai_Id == null ? "" : data.Pegawai_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Unit_Id, data.Unit_Id == null ? "" : data.Unit_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Nama_Unit, data.Nama_Unit == null ? "-" : data.Nama_Unit);
                    HttpContext.Session.SetString(SessionConstan.Session_User_Id, data.User_Id == null ? "" : data.User_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Role_Id, data.Role_Id == null ? "" : data.Role_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Role_Unit_Id, data.Role_Unit_Id == null ? "" : data.Role_Unit_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Role_Nama_Unit, data.Role_Nama_Unit == null ? "-" : data.Role_Nama_Unit);
                    HttpContext.Session.SetString(SessionConstan.Session_Nama_Role, data.Nama_Role == null ? "-" : data.Nama_Role);
                    HttpContext.Session.SetString(SessionConstan.Session_Images_User, data.Images_User == null ? GetConfig.AppSetting["AppSettings:GlobalSettings:DefaultImageUser"] : data.Images_User);
                    HttpContext.Session.SetString(SessionConstan.Session_Status_Role, data.Status_Role == null ? "-" : data.Status_Role);
                    HttpContext.Session.SetString(SessionConstan.Session_User_Role_Id, data.User_Role_Id == null ? "-" : data.User_Role_Id);
                    HttpContext.Session.SetString(SessionConstan.Session_Wilayah_Id, data.Wilayah_Id == null ? "-" : data.Wilayah_Id);


                    HttpContext.Session.SetObject("AllMenu", ListNav);

                    //Masukkan ke dalam table user session
                    TblUserSession UserSession = new TblUserSession();
                    int userid = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_User_Id));
                    TblUserSession ds = _context.TblUserSession.Where(f => f.UserId == userid).FirstOrDefault();
                    if (ds != null)
                    {
                        ds.SessionId = HttpContext.Session.Id;
                        ds.LastActive = DateTime.Now;
                        ds.RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                        ds.UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));
                        ds.Info = ipAddress;

                        _context.TblUserSession.Update(ds);
                        _context.SaveChanges();
                    }
                    else
                    {
                        UserSession.UserId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_User_Id));
                        UserSession.SessionId = HttpContext.Session.Id;
                        UserSession.LastActive = DateTime.Now;
                        UserSession.Info = ipAddress;
                        UserSession.RoleId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                        UserSession.UnitId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Unit_Id));

                        _context.TblUserSession.Add(UserSession);
                        _context.SaveChanges();
                    }

                    //Update table user
                    TblUser dataUser = _context.TblUser.Where(m => m.Id == userid).FirstOrDefault();
                    if (dataUser != null)
                    {
                        dataUser.LastLogin = DateTime.Now;
                        _context.TblUser.Update(dataUser);
                        _context.SaveChanges();
                    }

                    //Update table Pegawai
                    TblPegawai dataPegawai = _context.TblPegawai.Where(m => m.Npp == model.Username).FirstOrDefault();
                    if (dataPegawai != null)
                    {
                        dataPegawai.Lastlogin = DateTime.Now;
                        _context.TblPegawai.Update(dataPegawai);
                        _context.SaveChanges();
                    }

                    //Masukkan ke dalam table Log Activity
                    var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                    string uaString = Convert.ToString(userAgent[0]);
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(uaString);

                    TblLogActivity dataLog = new TblLogActivity();
                    dataLog.UserId = userid;
                    dataLog.Npp = HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai);
                    dataLog.Url = "../Login";
                    dataLog.ActionTime = DateTime.Now;
                    if (c != null)
                    {
                        if (c.UserAgent != null)
                        {
                            dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                        }

                        if (c.OS != null)
                        {
                            dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                        }
                    }
                    dataLog.Ip = ipAddress;
                    dataLog.ClientInfo = c.String;
                    dataLog.Keterangan = "Login Sukses";
                    _context.TblLogActivity.Add(dataLog);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    return Content("");
                }                
            }
            else
            {
                //Masukkan ke dalam table Log Activity
                var userAgent = _accessor.HttpContext.Request.Headers["User-Agent"];
                string uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(uaString);

                TblLogActivity dataLog = new TblLogActivity();
                dataLog.Npp = model.Username;
                dataLog.Url = "../Login";
                dataLog.ActionTime = DateTime.Now;
                if (c != null)
                {
                    if (c.UserAgent != null)
                    {
                        dataLog.Browser = c.UserAgent.Family + "." + c.UserAgent.Major + "." + c.UserAgent.Minor;

                    }

                    if (c.OS != null)
                    {
                        dataLog.Os = c.OS.Family + " " + c.OS.Major + " " + c.OS.Minor;
                    }
                }
                dataLog.Ip = ipAddress;
                dataLog.ClientInfo = c.String;
                dataLog.Keterangan = "Gagal Login";
                _context.TblLogActivity.Add(dataLog);
                _context.SaveChanges();

                if (ListNav.Count == 0)
                {
                    return RedirectToAction("NotAccess", "Error");
                }

                return View();
            }
        }



        public ActionResult Logout()
        {
            isLogout = true;
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
