using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PRS_System.Models.Login;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using PRS_System.IServices;
using Newtonsoft.Json;

namespace PRS_System.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LoginController(ILogger<LoginController> logger,
                                      IWebHostEnvironment hostEnvironment,
                                      IAccountService accountService,
                                      IInformationService informationService)
        {
            _logger = logger;
            _hostingEnvironment = hostEnvironment;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "FormPRS");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel.Sendlogin data)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (data.Username == "Admin" && data.Password == "1234")
                {
                    var result_chk = _accountService.CheckLogin(data.Username);

                    HttpContext.Session.SetString("AccessToken", "1234567890");
                    HttpContext.Session.SetString("uid", result_chk.UserID);
                    HttpContext.Session.SetString("thainame", result_chk.Full_NameThai);
                    HttpContext.Session.SetString("cn", result_chk.Full_NameEng);
                    HttpContext.Session.SetString("thaiprename", result_chk.Prefix_NameThai);
                    HttpContext.Session.SetString("ENG_NAME_FULL", result_chk.Prefix_NameEng);
                    HttpContext.Session.SetString("position", result_chk.User_Type);
                    HttpContext.Session.SetString("type_person", result_chk.Category);

                    return Json(new { status = "success" });
                    //return RedirectToActionPermanentPreserveMethod("Index", "FormPRS");
                }
                else
                {
                    return Json(new { status = "error", detail = "กรุณาลองใหม่อีกครั้ง", errorMessage = "ชื่อผู้ใช้งาน หรือรหัสผ่านไม่ถูกต้อง" });
                }

                //try
                //{
                //    byte[] encData_byte = new byte[data.Password.Length];
                //    encData_byte = System.Text.Encoding.UTF8.GetBytes(data.Password);
                //    string encodedData = Convert.ToBase64String(encData_byte);    
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Error in base64Encode" + ex.Message);
                //}
                //return RedirectToActionPermanentPreserveMethod("Index", "FormPRS");
            }
            else
            {
                if (HttpContext.Session.GetString("type_person") == "1" || HttpContext.Session.GetString("type_person") == "2")
                {
                    return RedirectToAction("Index", "FormPRS");
                }
                else
                {
                    return RedirectToAction("Index", "Login", Json(new { status = "error", detail = "กรุณาติดต่อเจ้าหน้าที่", errorMessage = "รหัสผู้ใช้งานของคุณไม่ได้รับสิทธื์เข้าใช้งาน" }));
                }
            }
        }

        public IActionResult KUoAuth2()
        {
            var AuthModel = new OAuth2TestTool.MVC.Models.OAuth2ViewModel();

            if (!string.IsNullOrEmpty(AuthModel.AccessToken))
            {
                HttpContext.Session.SetString("AccessToken", AuthModel.AccessToken);

                var userinfoResponse = JsonConvert.DeserializeObject<OAuth2TestTool.MVC.Models.UserInfoResponseModel>(AuthModel.UserInfo);
                HttpContext.Session.SetString("thainame", userinfoResponse.thainame);
                HttpContext.Session.SetString("first_name", userinfoResponse.first_name);
                HttpContext.Session.SetString("last_name", userinfoResponse.last_name);
                HttpContext.Session.SetString("cn", userinfoResponse.cn);
                HttpContext.Session.SetString("givenname", userinfoResponse.givenname);
                HttpContext.Session.SetString("surname", userinfoResponse.surname);
                HttpContext.Session.SetString("thaiprename", userinfoResponse.thaiprename);
                HttpContext.Session.SetString("jobtype", userinfoResponse.jobtype);
                HttpContext.Session.SetString("type_person", userinfoResponse.type_person);
                HttpContext.Session.SetString("position", userinfoResponse.position);
                HttpContext.Session.SetString("position_id", userinfoResponse.position_id);
                HttpContext.Session.SetString("campus", userinfoResponse.campus);
                HttpContext.Session.SetString("faculty", userinfoResponse.faculty);
                HttpContext.Session.SetString("faculty_id", userinfoResponse.faculty_id);
                HttpContext.Session.SetString("department", userinfoResponse.department);
                HttpContext.Session.SetString("department_id", userinfoResponse.department_id);
                HttpContext.Session.SetString("advisor_id", userinfoResponse.advisor_id);
                HttpContext.Session.SetString("mail", userinfoResponse.mail);
                HttpContext.Session.SetString("google_mail", userinfoResponse.google_mail);
                HttpContext.Session.SetString("office365_mail", userinfoResponse.office365_mail);
                HttpContext.Session.SetString("userprincipalname", userinfoResponse.userprincipalname);
                HttpContext.Session.SetString("uid", userinfoResponse.uid);

                //1=teacher, 2=staff
                if (HttpContext.Session.GetString("type_person") == "1" || HttpContext.Session.GetString("type_person") == "2")
                {
                    //ไปหน้าอนุมัติ สำหรับ User ที่กดลิงค์ใน Email
                    if (TempData["ApproverData"] != null)
                    {
                        //TempData.Keep("ApproverData");
                        return RedirectToAction("AddDataApprover", "FormPRS");
                    }
                    else
                    {
                        return RedirectToAction("Index", "FormPRS");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login", Json(new { status = "error", detail = "กรุณาติดต่อเจ้าหน้าที่", errorMessage = "รหัสผู้ใช้งานของคุณไม่ได้รับสิทธื์เข้าใช้งาน" }));
                }
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                var AuthControl = new OAuth2TestTool.MVC.Controllers.HomeController();
                AuthControl.Index("code", "state", false, false);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "FormPRS");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Information");
        }
    }
}
