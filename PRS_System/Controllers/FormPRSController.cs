using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.FormModel;
using PRS_System.Models.Data;
using Microsoft.Extensions.Logging;
using PRS_System.IServices;

namespace PRS_System.Controllers
{
    public class FormPRSController : Controller
    {
        private readonly ILogger<FormPRSController> _logger;
        private readonly IFormService _formService;
        public FormPRSController(ILogger<FormPRSController> logger,
                                      IFormService formService)
        {
            _logger = logger;
            _formService = formService;
        }
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
            //----แอดข้อมูลFormข้อมูลที่ไม่ได้เป้นลิสต์
            _formService.AddFormDetailData(Procurement.FormDataDetail());
            
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
