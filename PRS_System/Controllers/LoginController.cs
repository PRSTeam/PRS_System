using Microsoft.AspNetCore.Mvc;
using PRS_System.Models;
using OAuth2TestTool.MVC.Controllers;

namespace PRS_System.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public void Authen()
        {
           var o2 = new OAuth2TestTool.MVC.Controllers.HomeController();
           var kk = o2.Index("123456", "11111", false, false);
        }
    }
}
