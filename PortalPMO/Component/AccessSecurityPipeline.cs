using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PortalPMO.Models.dbPortalPMO;
using TwoTierTemplate.Models;

namespace PortalPMO.Component
{
    public class AccessSecurityPipeline
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly dbPipelineContext _context;
        private readonly IConfiguration _configuration;
        public AccessSecurityPipeline(IHttpContextAccessor httpContextAccessor, dbPipelineContext context, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = config;
        }
        public bool IsGetAccess(string fullpath)
        {
            bool getAccess = false;
            var Role_Id = _httpContextAccessor.HttpContext.Session.GetString(SessionConstan.Session_Role_Id);
            //string fullpath = "../" + controller + "/Index";
            getAccess = StoredProcedureExecutor.ExecuteScalarBool(_context, "sp_CheckAccessMenu", new SqlParameter[]{
                        new SqlParameter("@Role_Id", Role_Id ),
                        new SqlParameter("@Url", fullpath)});
            //foreach(var item in data)
            //{
            //    if(item.HasAccess==1 || item.HasAccess1 == 1)
            //    {
            //        getAccess = true;
            //    }
            //}
            return getAccess;
        }
    }
}
