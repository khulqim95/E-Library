using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using PortalPMO.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwoTierTemplate.Models;
using UAParser;

namespace TwoTierTemplate.Component
{
    public class LastSessionLogPipeline
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        public LastSessionLogPipeline(IHttpContextAccessor httpContextAccessor, dbPipelineContext context, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = config;
        }

        public bool Update()
        {
            bool isSession = false;
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            // get user ip info
            var ipAddress = heserver.AddressList.ToList().Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.
            InterNetwork).FirstOrDefault().ToString();


            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            string uaString = Convert.ToString(userAgent[0]);
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(uaString);

            var url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();

            TblUserSession UserSession = new TblUserSession();

            if (_httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_User_Id) == null)
            {
                //AccountController cont = new AccountController(_configuration, _context, _httpContextAccessor);
                //cont.CekSession();
                isSession = false;
            }
            else
            {
                var userid = int.Parse(_httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_User_Id));
                //var Npp = _httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_NPP_Pegawai);

                TblUserSession ds = _context.TblUserSession.Where(f => f.UserId == userid).FirstOrDefault();
                if (ds != null)
                {
                    ds.SessionId = _httpContextAccessor.HttpContext.Session.Id;
                    ds.LastActive = DateTime.Now;
                    ds.RoleId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_Role_Id));
                    ds.Info = ipAddress;

                    _context.TblUserSession.Update(ds);
                    _context.SaveChanges();
                }
                else
                {
                    UserSession.UserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_User_Id));
                    UserSession.SessionId = _httpContextAccessor.HttpContext.Session.Id;
                    UserSession.LastActive = DateTime.Now;
                    UserSession.Info = ipAddress;
                    UserSession.RoleId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_Role_Id));

                    _context.TblUserSession.Add(UserSession);
                    _context.SaveChanges();
                }

                //Masukkan ke dalam table Log Activity
                TblLogActivity dataLog = new TblLogActivity();
                dataLog.UserId = userid;
                //dataLog.Npp = Npp;
                dataLog.Url = url;
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
                _context.TblLogActivity.Add(dataLog);
                _context.SaveChanges();
                isSession = true;
            }
            return isSession;
        }
    }
}
