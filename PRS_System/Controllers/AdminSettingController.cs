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
        public IActionResult Showlistuser(ShowListUserModel datauser)
        {
            
            //datauser.userdata = _accountService.GetDataUser(user_id);
            return View();
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
                
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Status))
                {
                    ModelState.AddModelError("Status", "กรุณากดเลือกสถานะผู้ใช้");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.UserID))
                {
                    ModelState.AddModelError("UserID", "กรุณากรอกUserID");
                }
                if(string.IsNullOrWhiteSpace(AddnewuserModel.User_Type))
                {
                    ModelState.AddModelError("User Type", "กรุณากรอกตำแหน่ง");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameThai))
                {
                    ModelState.AddModelError("Prefix NameThai", "กรุณากดเลือกคำนำหน้าไทย");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameEng))
                {
                    ModelState.AddModelError("Prefix NameEng", "กรุณากดเลือกคำนำหน้าภาษาอังกฤษ");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameThai))
                {
                    ModelState.AddModelError("Full NameThai", "กรุณากรอกชื่อจริง");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameEng))
                {
                    ModelState.AddModelError("Full NameEng", "กรุณากรอกชื่อจริงภาษาอังกฤษ");
                }
                if (string.IsNullOrWhiteSpace(AddnewuserModel.Category))
                {
                    ModelState.AddModelError("Category", "กรุณากรอกประเภทผู้ใช้");
                }
                

                if(ModelState.IsValid)
                {
                    //-------สร้างรูปภาพ ลายเซ็น
                    if(AddnewuserModel.ESignature !=null)
                    {
                        string filesig = AddnewuserModel.ESignature;
                        string uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string fileName = AddnewuserModel.UserID + "_" + uniquefile + ".png";
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img\\signature", fileName); //image คือ พาทRoot ของ image โดยเซฟเป็นชื่อ filename
                        System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(AddnewuserModel.ESignature.Replace("data:image/png;base64,", string.Empty)));
                        UserDataModel userdata = AddnewuserModel.ToAddnewuserdata(fileName);
                        _accountService.AddNewUser(userdata);
                    }
                   
                    ////-------------------------
                    ////--------แอดข้อมูล User
                    
                    return Json(new { status = "success", Messege = "Add Complete" });
                }
                else
                {
                    string errorList = string.Join("<br/> -------------- <br/> ", (from item in ModelState.Values
                                                             from error in item.Errors
                                                             select error.ErrorMessage).ToList());
                    return Json(new { status = "error", detail = errorList, errorMessage = "Add Fail" });
                }
               


            }
            catch(Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new User" });
            }
           
        }
        public IActionResult EditUser(string user_id)
        {
            EdituserdataModel datauser = new EdituserdataModel();
            datauser.userdata = _accountService.GetDataUser(user_id);
            return View(datauser);
        }
        public IActionResult EditUserdata()
        {
            return View();
        }
    }
}
