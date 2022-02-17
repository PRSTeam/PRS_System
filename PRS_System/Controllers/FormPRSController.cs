using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Controllers
{
    public class FormPRSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
