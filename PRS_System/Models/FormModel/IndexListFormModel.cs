using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.Models.FormModel
{
    public class IndexListFormModel
    {
#nullable enable
        public List<FormPRSDataModel>? ListForm { get; set; }
        public List<FormPRSDataModel>? ListSuppies { get; set; }
        public List<FormPRSDataModel>? ListApproval { get; set; }
        public string login_userid { get; set; }
        public string category_user { get; set; }
    }
}
