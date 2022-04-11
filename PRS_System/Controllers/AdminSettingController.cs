using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Setting;
using PRS_System.Models.Data;
using PRS_System.IServices;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PRS_System.Models.Information;

namespace PRS_System.Controllers
{

    public class AdminSettingController : Controller
    {
        private readonly ILogger<AdminSettingController> _logger;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInformationService _informationService;
        public AdminSettingController(ILogger<AdminSettingController> logger,
                                      IWebHostEnvironment hostEnvironment,
                                      IAccountService accountService,
                                      IInformationService informationService)
        {
            _logger = logger;
            _hostingEnvironment = hostEnvironment;
            _accountService = accountService;
            _informationService = informationService;
        }
        public IActionResult Showlistuser(ShowListUserModel datauser)
        {
            if (string.IsNullOrWhiteSpace(datauser.Keyword))
            {
                datauser.UserID = "";
            }
            datauser.userdata = _accountService.GetDataUser(datauser.Keyword);
            //datauser.userdata = _accountService.GetDataUser(user_id);
            return View(datauser);
        }
        public IActionResult Searchlistuser(string user_id, ShowListUserModel datauser)
        {
            datauser.userdata = _accountService.GetDataUser(user_id);
            //datauser.userdata = _accountService.GetDataUser(user_id);
            return View(datauser);
        }
        public IActionResult Addnewuser()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Addnewuserdata(AddnewuserdataModel AddnewuserModel)
        {

            try
            {

                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Status))
                //{
                //    ModelState.AddModelError("Status", "กรุณากดเลือกสถานะผู้ใช้");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.UserID))
                //{
                //    ModelState.AddModelError("UserID", "กรุณากรอกUserID");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.User_))
                //{
                //    ModelState.AddModelError("User Type", "กรุณากรอกตำแหน่ง");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameThai))
                //{
                //    ModelState.AddModelError("Prefix NameThai", "กรุณากดเลือกคำนำหน้าไทย");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameEng))
                //{
                //    ModelState.AddModelError("Prefix NameEng", "กรุณากดเลือกคำนำหน้าภาษาอังกฤษ");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameThai))
                //{
                //    ModelState.AddModelError("Full NameThai", "กรุณากรอกชื่อจริง");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameEng))
                //{
                //    ModelState.AddModelError("Full NameEng", "กรุณากรอกชื่อจริงภาษาอังกฤษ");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Category))
                //{
                //    ModelState.AddModelError("Category", "กรุณากรอกประเภทผู้ใช้");
                //}

                string fileName = null;
                if (ModelState.IsValid)
                {
                    UserDataModel checkid = _accountService.CheckLogin(AddnewuserModel.UserID);
                    if (checkid.UserID!=null)
                    {
                        return Json(new { status = "error", detail = "มีID :" + checkid.UserID + " ในระบบอยู่แล้ว", errorMessage = "Edit Fail" });
                    }
                    UserDataModel checkposition = _accountService.CheckPositionManegment(AddnewuserModel.User_Type_Magnement);
                    if (checkposition.UserID != null && AddnewuserModel.UserID != checkposition.UserID)
                    {
                        return Json(new { status = "error", detail = "มีบุคคลที่มีตำแหน่ง " + AddnewuserModel.User_Type_Magnement + " นี้อยู่แล้ว", errorMessage = "Edit Fail" });

                    }
                    //-------สร้างรูปภาพ ลายเซ็น
                    if (AddnewuserModel.ESignature != null)
                    {
                        string filesig = AddnewuserModel.ESignature;
                        string uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss");
                         fileName = AddnewuserModel.UserID + "_" + uniquefile + ".png";
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img\\signature", fileName); //image คือ พาทRoot ของ image โดยเซฟเป็นชื่อ filename
                        System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(AddnewuserModel.ESignature.Replace("data:image/png;base64,", string.Empty)));
                        
                    }
                    
                    if (checkposition.Manage_Pos == null)
                    {
                        UserDataModel userdata = AddnewuserModel.ToAddnewuserdata(fileName);
                        _accountService.AddNewUser(userdata);

                    }
                    
                    

                    ////-------------------------
                    ////--------แอดข้อมูล User

                    return Json(new { status = "success", Messege = "Add Complete" });
                }
                else
                {
                    string errorList = string.Join("<br/> -------------- <br/> ", (from item in ModelState.Values
                                                                                   from error in item.Errors
                                                                                   select error.ErrorMessage).ToList());
                    return Json(new { status = "error", detail = errorList, errorMessage = "Add Fail" });
                }



            }
            catch (Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new User" });
            }

        }

        public IActionResult EditUser(string user_id)
        {
            EdituserdataModel datauser = new EdituserdataModel();
            datauser.userdata = _accountService.GetDataUser(user_id);
            datauser.UserID = datauser.userdata[0].UserID;
            datauser.Prefix_NameThai = datauser.userdata[0].Prefix_NameThai;
            datauser.Status = datauser.userdata[0].Status;
            datauser.User_Type_Operation= datauser.userdata[0].Operate_Pos;
            datauser.User_Type_Magnement = datauser.userdata[0].Manage_Pos;
            datauser.Full_NameThai = datauser.userdata[0].Full_NameThai;
            datauser.ESignature = datauser.userdata[0].ESignature;
            datauser.Email = datauser.userdata[0].Email;
            datauser.Category = datauser.userdata[0].Category;
            return View(datauser);
        }
        public IActionResult DeleteUser(string user_id)
        {
            try
            {
                _accountService.DeleteUser(user_id);
                return Json(new { status = "success", Messege = "Delete Complete" });
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while delete" });
            }
        }

        public async Task<IActionResult> EditUserdata(EdituserdataModel datamodel)
        {
            try
            {

                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Status))
                //{
                //    ModelState.AddModelError("Status", "กรุณากดเลือกสถานะผู้ใช้");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.UserID))
                //{
                //    ModelState.AddModelError("UserID", "กรุณากรอกUserID");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.User_))
                //{
                //    ModelState.AddModelError("User Type", "กรุณากรอกตำแหน่ง");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameThai))
                //{
                //    ModelState.AddModelError("Prefix NameThai", "กรุณากดเลือกคำนำหน้าไทย");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Prefix_NameEng))
                //{
                //    ModelState.AddModelError("Prefix NameEng", "กรุณากดเลือกคำนำหน้าภาษาอังกฤษ");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameThai))
                //{
                //    ModelState.AddModelError("Full NameThai", "กรุณากรอกชื่อจริง");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Full_NameEng))
                //{
                //    ModelState.AddModelError("Full NameEng", "กรุณากรอกชื่อจริงภาษาอังกฤษ");
                //}
                //if (string.IsNullOrWhiteSpace(AddnewuserModel.Category))
                //{
                //    ModelState.AddModelError("Category", "กรุณากรอกประเภทผู้ใช้");
                //}

                string fileName = null;
                if (ModelState.IsValid)
                {
                    
                    //-------สร้างรูปภาพ ลายเซ็น
                    if (datamodel.ESignature != null)
                    {
                        string filesig = datamodel.ESignature;
                        string uniquefile = DateTime.Now.ToString("yyyyMMddHHmmss");
                        fileName = datamodel.UserID + "_" + uniquefile + ".png";
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img\\signature", fileName); //image คือ พาทRoot ของ image โดยเซฟเป็นชื่อ filename
                        System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(datamodel.ESignature.Replace("data:image/png;base64,", string.Empty)));
                        
                        
                    }
                    string checksignature=_accountService.GetSignature(datamodel.UserID);
                    if(checksignature!=null)
                    {
                        fileName = checksignature;
                    }
                    UserDataModel userdata = datamodel.ToEdituserdata(fileName);
                    UserDataModel checkposition = _accountService.CheckPositionManegment(datamodel.User_Type_Magnement);
                    if (checkposition.Manage_Pos == null)
                    {
                        
                        
                    }
                    else if (checkposition.Manage_Pos != null && datamodel.UserID != checkposition.UserID && checkposition.UserID!=null)
                    {
                        return Json(new { status = "error", detail = "มีบุคคลที่มีตำแหน่ง " + datamodel.User_Type_Magnement + " นี้อยู่แล้ว", errorMessage = "Edit Fail" });
                        goto errorreturn;

                    }
                    _accountService.EditUser(userdata);
                    return Json(new { status = "success", Messege = "Edit Complete" });
                errorreturn:
                    Console.WriteLine("Error");

                }
                else
                {
                    string errorList = string.Join("<br/> -------------- <br/> ", (from item in ModelState.Values
                                                                                   from error in item.Errors
                                                                                   select error.ErrorMessage).ToList());
                    return Json(new { status = "error", detail = errorList, errorMessage = "Edit Fail" });
                }



            }
            catch (Exception ex)
            {
                return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while Edit User" });
            }
           
        }

        public IActionResult InformationSetting()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    ViewBag.item_pic = null;
                    string h = null;
                    string s = null;
                    int i = 0;
                    foreach (var item in _informationService.ShowInformation())
                    {
                        if (item.Header.ToString() == "รูปปก")
                        {
                            i++;
                            ViewBag.item_pic += "<tr><th>" + i + "</th>";
                            ViewBag.item_pic += "<td><img src = '../File/Information/" + item.FilePath.ToString() + "' alt = '" + item.FilePath.ToString() + "'></td>";
                            ViewBag.item_pic += "<td>" + item.Description.ToString() + "</td>";
                            ViewBag.item_pic += "<td>" + item.Date.ToString() + "</td>";
                            ViewBag.item_pic += "<td><span class='btn-action'>";
                            ViewBag.item_pic += "<button type='button' class='btn btn-delete' onclick='deletefile(\"" + item.FilePath.ToString() + "\")'> ลบ </button>";
                            ViewBag.item_pic += "</span></td></tr>";
                        }
                        else if (item.Header.ToString() == "เอกสารดาวน์โหลด")
                        {
                            if (s == null)
                            {
                                s += item.Section.ToString();
                            }
                            else
                            {
                                s += "," + item.Section.ToString();
                            }
                        }
                        else
                        {
                            if (h == null)
                            {
                                h += item.Header.ToString();
                            }
                            else
                            {
                                h += "," + item.Header.ToString();
                            }

                        }

                    }

                    for (int n = i; n < 5; n++)
                    {
                        n++;
                        ViewBag.item_pic += "<tr><th>" + n + "</th>";
                        ViewBag.item_pic += "<td>-</td>";
                        ViewBag.item_pic += "<td>-</td>";
                        ViewBag.item_pic += "<td>-</td>";
                        ViewBag.item_pic += "<td><span class='btn-action'>";
                        ViewBag.item_pic += "<button type='button' class='btn btn-upload' onclick='toggle()'> อัพโหลด </button>";
                        ViewBag.item_pic += "</span></td></tr>";
                    }

                    string[] str_header = h.Split(",");
                    IEnumerable<string> result_header = str_header.Distinct();
                    string[] data_header = result_header.ToArray();

                    string[] str_section = s.Split(",");
                    IEnumerable<string> result_section = str_section.Distinct();
                    string[] data_section = result_section.ToArray();

                    ViewBag.tab_header_topic = data_header;

                    var result_data = _informationService.ShowInformation();

                    for (int m = 0; m < data_header.Length; m++)
                    {
                        if (m == 0)
                        {
                            ViewBag.tab_header = "<li class='current-news'><a href ='#tab-news" + (m + 1) + "' class='btn_news active_news'>" + data_header[m].ToString() + "</a><input type='hidden' id='tabHeader' name='tabHeader' value='" + data_header[m].ToString() + "' /></li>";
                        }
                        else
                        {
                            ViewBag.tab_header += "<li><a href ='#tab-news" + (m + 1) + "' class='btn_news'>" + data_header[m].ToString() + "</a><input type='hidden' id='tabHeader' name='tabHeader' value='" + data_header[m].ToString() + "' ></li>";
                        }
                        ViewBag.tab_body += "<div id='tab-news" + (m + 1) + "' class='tab-content-news'>";
                        ViewBag.tab_body += "<div class='rename-tag'><div class='btn2 btn-delete-tab'><h3>เปลื่ยนชื่อแท็บ : " + data_header[m].ToString() + "</h3><button class='delete-tab' onclick='deletetab()'>ลบแท็บ</button></div>";
                        ViewBag.tab_body += "<div class='rename-con'><input class='rename-input' type='text' placeholder='เปลื่ยนชื่อแท็บ' id='rename' name='rename' /><button onclick='renametab()'>เปลี่ยนชื่อ</button></div></div>";
                        ViewBag.tab_body += "<div class='add-news'><div class='btn2 btn-add-tab'><h3>เพิ่มข่าว</h3><button class='add-tab fa-solid fa-plus' onclick='togglefile()'></button></div>";
                        ViewBag.tab_body += "<div class='add-news-field'><table><thead><tr><th> No </th><th style='width:60%'> คำอธิบาย </th><th style='width:15%'> วันที่ </th><th>  </th></tr></thead><tbody>";

                        int t = 0;
                        foreach (var desc in result_data)
                        {
                            if (desc.Header == data_header[m])
                            {
                                t++;
                                if (desc.FilePath == null && desc.Description == null)
                                {
                                    ViewBag.tab_body += "";
                                }
                                else
                                {
                                    ViewBag.tab_body += "<tr><th>" + t + "</th>";
                                    ViewBag.tab_body += "<td><a href='../File/Information/" + desc.FilePath.ToString() + "' target='_blank'>" + desc.Description.ToString() + "</a></td>";
                                    ViewBag.tab_body += "<td>" + desc.Date.ToString() + "</td>";
                                    ViewBag.tab_body += "<td><span class='btn-action'><input type='hidden' id='file_name' value='" + desc.FilePath.ToString() + "' /><button type='button' class='btn btn-delete' onclick='deletefile(\"" + desc.FilePath.ToString() + "\")'> ลบ </button></span></td></tr>";
                                }

                            }
                        }
                        ViewBag.tab_body += "</tbody></table></div></div></div>";
                    }

                    for (int n = 0; n < data_section.Length; n++)
                    {
                        if (n == 0)
                        {
                            ViewBag.section_header = "<li class='current-section'><a href ='#tab-section" + (n + 1) + "' class='btn_section active_section'>" + data_section[n].ToString() + "</a><input type='hidden' id='sectionHeader' name='sectionHeader' value='" + data_section[n].ToString() + "' /></li>";
                        }
                        else
                        {
                            ViewBag.section_header += "<li><a href ='#tab-section" + (n + 1) + "' class='btn_section'>" + data_section[n].ToString() + "</a><input type='hidden' id='sectionHeader' name='sectionHeader' value='" + data_section[n].ToString() + "' ></li>";
                        }
                        ViewBag.section_body += "<div id='tab-section" + (n + 1) + "' class='tab-content-section'>";
                        ViewBag.section_body += "<div class='rename-tag'><div class='btn2 btn-delete-tab'><h3>เปลื่ยนชื่อหัวข้อเอกสาร : " + data_section[n].ToString() + "</h3><button class='delete-tab' onclick='deletesec()'>ลบหัวข้อเอกสาร</button></div>";
                        ViewBag.section_body += "<div class='rename-con'><input class='rename-input' type='text' placeholder='เปลื่ยนชื่อหัวข้อเอกสาร' id='rename' name='rename' /><button onclick='renamesec()'>เปลี่ยนชื่อ</button></div></div>";
                        ViewBag.section_body += "<div class='add-news'><div class='btn2 btn-add-tab'><h3>เพิ่มเอกสาร</h3><button class='add-tab fa-solid fa-plus' onclick='togglesection()'></button></div>";
                        ViewBag.section_body += "<div class='add-news-field'><table><thead><tr><th> No </th><th style='width:60%'> คำอธิบาย </th><th style='width:15%'> วันที่ </th><th>  </th></tr></thead><tbody>";

                        int u = 0;
                        foreach (var desc in result_data)
                        {
                            if (desc.Header == "เอกสารดาวน์โหลด" && desc.Section == data_section[n])
                            {
                                u++;
                                if (desc.FilePath == null && desc.Description == null)
                                {
                                    ViewBag.section_body += "";
                                }
                                else
                                {
                                    ViewBag.section_body += "<tr><th>" + u + "</th>";
                                    ViewBag.section_body += "<td><a href='../File/Information/" + desc.FilePath.ToString() + "' target='_blank'>" + desc.Description.ToString() + "</a></td>";
                                    ViewBag.section_body += "<td>" + desc.Date.ToString() + "</td>";
                                    ViewBag.section_body += "<td><span class='btn-action'><input type='hidden' id='file_name' value='" + desc.FilePath.ToString() + "' /><button type='button' class='btn btn-delete' onclick='deletefile(\"" + desc.FilePath.ToString() + "\")'> ลบ </button></span></td></tr>";
                                }

                            }
                        }
                        ViewBag.section_body += "</tbody></table></div></div></div>";
                    }

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InformationSetting(InfomationModel infor_data)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    try
                    {
                        string uniquefile = null;
                        string filepath = null;
                        FileStream fs = null;

                        if (infor_data.FilePic != null)
                        {
                            string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "File\\information");
                            string filename = infor_data.FilePic.FileName;
                            string[] fileextension = filename.Split(".");
                            //uniquefile = filename + "." + fileextension[1];
                            filepath = Path.Combine(uploadfile, filename);
                            fs = new FileStream(filepath, FileMode.Create);
                            infor_data.FilePic.CopyTo(fs);
                            infor_data.FilePath = filename;
                            if (infor_data.Description == null)
                            {
                                infor_data.Description = filepath;
                            }
                        }
                        else if (infor_data.FilePDF != null)
                        {
                            string uploadfile = Path.Combine(_hostingEnvironment.WebRootPath, "File\\information");
                            string filename = infor_data.FilePDF.FileName;
                            string[] fileextension = filename.Split(".");
                            //uniquefile = filename + "." + fileextension[1];
                            filepath = Path.Combine(uploadfile, filename);
                            fs = new FileStream(filepath, FileMode.Create);
                            infor_data.FilePDF.CopyTo(fs);
                            infor_data.FilePath = filename;
                        }
                        else
                        {
                            return Json(new { status = "error", detail = "Error", errorMessage = "Have a problem while adding new performance testing" });
                        }

                        fs.Close();
                        Console.WriteLine("Check");
                        _informationService.AddNewsDetailData(infor_data.ToAddNews());

                        return RedirectToAction("InformationSetting", "AdminSetting");
                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "error", detail = ex.ToString(), errorMessage = "Have a problem while adding new performance testing" });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteData(string filename)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    try
                    {
                        //ลบไฟล์
                        var rootFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "File\\information");
                        string[] fileList = Directory.GetFiles(rootFolderPath, filename);
                        foreach (var file in fileList)
                        {
                            //System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                            System.IO.File.Delete(file);
                            Console.WriteLine(file + "deleted");
                        }

                        _informationService.Del_data(filename);
                        return Json(new { status = "success" });
                    }
                    catch(IOException ex)
                    {
                        return Json(new { status = "error", detail = ex, errorMessage = "Fail" });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult addTab(string tabname, string secname)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    _informationService.Add_tab(tabname, secname);
                    return Json(new { status = "success" });
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult deleteTab(string tabname, string secname)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    var rootFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "File\\information");
                    var filename = _informationService.ShowInformation();
                    string[] fileList = null;
                    foreach (var item in filename)
                    {
                        if ((item.FilePath != null && item.Header == tabname) || (item.FilePath != null && item.Header == tabname && item.Section == secname))
                        {
                            fileList = System.IO.Directory.GetFiles(rootFolderPath, item.FilePath);
                            foreach (var file in fileList)
                            {
                                System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                                System.IO.File.Delete(file);
                            }
                        }
                    }

                    //string[] fileList = System.IO.Directory.GetFiles(rootFolderPath,);
                    //foreach (var file in fileList)
                    //{
                    //    System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                    //    System.IO.File.Delete(file);
                    //}

                    _informationService.Del_tab(tabname, secname);
                    return Json(new { status = "success" });
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult renameTab(string old_tabname, string new_tabname, string header)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
            {
                if (HttpContext.Session.GetString("type_person") == "Admin")
                {
                    _informationService.Rename_tab(old_tabname, new_tabname, header);
                    return Json(new { status = "success" });
                }
                else
                {
                    return RedirectToAction("Index", "formPRS");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
