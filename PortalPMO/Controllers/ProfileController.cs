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
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PortalPMO.Controllers
{
    public class ProfileController : Controller
    {
        private readonly dbPortalPMOContext _context;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _accessor;
        private readonly LastSessionLog lastSession;

        public ProfileController(IConfiguration config, dbPortalPMOContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = config;
            _accessor = accessor;
            lastSession = new LastSessionLog(accessor, context, config);
        }

        #region Get Profile
        public JsonResult GetInfoUser()
        {
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));

            Profile_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<Profile_ViewModels>(_context, "[Sp_Profile_GetDatePegawai]", new SqlParameter[]{
                        new SqlParameter("@PegawaiId", PegawaiId)});

            HttpContext.Session.SetString(SessionConstan.Session_Images_User, data.Images == null ? GetConfig.AppSetting["AppSettings:GlobalSettings:DefaultImageUser"] : data.Images);

            return Json(data);
        }
        #endregion
        public IActionResult Edit()
        {
            if (!lastSession.Update())
            {
                return RedirectToAction("Login", "Login", new { a = true });
            }
            int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));

            Profile_ViewModels data = StoredProcedureExecutor.ExecuteSPSingle<Profile_ViewModels>(_context, "[Sp_Profile_GetDatePegawai]", new SqlParameter[]{
                        new SqlParameter("@PegawaiId", PegawaiId)});

            return View(data);
        }

        [HttpPost]
        public ActionResult SubmitEdit(Profile_ViewModels model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));

                TblPegawai data = _context.TblPegawai.Where(m => m.Id == PegawaiId).FirstOrDefault(); // Ambil data sesuai dengan ID
                data.Nama = model.Nama_Pegawai;
                data.Npp = model.Npp;
                data.Email = model.Email;
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedDate = DateTime.Now;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        [HttpPost]
        public ActionResult SubmitUbahPassword(ChangesPassword model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));

                TblUser data = _context.TblUser.Where(m => m.PegawaiId == PegawaiId).FirstOrDefault(); // Ambil data sesuai dengan ID
                if (model.PasswordLama != data.Password)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Login:UpdatePasswordSalah"]);

                }

                if (model.ConfirmPasswordBaru != model.PasswordBaru)
                {
                    return Content(GetConfig.AppSetting["AppSettings:Login:SalahConfirmPasssword"]);

                }

                data.Password = model.PasswordBaru;
                data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                data.UpdatedTime = DateTime.Now;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }


        [HttpPost]
        public ActionResult SubmitUploadFoto(UploadFile model)
        {
            try
            {
                if (!lastSession.Update())
                {
                    return RedirectToAction("Login", "Login", new { a = true });
                }
                int PegawaiId = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                TblSystemParameter ConfigAppsLocalPath = _context.TblSystemParameter.Where(m=>m.Key == "LocalPathProfile").FirstOrDefault();
                TblSystemParameter ConfigAppsTypeImages = _context.TblSystemParameter.Where(m => m.Key == "TypeFileUploadOnlyImages").FirstOrDefault();
                TblSystemParameter ConfigAppsMaxFile = _context.TblSystemParameter.Where(m => m.Key == "MaxFileSizeProfile").FirstOrDefault();

                TblPegawai data = _context.TblPegawai.Where(m => m.Id == PegawaiId).FirstOrDefault(); // Ambil data sesuai dengan ID
                if (model.file != null)
                {
                    TblProjectFile FileAttachment = new TblProjectFile();
                    var File = model.file;
                    string AllowedFileUploadType = ConfigAppsTypeImages.Value;
                    decimal SizeFile = File.Length / 1000000;

                    string Ext = Path.GetExtension(File.FileName);

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

                    var fileNameReplaceSpace = File.FileName.Replace(" ", "_");
                    var path = Path.Combine(PathFolder, fileNameReplaceSpace);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                    //data.NameImages = Path.GetFileNameWithoutExtension(fileNameReplaceSpace);
                    //data.Images = fileNameReplaceSpace;
                    //data.ImagesFullPath = path;
                    data.UpdatedById = int.Parse(HttpContext.Session.GetString(SessionConstan.Session_Pegawai_Id));
                    data.UpdatedDate = DateTime.Now;
                    _context.TblPegawai.Update(data);
                    _context.SaveChanges();

                }

                return Content("");
            }
            catch
            {
                return Content(GetConfig.AppSetting["AppSettings:SistemError"]);
            }
        }

    }
}
