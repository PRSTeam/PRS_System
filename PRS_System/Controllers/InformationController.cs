using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRS_System.IServices;
using PRS_System.Models.Information;
using System.Collections.Generic;
using System.Linq;

namespace PRS_System.Controllers
{
    public class InformationController : Controller
    {
        private List<InformationModel> _i;

        public InformationController(IInformationService i_info)
        {
            _i = i_info.ShowInformation();
        }

        public IActionResult Index()
        {
            string h = null;
            string[] str = null;
            int img_count = 0;
            foreach (InformationModel i in _i)
            {
                if (i.Header.ToString() != "รูปปก")
                {
                    if(h == null)
                    {
                        h += i.Header.ToString();
                    }
                    else
                    {
                        h += "," + i.Header.ToString();
                    }
                }
                else
                {
                    img_count = img_count + 1;
                }
            }
            ViewBag.count_img = 100/img_count;

            str = h.Split(",");
            IEnumerable<string> result = str.Distinct();

            string[] data = result.ToArray();

            ViewBag.tab_header_topic = data;
            ViewBag.css_tab_header = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    ViewBag.tab_header = "<div class='actives tab-desc'>";
                    ViewBag.tab_header += "<h3>" + data[i] +"</h3>";
                    ViewBag.tab_header += "</div>";

                    ViewBag.tab_body = "<div class='actives tab-desc'>";
                    foreach(var desc in _i)
                    {
                        if (desc.Header == data[i])
                        {
                            ViewBag.tab_body += "<p><a href='../File/Information/" + desc.Name + "'>";
                            ViewBag.tab_body += desc.Description + "</a></p>";
                        }
                    }
                    ViewBag.tab_body += "</div>";
                }
                else
                {
                    ViewBag.tab_header += "<div class='tab-desc'>";
                    ViewBag.tab_header += "<h3>" + data[i] + "</h3>";
                    ViewBag.tab_header += "</div>";

                    ViewBag.tab_body += "<div class='tab-desc'>";
                    foreach (var desc in _i)
                    {
                        if (desc.Header == data[i])
                        {
                            ViewBag.tab_body += "<p><a href='../File/Information/" + desc.Name + "'>";
                            ViewBag.tab_body += desc.Description + "</a></p>";
                        }
                    }
                    ViewBag.tab_body += "</div>";
                }
            }





            //if (HttpContext.Session.GetString("advisor-id") == null)
            //{
            //    ViewData["nav_manu"] = HttpContext.Session.GetString("advisor-id");
            //}
            //else if (HttpContext.Session.GetString("type-person") == "1" || HttpContext.Session.GetString("type-person") == "2")
            //{
            //    ViewData["nav_manu"] = HttpContext.Session.GetString("type-person").ToString();
            //}
            return View(_i);
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
