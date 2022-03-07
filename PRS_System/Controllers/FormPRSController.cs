using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.FormModel;
using PRS_System.Models.Data;
using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace PRS_System.Controllers
{
    public class FormPRSController : Controller
    {
        private readonly ILogger<FormPRSController> _logger;
        private readonly IFormService _formService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FormPRSController(ILogger<FormPRSController> logger,
                                      IFormService formService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _formService = formService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult form()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  AddDataProcurement(CreateFormModel Procurement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniquefile = null;
                    string filepath = null;
                    if (Procurement.FilePDF != null)
                    {
                        string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "image");
                        string filename = Procurement.FilePDF.FileName;
                        string[] fileextension = filename.Split(".");
                        uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileextension[1];/* + "_" + editdata.ImagePicture.FileName*/
                        filepath = Path.Combine(uploadfile, uniquefile);
                        Procurement.FilePDF.CopyTo(new FileStream(filepath, FileMode.Create));
                        Procurement.FilePath = uniquefile;

                    }
                    Console.WriteLine("Check");
                    //----แอดข้อมูลFormข้อมูลที่ไม่ได้เป้นลิสต์
                    _formService.AddFormDetailData(Procurement.FormDataDetail());
                    int id_tor = _formService.GetMaximumID_TOR();
                    //--------------------------------
                    _formService.AddProductData(Procurement.Productdata, id_tor);
                    _formService.AddSubjectData(Procurement.Subjectdata, id_tor);

                    return Json(new { status = "success", Messege = "Add Complete" });
                }
                else
                {
                    string errorList = string.Join("<br/>", (from item in ModelState.Values
                                                             from error in item.Errors
                                                             select error.ErrorMessage).ToList());
                    return Json(new { status = "error", detail = errorList, errorMessage = "Add fail" });
                }
               
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new performance testing" });
            }
           

            

           
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
