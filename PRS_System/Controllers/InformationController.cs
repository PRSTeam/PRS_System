using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PRS_System.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            //if (HttpContext.Session.GetString("advisor-id") == null)
            //{
            //    ViewData["nav_manu"] = HttpContext.Session.GetString("advisor-id");
            //}
            //else if (HttpContext.Session.GetString("type-person") == "1" || HttpContext.Session.GetString("type-person") == "2")
            //{
            //    ViewData["nav_manu"] = HttpContext.Session.GetString("type-person").ToString();
            //}
            return View();
        }

        //public IActionResult GetSessionData(string txt)
        //{
        //    if (txt == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        HttpContext.Session.SetString("advisor-id", txt);
        //        HttpContext.Session.SetString("type-person", "1");
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}
