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
            int img_count = 0;
            foreach (InfomationModel i in _informationService.ShowInformation())
            {
                if (i.Header.ToString() != "รูปปก")
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
                else
                {
                    img_count = img_count + 1;
                }
            }
            ViewBag.count_img = 100 / img_count;

            string[] str = h.Split(",");
            IEnumerable<string> result = str.Distinct();

            string[] data = result.ToArray();

            ViewBag.tab_header_topic = data;
            ViewBag.css_tab_header = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    ViewBag.tab_header = "<div class='actives tab-desc'>";
                    ViewBag.tab_header += "<h3>" + data[i] + "</h3>";
                    ViewBag.tab_header += "</div>";

                    ViewBag.tab_body = "<div class='actives tab-desc'>";
                    foreach (var desc in _informationService.ShowInformation())
                    {
                        if (desc.Header == data[i])
                        {
                            ViewBag.tab_body += "<p><a href='../File/Information/" + desc.FilePath + "' target='_blank'>";
                            ViewBag.tab_body += desc.Description + "</a></p>";
                            ViewBag.tab_body += "<hr>";
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
                    foreach (var desc in _informationService.ShowInformation())
                    {
                        if (desc.Header == data[i])
                        {
                            ViewBag.tab_body += "<p><a href='../File/Information/" + desc.FilePath + "' target='_blank'>";
                            ViewBag.tab_body += desc.Description + "</a></p>";
                            ViewBag.tab_body += "<hr>";
                        }
                    }
                    ViewBag.tab_body += "</div>";
                }
            }

            return View(_informationService.ShowInformation());
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
