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
using Microsoft.AspNetCore.Http;

namespace PRS_System.Controllers
{
    public class FormPRSController : Controller
    {
        private readonly ILogger<FormPRSController> _logger;
        private readonly IFormService _formService;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FormPRSController(ILogger<FormPRSController> logger,
                                      IFormService formService, IWebHostEnvironment hostingEnvironment,IAccountService accountService)
        {
            _logger = logger;
            _formService = formService;
            _hostingEnvironment = hostingEnvironment;
            _accountService = accountService;
        }
        public IActionResult Index(IndexListFormModel indexmodel)
        {
            string user_id = HttpContext.Session.GetString("USER_ID").ToString();
            indexmodel.ListForm= _formService.GetnamePRS(user_id);
            return View(indexmodel);

        }
        public IActionResult form(int id_tor,FormPRSModel Createview)
        {
            //----Add Form Page
            if(id_tor==0)
            {

            }
            //----Edit Form Page
            else if(id_tor!=0)
            {
                double sumvalue = 0;
                double vaxvalue = 0;
                Createview = _formService.GetValuesFormPRS(id_tor);
                Createview.Productdata = _formService.GetValuesFormPRSProduct(id_tor);
                for(int i=0;i<Createview.Productdata.Count;i++)
                {
                    vaxvalue = (0.07) * (sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece));
                    sumvalue = sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece);
                }
                Createview.vaxproduct = Math.Round(vaxvalue,2);
                Createview.sumproduct = Math.Round(sumvalue +vaxvalue,2);
                Createview.Subjectdata = _formService.GetValuesFormPRSSubject(id_tor);
            }
            Console.WriteLine("Check" + id_tor);
            string user_id= HttpContext.Session.GetString("USER_ID").ToString();       
            Createview.FilePath = _accountService.GetSignature(user_id);
            Createview.id_tor = id_tor;
            return View(Createview);
        }
        [HttpPost]
        public async Task<IActionResult>  AddDataProcurement(FormPRSModel Procurement)
        {
            try
            {
                Console.WriteLine("Check"+ Procurement.id_tor);
                //------Add New form to database
                if (Procurement.id_tor == 0)
                {
                    string uniquefile = null;
                    string filepath = null;
                    if (Procurement.FilePDF != null)
                    {
                        string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "File\\ListFormUser");
                        string filename = Procurement.FilePDF.FileName;
                        string[] fileextension = filename.Split(".");
                        uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileextension[1];/* + "_" + editdata.ImagePicture.FileName*/
                        filepath = Path.Combine(uploadfile, uniquefile);
                        Procurement.FilePDF.CopyTo(new FileStream(filepath, FileMode.Create));
                        Procurement.FilePath = uniquefile;
                    }
                    Procurement.User_ID = HttpContext.Session.GetString("USER_ID").ToString();
                    Console.WriteLine("Check");
                    //----แอดข้อมูลFormข้อมูลที่ไม่ได้เป้นลิสต์
                    _formService.AddFormDetailData(Procurement.FormDataDetail());
                    Procurement.id_tor = _formService.GetMaximumID_TOR();
                    //--------------------------------
                    _formService.AddProductData(Procurement.Productdata, Procurement.id_tor);
                    _formService.AddSubjectData(Procurement.Subjectdata, Procurement.id_tor);

                    
                }
                //----Edit Form to Database
                else if(Procurement.id_tor != 0)
                {

                }
                return Json(new { status = "success", Messege = "Add Complete" });
                //if (ModelState.IsValid)
                //{

                //}
                //else
                //{
                //    string errorList = string.Join("<br/>", (from item in ModelState.Values
                //                                             from error in item.Errors
                //                                             select error.ErrorMessage).ToList());
                //    return Json(new { status = "error", detail = errorList, errorMessage = "Add fail" });
                //}

            }
            catch(Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new performance testing" });
            }
           

            

           
        }
        public IActionResult AddDataSuppies(FormPRSModel Suppies)
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
