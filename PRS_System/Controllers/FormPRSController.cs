﻿using Microsoft.AspNetCore.Mvc;
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
                                      IFormService formService, IWebHostEnvironment hostingEnvironment, IAccountService accountService)
        {
            _logger = logger;
            _formService = formService;
            _hostingEnvironment = hostingEnvironment;
            _accountService = accountService;
        }
        public IActionResult Index(IndexListFormModel indexmodel)
        {
            indexmodel.category_user = HttpContext.Session.GetString("type_person").ToString();
            string user_id = HttpContext.Session.GetString("uid").ToString();
            indexmodel.ListForm = _formService.GetnamePRS(user_id);
            if (indexmodel.category_user == "Admin")
            {
                indexmodel.ListSuppies = _formService.GetListSuppies();
            }
            UserDataModel magnement_type = _accountService.CheckPositionManegment(indexmodel.category_user);
            if(magnement_type.Manage_Pos!="")
            {
                indexmodel.ListApproval = _formService.GetListApproval(user_id);
            }
            
            return View(indexmodel);

        }
        public IActionResult form(int id_tor, FormPRSModel Createview)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                // เพิ่มโค้ด
                if (id_tor == 0)
                {
                    List<ProductDataModel> productdata = new List<ProductDataModel>();
                    //List<SubjectDataModel> subjectdata = new List<SubjectDataModel>();
                    productdata.Add(new ProductDataModel
                    {
                        Id_Product = 0
                        ,
                        AmtProduct = 0
                        ,
                        Unit = ""
                        ,
                        NameProduct = ""
                        ,
                        Price_Per_Piece = 0
                        ,
                        status = "open"
                    });
                    Createview.Productdata = productdata;
                    //subjectdata.Add(new SubjectDataModel
                    //{
                    //    Id_Subject = 0
                    //    ,
                    //    Subject = ""
                    //    ,
                    //    status="Open"
                    //}) ;
                    //Createview.Subjectdata = subjectdata;
                    Createview.diractor_2 = "";
                    Createview.diractor_3 = "";

                }
                //----Edit Form Page
                else if (id_tor != 0)
                {
                    double sumvalue = 0;
                    double vaxvalue = 0;
                    Createview = _formService.GetValuesFormPRS(id_tor);

                    Createview.Productdata = _formService.GetValuesFormPRSProduct(id_tor);
                    for (int i = 0; i < Createview.Productdata.Count; i++)
                    {
                        vaxvalue = (0.07) * (sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece));
                        sumvalue = sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece);
                    }
                    Createview.vaxproduct = Math.Round(vaxvalue, 2);
                    Createview.sumproduct = Math.Round(sumvalue + vaxvalue, 2);
                    Createview.Subjectdata = _formService.GetValuesFormPRSSubject(id_tor);
                    
                    if(Createview.status == "Sent to Approval")
                    {
                        FormPRSModel order_diract = new FormPRSModel();
                        order_diract = _formService.Get_PRS_ORDER_DIRACT(id_tor);
                        Createview.id_order = order_diract.id_order;
                        Createview.name_select1 = order_diract.name_select1;
                        Createview.name_select2 = order_diract.name_select2;
                        Createview.definition = order_diract.definition;
                        Createview.last_approval = _formService.GetLastApproval(id_tor);
                        if (Createview.last_approval == "")
                        {
                            Createview.last_approval = order_diract.name_select1;
                        }
                        FormPRSModel.CommentDataModel com_ment = new FormPRSModel.CommentDataModel();
                        if (Createview.definition == "ปกติ")
                        {
                            string id_com = "";
                            string email = "";
                            Createview.Email_approval6 = _accountService.GetUserEmail(Createview.name_select1);
                            id_com = _accountService.GetUserid(Createview.name_select1);
                            Createview.Email_approval = _accountService.GetUserEmail(Createview.name_select2); // เรียก email ลำดับต่อจากคนปัจจุบัน
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval = com_ment.comment;

                            id_com = _accountService.GetUserid(Createview.name_select2);
                            Createview.Email_approval2 = _accountService.GetUserEmail("นักวิเคราะห์นโยบายและแผนชำนาญการ"); // เรียก email ลำดับต่อจากคนปัจจุบัน
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval2 = com_ment.comment;

                            id_com = _accountService.GetUserid("นักวิเคราะห์นโยบายและแผนชำนาญการ");
                            Createview.Email_approval3 = _accountService.GetUserEmail("หัวหน้าสำนักงานเลขานุการ"); // เรียก email ลำดับต่อจากคนปัจจุบัน
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval3 = com_ment.comment;

                            id_com = _accountService.GetUserid("หัวหน้าสำนักงานเลขานุการ");
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval4 = com_ment.comment;
                        }
                        else if (Createview.definition == "พิเศษ")
                        {
                            string id_com = "";
                            id_com = _accountService.GetUserid(Createview.name_select1);
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval = com_ment.comment;

                            id_com = _accountService.GetUserid(Createview.name_select2);
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval2 = com_ment.comment;

                            id_com = _accountService.GetUserid("นักวิเคราะห์นโยบายและแผนการชำนาญการ");
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval3 = com_ment.comment;

                            id_com = _accountService.GetUserid("กรรมการเลขานุการฯ");
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval4 = com_ment.comment;

                            id_com = _accountService.GetUserid("ประธานกรรมการดำเนินการฯ");
                            com_ment = _formService.GetCommentApproval(id_tor, id_com);
                            Createview.des_approval5 = com_ment.comment;
                        }
                    }
                }
                Createview.ListEmailAdmin = _accountService.GetAllAdminEmail();
                string listemailadmin = "";
                for (int i = 0; i < Createview.ListEmailAdmin.Count; i++)
                {
                    listemailadmin = Createview.ListEmailAdmin[i].Email + "/" + listemailadmin;

                }
                if (listemailadmin[listemailadmin.Length - 1] == '/')
                {

                    Createview.stringlistemail_admin = listemailadmin.Substring(0, (listemailadmin.Length - 1));
                }
                Createview.ListEmailAdmin = _accountService.GetAllAdminEmail();
                for (int i = 0; i < Createview.ListEmailAdmin.Count; i++)
                {
                    listemailadmin = Createview.ListEmailAdmin[i].Email+"/"+ listemailadmin;

                }
                if (listemailadmin[listemailadmin.Length - 1] == '/')
                {
                    
                    Createview.stringlistemail_admin = listemailadmin.Substring(0, (listemailadmin.Length - 1));
                }
                
                Console.WriteLine("Check" + id_tor);
                Createview.login_userid = HttpContext.Session.GetString("uid").ToString();
                UserDataModel user_login = _accountService.CheckLogin(Createview.login_userid);
                Createview.Email_Proquement = user_login.Email;
                Createview.type_user_magnement = HttpContext.Session.GetString("Manage_Pos").ToString();
                bool check = (Createview.last_approval == "หัวหน้าภาควิชา" || Createview.last_approval == "หัวหน้าฝ่ายสนับสนุนงานกลาง" || Createview.last_approval == "หัวหน้าฝ่ายบริการและพัฒนาคุณภาพการศึกษา") && (Createview.type_user_magnement == "หัวหน้าภาควิชา" || Createview.type_user_magnement == "หัวหน้าฝ่ายสนับสนุนงานกลาง" || Createview.type_user_magnement == "หัวหน้าฝ่ายบริการและพัฒนาคุณภาพการศึกษา");
                Console.WriteLine(check);
                Createview.category_user= HttpContext.Session.GetString("type_person").ToString();
                //Createview.FilePath = _accountService.GetSignature(Createview.login_userid);
                Createview.id_tor = id_tor;
                return View(Createview);
            }
            else
            {
                //เก็บ Temp ไว้ใช้สำหรับเช็คค่าตอน Login ว่ามาจากการกดลิงค์ใน Email
                TempData["ApproverData"] = id_tor;
                return RedirectToAction("Index", "Login");
            }
            //----Add Form Page

        }
        [HttpPost]
        public async Task<IActionResult> AddDataProcurement(FormPRSModel Procurement)
        {
            try
            {
                Console.WriteLine("Check" + Procurement.id_tor);
                //------Add New form to database
                if (Procurement.id_tor == 0)
                {
                    string uniquefile = null;
                    string filepath = null;
                    if (Procurement.FilePDF != null)
                    {
                        string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "File\\FileDocUser");
                        string filename = Procurement.FilePDF.FileName;
                        string[] fileextension = filename.Split(".");
                        uniquefile = fileextension[0] + "__" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileextension[1];/* + "_" + editdata.ImagePicture.FileName*/
                        filepath = Path.Combine(uploadfile, uniquefile);
                        Procurement.FilePDF.CopyTo(new FileStream(filepath, FileMode.Create));
                        Procurement.FilePath = uniquefile;
                    }
                    Procurement.User_ID = HttpContext.Session.GetString("uid").ToString();
                    Console.WriteLine("Check");
                    //----แอดข้อมูลFormข้อมูลที่ไม่ได้เป้นลิสต์
                    _formService.AddFormDetailData(Procurement.FormDataDetail());
                    Procurement.id_tor = _formService.GetMaximumID_TOR();
                    //--------------------------------
                    _formService.AddProductData(Procurement.Productdata, Procurement.id_tor);
                    if(Procurement.Subjectdata!=null)
                    {
                        _formService.AddSubjectData(Procurement.Subjectdata, Procurement.id_tor);
                    }
                    if (Procurement.supportType != null)
                    {
                        if (Procurement.supportType == "ได้รับสนับสนุนจาก")
                        {
                            Procurement.desc_assist3 = Procurement.desc_assist1;
                        }
                        else if (Procurement.supportType == "ไม่ได้รับแหล่งเงินทุนสนับสนุนใดๆ")
                        {
                            Procurement.desc_assist3 = null;
                        }
                        else if (Procurement.supportType == "เหตุผลอื่นๆ")
                        {
                            Procurement.desc_assist3 = Procurement.desc_assist2;
                        }
                    }
                    else if(Procurement.type_assitst != "เพื่อใช้ในการสนับสนุนรายวิชา")
                    {
                        _formService.AddAssist_TOR(Procurement);
                    }
                    
                    


                }
                //----Edit Form to Database
                else if (Procurement.id_tor != 0)
                {
                    string uniquefile = null;
                    string filepath = null;
                    if (Procurement.FilePDF != null)
                    {
                        string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "File\\FileDocUser");
                        string filename = Procurement.FilePDF.FileName;
                        string[] fileextension = filename.Split(".");
                        uniquefile = fileextension[0] + "__" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileextension[1];/* + "_" + editdata.ImagePicture.FileName*/
                        filepath = Path.Combine(uploadfile, uniquefile);
                        Procurement.FilePDF.CopyTo(new FileStream(filepath, FileMode.Create));
                        Procurement.FilePath = uniquefile;
                    }
                    Procurement.User_ID = HttpContext.Session.GetString("uid").ToString();
                    _formService.EditFormDetailData(Procurement.FormDataDetail(), Procurement.id_tor);
                    if(Procurement.type_assitst=="")
                    if (Procurement.IndexProDelete != null)
                    {
                        // Delete Product กับ Subject ที่ต้องการจะลบ
                        string[] listdelete_idProduct = Procurement.IndexProDelete.Split(",");
                        for (int i = 0; i < listdelete_idProduct.Length; i++)
                        {
                            int id_productdelete = int.Parse(listdelete_idProduct[i]);
                            if (id_productdelete != 0)
                            {
                                _formService.DeleteFormProductData(id_productdelete);
                            }

                        }
                    }
                    if (Procurement.IndexSubdelete != null)
                    {
                        string[] listdelete_idSubject = Procurement.IndexSubdelete.Split(",");
                        for (int i = 0; i < listdelete_idSubject.Length; i++)
                        {
                            int id_productdelete = int.Parse(listdelete_idSubject[i]);
                            if (id_productdelete != 0)
                            {
                                _formService.DeleteFormSubjectData(id_productdelete);
                            }

                        }
                    }

                    //-------Edit and Add New Data----------------------------------
                    for (int i = 0; i < Procurement.Productdata.Count; i++)
                    {
                        // Edit Data Product 
                        if (Procurement.Productdata[i].Id_Product != 0)
                        {
                            _formService.EditFormProductlData(Procurement.Productdata[i]);
                        }
                        // Add New Data Product
                        else if (Procurement.Productdata[i].Id_Product == 0)
                        {
                            _formService.UpdateAddProductData(Procurement.Productdata[i], Procurement.id_tor);
                        }
                    }
                    for (int i = 0; i < Procurement.Subjectdata.Count; i++)
                    {
                        if (Procurement.Subjectdata[i].Id_Subject != 0)
                        {
                            _formService.EditFormSubjectData(Procurement.Subjectdata[i]);
                        }
                        else if (Procurement.Subjectdata[i].Id_Subject == 0)
                        {
                            _formService.UpdateAddSubjectData(Procurement.Subjectdata[i], Procurement.id_tor);
                        }
                    }
                }
                return Json(new { status = "success", Messege = (Procurement.id_tor == 0 ? "Add" : "Update") + "Complete" });
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
            catch (Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new performance testing" });
            }
        }

        public IActionResult AddDataSuppies(FormPRSModel Suppies)
        {
            try
            {
                Suppies.category_user = HttpContext.Session.GetString("type_person").ToString();
                if (Suppies.buttonstatus_2 == "Sent to Approval")
                {
                    FormPRSModel check_order_data = _formService.Get_PRS_ORDER_DIRACT(Suppies.id_tor);
                    //checkว่าค้นหาข้อมูลจัดซื้อว่ามีหรือไม่ ถ้าไม่มีให้เพิ่ม ถ้ามีให้แก้ไข
                    if (check_order_data.id_order != 0)
                    {
                        _formService.updatestatusform(Suppies.buttonstatus_2, Suppies.id_tor, Suppies.category_user);
                    }
                    else if (check_order_data.id_order == 0)
                    {
                        _formService.AddDataSupplies(Suppies, Suppies.id_tor);
                        _formService.updatestatusform(Suppies.buttonstatus_2, Suppies.id_tor, Suppies.category_user);
                        
                    }


                }
                else if (Suppies.buttonstatus_2 == "Return to Requester")
                {
                    
                    _formService.updatestatusform(Suppies.buttonstatus_2, Suppies.id_tor,Suppies.User_ID);
                   
                }
                return Json(new { status = "success", Messege = Suppies.buttonstatus_2 + " Complete" });

            }
            catch (Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new performance testing" });
            }



        }

        public IActionResult AddDataApprover(FormPRSModel Approver)
        {
            Approver.login_userid = HttpContext.Session.GetString("uid").ToString();
            Approver.category_user = HttpContext.Session.GetString("type_person").ToString();
            var checkcomment = _formService.GetCommentApproval(Approver.id_tor, Approver.login_userid);
            if (checkcomment.comment == null)
            {
                if (Approver.des_approval != null)
                {
                    Approver.des_approval0 = Approver.des_approval;
                }
                else if (Approver.des_approval2 != null)
                {
                    Approver.des_approval0 = Approver.des_approval2;
                }
                else if (Approver.des_approval3 != null)
                {
                    Approver.des_approval0 = Approver.des_approval3;
                }
                else if (Approver.des_approval4 != null)
                {
                    Approver.des_approval0 = Approver.des_approval4;
                }
                else if (Approver.des_approval5 != null)
                {
                    Approver.des_approval0 = Approver.des_approval5;
                }

                _formService.AddCommentApproval(Approver);

            }
            else if (checkcomment != null)
            {
                if (Approver.des_approval != null)
                {
                    Approver.des_approval0 = Approver.des_approval;
                }
                else if (Approver.des_approval2 != null)
                {
                    Approver.des_approval0 = Approver.des_approval2;
                }
                else if (Approver.des_approval3 != null)
                {
                    Approver.des_approval0 = Approver.des_approval3;
                }
                else if (Approver.des_approval4 != null)
                {
                    Approver.des_approval0 = Approver.des_approval4;
                }
                else if (Approver.des_approval5 != null)
                {
                    Approver.des_approval0 = Approver.des_approval5;
                }
                _formService.EditCommentApproval(Approver);
            }
            if (Approver.buttonstatus_3 == "Sent to Approval" || Approver.buttonstatus_3 == "Approved")
            {
                _formService.updatestatusform(Approver.buttonstatus_3, Approver.id_tor, Approver.category_user);
            }
            else if (Approver.buttonstatus_3 == "Return to Requester")
            {
                _formService.updatestatusform(Approver.buttonstatus_3, Approver.id_tor, Approver.User_ID);
            }

            return Json(new { status = "success", Messege = (checkcomment == null ? "Add Approval" : "Update Approval") + "Complete" }); ;
        }
        public IActionResult productpdf(int id_tor)
        {
            return View();
        }
        public IActionResult torpdf(int id_tor)
        {
            FormPRSModel Createview = new FormPRSModel();
            if (id_tor != 0)
            {
                double sumvalue = 0;
                double vaxvalue = 0;
                Createview = _formService.GetValuesFormPRS(id_tor);

                Createview.Productdata = _formService.GetValuesFormPRSProduct(id_tor);
                for (int i = 0; i < Createview.Productdata.Count; i++)
                {
                    vaxvalue = (0.07) * (sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece));
                    sumvalue = sumvalue + (Createview.Productdata[i].AmtProduct * Createview.Productdata[i].Price_Per_Piece);
                }
                Createview.vaxproduct = Math.Round(vaxvalue, 2);
                Createview.sumproduct = Math.Round(sumvalue + vaxvalue, 2);
                Createview.Subjectdata = _formService.GetValuesFormPRSSubject(id_tor);

                if (Createview.status == "Sent to Approval")
                {
                    FormPRSModel order_diract = new FormPRSModel();
                    order_diract = _formService.Get_PRS_ORDER_DIRACT(id_tor);
                    Createview.id_order = order_diract.id_order;
                    Createview.name_select1 = order_diract.name_select1;
                    Createview.name_select2 = order_diract.name_select2;
                    Createview.definition = order_diract.definition;
                    Createview.last_approval = _formService.GetLastApproval(id_tor);
                    if (Createview.last_approval == "")
                    {
                        Createview.last_approval = order_diract.name_select1;
                    }
                    FormPRSModel.CommentDataModel com_ment = new FormPRSModel.CommentDataModel();
                    UserDataModel userapproval = new UserDataModel();
                    if (Createview.definition == "ปกติ")
                    {
                        string id_com = "";
                        string email = "";

                        id_com = _accountService.GetUserid(Createview.name_select1);
                        Createview.Email_approval = _accountService.GetUserEmail(Createview.name_select2); // เรียก email ลำดับต่อจากคนปัจจุบัน
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval = com_ment.comment;
                        Createview.date_approval = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment(Createview.name_select1);
                        Createview.name_approval = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval = userapproval.ESignature;

                        id_com = _accountService.GetUserid(Createview.name_select2);
                        Createview.Email_approval2 = _accountService.GetUserEmail("นักวิเคราะห์นโยบายและแผนชำนาญการ"); // เรียก email ลำดับต่อจากคนปัจจุบัน
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval2 = com_ment.comment;
                        Createview.date_approval2 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment(Createview.name_select2);
                        Createview.name_approval2 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval2 = userapproval.ESignature;

                        id_com = _accountService.GetUserid("นักวิเคราะห์นโยบายและแผนชำนาญการ");
                        Createview.Email_approval3 = _accountService.GetUserEmail("หัวหน้าสำนักงานเลขานุการ"); // เรียก email ลำดับต่อจากคนปัจจุบัน
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval3 = com_ment.comment;
                        Createview.date_approval3 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment("นักวิเคราะห์นโยบายและแผนชำนาญการ");
                        Createview.name_approval3 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval3 = userapproval.ESignature;

                        id_com = _accountService.GetUserid("หัวหน้าสำนักงานเลขานุการ");
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval4 = com_ment.comment;
                        Createview.date_approval = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment("หัวหน้าสำนักงานเลขานุการ");
                        Createview.name_approval4 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval4 = userapproval.ESignature;
                    }
                    else if (Createview.definition == "พิเศษ")
                    {
                        string id_com = "";
                        id_com = _accountService.GetUserid(Createview.name_select1);
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval = com_ment.comment;
                        Createview.date_approval = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment(Createview.name_select1);
                        Createview.name_approval = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval = userapproval.ESignature;

                        id_com = _accountService.GetUserid(Createview.name_select2);
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval2 = com_ment.comment;
                        Createview.date_approval2 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment(Createview.name_select2);
                        Createview.name_approval2 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval2 = userapproval.ESignature;

                        id_com = _accountService.GetUserid("นักวิเคราะห์นโยบายและแผนการชำนาญการ");
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval3 = com_ment.comment;
                        Createview.date_approval3 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment("นักวิเคราะห์นโยบายและแผนการชำนาญการ");
                        Createview.name_approval3 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval3 = userapproval.ESignature;

                        id_com = _accountService.GetUserid("กรรมการเลขานุการฯ");
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval4 = com_ment.comment;
                        Createview.date_approval4 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment("กรรมการเลขานุการฯ");
                        Createview.name_approval4 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval4 = userapproval.ESignature;

                        id_com = _accountService.GetUserid("ประธานกรรมการดำเนินการฯ");
                        com_ment = _formService.GetCommentApproval(id_tor, id_com);
                        Createview.des_approval5 = com_ment.comment;
                        Createview.date_approval5 = com_ment.com_date;
                        userapproval = _accountService.CheckPositionManegment("ประธานกรรมการดำเนินการฯ");
                        Createview.name_approval5 = userapproval.Prefix_NameThai + userapproval.Full_NameThai;
                        Createview.esign_approval5 = userapproval.ESignature;
                    }
                }
            }
            //Createview.ListEmailAdmin = _accountService.GetAllAdminEmail();
            //string listemailadmin = "";
            //for (int i = 0; i < Createview.ListEmailAdmin.Count; i++)
            //{
            //    listemailadmin = Createview.ListEmailAdmin[i].Email + "/" + listemailadmin;

            //}
            //if (listemailadmin[listemailadmin.Length - 1] == '/')
            //{

            //    Createview.stringlistemail_admin = listemailadmin.Substring(0, (listemailadmin.Length - 1));
            //}

            //Console.WriteLine("Check" + id_tor);
            //Createview.login_userid = HttpContext.Session.GetString("uid").ToString();
            //UserDataModel user_login = _accountService.CheckLogin(Createview.login_userid);
            //Createview.Email_Proquement = user_login.Email;
            //Createview.type_user_magnement = HttpContext.Session.GetString("Manage_Pos").ToString();
            //bool check = (Createview.last_approval == "หัวหน้าภาควิชา" || Createview.last_approval == "หัวหน้าฝ่ายสนับสนุนงานกลาง" || Createview.last_approval == "หัวหน้าฝ่ายบริการและพัฒนาคุณภาพการศึกษา") && (Createview.type_user_magnement == "หัวหน้าภาควิชา" || Createview.type_user_magnement == "หัวหน้าฝ่ายสนับสนุนงานกลาง" || Createview.type_user_magnement == "หัวหน้าฝ่ายบริการและพัฒนาคุณภาพการศึกษา");
            //Console.WriteLine(check);
            //Createview.category_user = HttpContext.Session.GetString("type_person").ToString();
            ////Createview.FilePath = _accountService.GetSignature(Createview.login_userid);
            //Createview.id_tor = id_tor;
            return View(Createview);
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
        public IActionResult getEmail(FormPRSModel type_approval)
        {
            type_approval.Email_approval6 = _accountService.GetUserEmail(type_approval.name_select1);
            return Json(new { status = "success", email = type_approval.Email_approval6 }); ;
        }
    }
}
