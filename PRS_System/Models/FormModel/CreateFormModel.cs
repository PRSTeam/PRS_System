using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.Models.FormModel
{
    public class CreateFormModel
    {
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
        List<ProductModel> ProductModels { get; set; }
        public string name_select1 { get; set; }
        public string name_select2 { get; set; }
        public FormPRSDataModel FormData()
        {
            FormPRSDataModel formprs_data = new FormPRSDataModel();
            return formprs_data;
        }
    }
}
