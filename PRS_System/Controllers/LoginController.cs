using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PRS_System.Models.Login;
using System;

namespace PRS_System.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ACCESS_TOKEN")))
            {
                return View();
            }
            else
            {
                var route = new FormPRSController().Index();
                return View(route);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel.Sendlogin data)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ACCESS_TOKEN")))
            {
                try
                {
                    byte[] encData_byte = new byte[data.Password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(data.Password);
                    string encodedData = Convert.ToBase64String(encData_byte);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in base64Encode" + ex.Message);
                }
                var route = new FormPRSController().Index();
                return View(route);
            }
            else
            {
                return View();
            }
        }

        public IActionResult KUoAuth2()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ACCESS_TOKEN")))
            {
                var AuthControl = new OAuth2TestTool.MVC.Controllers.HomeController();
                var Auth = AuthControl.Index("code", "state", false, false);
                return View(Auth);
            }
            else
            {
                var route = new FormPRSController().Index();
                return View(route);
            }
        }
    }
}
