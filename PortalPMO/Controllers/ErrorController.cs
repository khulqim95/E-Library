using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalPMO.Models;

namespace PortalPMO.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusErrorHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.Img = "../img/error404.png";
                    ViewBag.ErrorMessage = "Halaman tidak ditemukan";
                    break;
                case 500:
                    ViewBag.Img = "../img/error500.png";
                    ViewBag.ErrorMessage = "Terjadi kesalahan dalam sistem!";
                    break;
                case 405:
                    ViewBag.Img = "../img/access_denied.png";
                    ViewBag.ErrorMessage = "Maaf, Anda tidak mempunyai kewenangan akses!";
                    break;
                default:
                    ViewBag.Img = "../img/error500.png";
                    ViewBag.ErrorMessage = "Terjadi kesalahan dalam sistem!";
                    break;
            }

            return View("Error");
        }


        public IActionResult HttpStatusErrorLayout(int statusCode)
        {
            switch (statusCode)
            {
                case 204:
                    ViewBag.Img = "../img/error404.png";
                    ViewBag.ErrorMessage = "Data tidak ditemukan";
                    break;
                case 404:
                    ViewBag.Img = "../img/error404.png";
                    ViewBag.ErrorMessage = "Halaman tidak ditemukan";
                    break;
                case 500:
                    ViewBag.Img = "../img/error500.png";
                    ViewBag.ErrorMessage = "Terjadi kesalahan dalam sistem!";
                    break;
                case 405:
                    ViewBag.Img = "../img/access_denied.png";
                    ViewBag.ErrorMessage = "Maaf, Anda tidak mempunyai kewenangan akses!";
                    break;
                default:
                    ViewBag.Img = "../img/error500.png";
                    ViewBag.ErrorMessage = "Terjadi kesalahan dalam sistem!";
                    break;
            }

            return View("ErrorEmptyAccess");
        }

        public ActionResult SessionExp()
        {
            
            return View("SessionExp");
        }

        public IActionResult NotAccess()
        {
            ViewBag.Img = "../img/access_denied.png";
            ViewBag.ErrorMessage = "Maaf, Anda tidak mempunyai kewenangan akses!";
            return View("Error");
        }

        public IActionResult EmptyAccess()
        {
            ViewBag.Img = "../img/access_denied.png";
            ViewBag.ErrorMessage = "Maaf, Anda tidak mempunyai kewenangan akses, Silahkan hubungi admin!";
            return View("ErrorEmptyAccess");
        }


    }
}
