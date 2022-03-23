using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
namespace PRS_System.Models.Information
{
    public class InfomationModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public IFormFile FilePDF { get; set; }
        public string FilePath { get; set; }
        public string Date { get; set; }

        public InfomationModel ToAddNews()
        {
            InfomationModel addnews = new InfomationModel();
            addnews.Header = Header;
            addnews.Description = Description;
            addnews.FilePath = FilePath;
            addnews.Date = Date;
            return addnews;
        }
    }
}
