using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Information;
using System.Collections.Generic;
using System.Linq;

namespace PRS_System.Controllers
{
    public class InformationController : Controller
    {
        private readonly ILogger<InformationController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInformationService _informationService;

        public InformationController(ILogger<InformationController> logger,
                                      IWebHostEnvironment hostEnvironment,
                                      IInformationService informationService)
        {
            _logger = logger;
            _hostingEnvironment = hostEnvironment;
            _informationService = informationService;
        }

        public IActionResult Index()
        {

            string h = null;
            string s = null;
            int img_count = 0;
            foreach (var i in _informationService.ShowInformation())
            {
                if (i.Header.ToString() == "รูปปก")
                {
                    img_count = img_count + 1;
                }
                else if (i.Header.ToString() == "เอกสารดาวน์โหลด")
                {
                    if (s == null)
                    {
                        s += i.Section.ToString();
                    }
                    else
                    {
                        s += "," + i.Section.ToString();
                    }
                }
                else
                {
                    if (h == null)
                    {
                        h += i.Header.ToString();
                    }
                    else
                    {
                        h += "," + i.Header.ToString();
                    }
                }
            }
            ViewBag.count_img = 100 / img_count;

            string[] str_header = h.Split(",");
            IEnumerable<string> result_header = str_header.Distinct();
            string[] data_header = result_header.ToArray();

            string[] str_section = s.Split(",");
            IEnumerable<string> result_section = str_section.Distinct();
            string[] data_section = result_section.ToArray();

            ViewBag.tab_header_topic = data_header;
            ViewBag.css_tab_header = data_header.Length + 1;

            var result_data = _informationService.ShowInformation();

            for (int n_header = 0; n_header < data_header.Length; n_header++)
            {
                if (n_header == 0)
                {
                    ViewBag.tab_header = "<div class='actives tab-desc'>";
                    ViewBag.tab_body = "<div class='actives tab-desc'>";
                }
                else
                {
                    ViewBag.tab_header += "<div class='tab-desc'>";
                    ViewBag.tab_body += "<div class='tab-desc'>";
                }

                ViewBag.tab_header += "<h3>" + data_header[n_header] + "</h3>";
                ViewBag.tab_header += "</div>";

                foreach (var desc in result_data)
                {
                    if (desc.Header == data_header[n_header])
                    {
                        //ViewBag.tab_body += "<h4>" + desc.Section + "</h4>";


                        ViewBag.tab_body += "<p><a href='../File/Information/" + desc.FilePath + "' target='_blank'>";
                        ViewBag.tab_body += desc.Description + "</a></p>";
                        ViewBag.tab_body += "<hr>";
                    }
                    //ViewBag.tab_body += "<hr>";
                }
                ViewBag.tab_body += "</div>";
            }

            ViewBag.tab_header += "<div class='tab-desc'>";
            ViewBag.tab_header += "<h3>เอกสารดาวน์โหลด</h3>";
            ViewBag.tab_header += "</div>";

            ViewBag.tab_body += "<div class='tab-desc'>";

            for (int n_section = 0; n_section < data_section.Length; n_section++)
            {
                ViewBag.tab_body += "<h4>" + data_section[n_section] + "</h4>";
                foreach (var desc in result_data)
                {
                    if (desc.Header == "เอกสารดาวน์โหลด" && desc.Section == data_section[n_section])
                    {
                        ViewBag.tab_body += "<p><a href='../File/Information/" + desc.FilePath + "' target='_blank'>";
                        ViewBag.tab_body += desc.Description + "</a></p>";
                        //ViewBag.tab_body += "<hr>";
                    }
                }
                ViewBag.tab_body += "<hr>";
            }
            ViewBag.tab_body += "</div>";

            return View(_informationService.ShowInformation());
        }

    }
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
