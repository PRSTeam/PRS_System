using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Data
{
    public class FormPRSDataModel
    {

        public int id_tor { get; set; }
        public string idRoom { get; set; }
        public string nameProcument { get; set; }
        public string description_1 { get; set; }
        public string supportBy { get; set; }
        public string checksupport { get; set; }
        public int quotationNum { get; set; }
        public int scopeWork { get; set; }
        public int docNum { get; set; }
        public int budgetDoc { get; set; }
        public string otherSupport { get; set; }
        public int otherSupport_num { get; set; }
        public string supportType { get; set; }
        public string definition { get; set; }
        public string diractor_1 { get; set; }
        public string diractor_2 { get; set; }
        public string diractor_3 { get; set; }
        public string Status { get; set; }
#nullable enable
        public DateTime? Date { get; set; }
        public string? FilePath { get; set; }
        public string? User_ID { get; set; }
        public string Fullname_PRS { get; set; }
        public string buttonstatus { get; set; }
    }
}
