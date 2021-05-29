using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalPMO.Models.dbPortalPMO;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalPMO.Component
{
    public class ParentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public string GetValueConfigurationText(string Type)
        //{
        //    string res = "";
        //    using (dbFormasiPegawaiContext db = new dbFormasiPegawaiContext())
        //    {
        //        res = db.TblConfigurationText.Where(m => m.Type == Type).Select(m => m.Value).FirstOrDefault();
        //    }

        //    return res;
        //}

        public static string ConvertDate(string strDate)
        {
            if (strDate == "")
            {
                return null;
            }
            string[] sa = strDate.Split('/');
            string strNew = sa[2] + "-" + sa[1] + "-" + sa[0];
            return strNew;
        }
    }
}
