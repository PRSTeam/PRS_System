﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.FormModel;

namespace PRS_System.Controllers
{
    public class FormPRSController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult form()
        {
            return View();
        }
        public IActionResult AddDataProcurement(CreateFormModel Procurement)
        {
            
            return View();
        }
        public IActionResult AddDataSuppies(CreateFormModel Suppies)
        {
            return View();
        }
        public IActionResult AddDataApprover(CreatedResult Approver)
        {
            return View();
        }
        public IActionResult Showlistuser()
        {
            return View();
        }
        public IActionResult Addnewuser()
        {
            return View();
        }
        public IActionResult Addnewuserdata()
        {

            return View();
        }
    }
}
