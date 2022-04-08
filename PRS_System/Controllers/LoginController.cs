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
                if ((data.Username == "Admin" && data.Password == "1234") || (!string.IsNullOrEmpty(data.Username) && !string.IsNullOrEmpty(data.Password)))
                {
                    try
                    {
                        var result_chk = _accountService.CheckLogin(data.Username);

                        HttpContext.Session.SetString("AccessToken", "1234567890");
                        HttpContext.Session.SetString("uid", result_chk.UserID);
                        HttpContext.Session.SetString("thaiprename", result_chk.Prefix_NameThai);
                        HttpContext.Session.SetString("thainame", result_chk.Full_NameThai);
                        HttpContext.Session.SetString("Operate_Pos", result_chk.Operate_Pos);
                        HttpContext.Session.SetString("Manage_Pos", result_chk.Manage_Pos);
                        //HttpContext.Session.SetString("position", result_chk.Operate_Pos + ", " + result_chk.Manage_Pos);
                        HttpContext.Session.SetString("ESignature", result_chk.ESignature);
                        HttpContext.Session.SetString("mail", result_chk.Email);
                        HttpContext.Session.SetString("type_person", result_chk.Category);

                        ////ไปหน้าอนุมัติ สำหรับ User ที่กดลิงค์ใน Email
                        //if (TempData["ApproverData"] != null)
                        //{
                        //    TempData.Keep("ApproverData");
                        //    return RedirectToAction("form", "FormPRS", new { id_tor = TempData["ApproverData"] });
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "FormPRS");
                        //}
                        if (TempData["ApproverData"] != null)
                        {
                            //TempData.Keep("ApproverData");
                            return Json(new { status = "success", temp = TempData["ApproverData"] });
                        }
                        else
                        {
                            return Json(new { status = "success" });
                        }

                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "error", detail = "กรุณาติดต่อเจ้าหน้าที่", errorMessage = "เกิดข้อผิดพลาด" });
                    }
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
                //ชื่อเต็มภาษาไทย
                HttpContext.Session.SetString("thainame", userinfoResponse.thainame); //มหาราช ทศศะ
                //ชื่อต้นภาษาไทย
                HttpContext.Session.SetString("first_name", userinfoResponse.first_name); //มหาราช
                //นามสกุลภาษาไทย
                HttpContext.Session.SetString("last_name", userinfoResponse.last_name); //ทศศะ
                //ชื่อเต็มภาษาอังกฤษ
                HttpContext.Session.SetString("cn", userinfoResponse.cn); //maharat tossa
                //ชื่อต้นภาษาอังกฤษ
                HttpContext.Session.SetString("givenname", userinfoResponse.givenname); //maharat
                //นามสกุลภาษาอังกฤษ
                HttpContext.Session.SetString("surname", userinfoResponse.surname); //tossa
                //คำนำหน้าชื่อ
                HttpContext.Session.SetString("thaiprename", userinfoResponse.thaiprename); //นาย
                //ลักษณะงานตามข้อมูลกองการเจ้าหน้าที่
                HttpContext.Session.SetString("jobtype", userinfoResponse.jobtype); //ข้าราชการ สาย ก.
                //ประเภทบุคคลตามข้อมูลกองการเจ้าหน้าที่
                HttpContext.Session.SetString("type_person", userinfoResponse.type_person); //ประเภทบุคคล: 1=teacher,2=staff,3=student ,4=alumni,5=guest,6=emailfac,7=kol,8=nondegree
                //ตำแหน่งตามข้อมูลกองการเจ้าหน้าที่
                HttpContext.Session.SetString("position", userinfoResponse.position); //ตำแหน่ง เช่น นักวิชาการคอมพิวเตอร์, อาจารย์
                //รหัสตำแหน่งตามข้อมูลกองการเจ้าหน้าที่
                HttpContext.Session.SetString("position_id", userinfoResponse.position_id); //รหัสตำแหน่ง: 001-004 = Teacher (!(001-004)) = Staff
                //วิทยาเขตสังกัด
                HttpContext.Session.SetString("campus", userinfoResponse.campus); //B=วิทยาเขตบางเขน, K = วิทยาเขตกำแพงแสน, C = วิทยาเขตสกลนคร, S = วิทยาเขตศรีราชา
                //ชื่อคณะสังกัด (เฉพาะบุคลากร)
                HttpContext.Session.SetString("faculty", userinfoResponse.faculty); //สำนักบริการคอมพิวเตอร์
                //รหัสคณะสังกัด (เฉพาะบุคลากร)
                HttpContext.Session.SetString("faculty_id", userinfoResponse.faculty_id); //B20
                //ชื่อภาควิชาสังกัด (เฉพาะบุคลากร)
                HttpContext.Session.SetString("department", userinfoResponse.department); //ฝ่ายระบบคอมพิวเตอร์และเครือข่าย
                //รหัสภาควิชาสังกัด (เฉพาะบุคลากร)
                HttpContext.Session.SetString("department_id", userinfoResponse.department_id); //B20xx
                //รหัสอาจารย์ (เฉพาะอาจารย์)
                HttpContext.Session.SetString("advisor_id", userinfoResponse.advisor_id); //รหัสอาจารย์ เช่น D4021
                //KU Mail รวม alias ทั้งหมด (เฉพาะบุคลากร)
                HttpContext.Session.SetString("mail", userinfoResponse.mail); //อีเมล cpcmrt@ku.ac.th, maharat.t@ku.ac.th
                //Google Mail
                HttpContext.Session.SetString("google_mail", userinfoResponse.google_mail); //อีเมล KU-Google เช่น maharat.t@ku.th
                //MS Live Mail
                HttpContext.Session.SetString("office365_mail", userinfoResponse.office365_mail); //อีเมล KU-Office เช่น maharat.t@live.ku.th
                //Login ID format ใหม่
                HttpContext.Session.SetString("userprincipalname", userinfoResponse.userprincipalname); //maharat.t
                //Login ID format เดิม (อายุชั่วคราว 3 ปี)
                HttpContext.Session.SetString("uid", userinfoResponse.uid); //cpcmrt

                //1=teacher, 2=staff
                if (HttpContext.Session.GetString("type_person") == "1" || HttpContext.Session.GetString("type_person") == "2")
                {
                    //ไปหน้าอนุมัติ สำหรับ User ที่กดลิงค์ใน Email
                    if (TempData["ApproverData"] != null)
                    {
                        TempData.Keep("ApproverData");
                        return RedirectToAction("form", "FormPRS", new {id_tor = TempData["ApproverData"] });
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
