using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Setting;
using PRS_System.Models.Data;
using PRS_System.IServices;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PRS_System.Controllers
{
    
    public class AdminSettingController : Controller     
    {
        private readonly ILogger<AdminSettingController> _logger;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminSettingController(ILogger<AdminSettingController> logger, 
                                      IWebHostEnvironment hostEnvironment,
                                      IAccountService accountService)
        {
            _logger = logger;
            _hostingEnvironment = hostEnvironment;
            _accountService = accountService;
        }
        public IActionResult Addnewuser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Addnewuserdata(AddnewuserdataModel AddnewuserModel)
        {
           
            try
            {
                //-------สร้างรูปภาพ ลายเซ็น
                string filesig = AddnewuserModel.ESignature;
                string uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = AddnewuserModel.UserID + "_" + uniquefile + ".png";
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img\\signature", fileName); //image คือ พาทRoot ของ image โดยเซฟเป็นชื่อ filename
                System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(AddnewuserModel.ESignature.Replace("data:image/png;base64,", string.Empty)));
                //-------------------------
                //--------แอดข้อมูล User
                UserDataModel userdata = AddnewuserModel.ToAddnewuserdata(filePath);
                _accountService.AddNewUser(userdata);
                return Json(new { status = "success", Messege = "Add Complete" });
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new User" });
            }
           
        }
    }
}
