using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.Models.FormModel
{
    public class CreateFormModel
    {
        //-----------Procurement--------------
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
        public List<ProductDataModel> Productdata { get; set; }
        public List<SubjectDataModel> Subjectdata { get; set; }
        //---------------------------------------

        public string definition { get; set; }
        public string name_select1 { get; set; }
        public string name_select2 { get; set; }
       
        public FormPRSDataModel FormDataDetail()
        {
            FormPRSDataModel formprs_data = new FormPRSDataModel();
            formprs_data.idRoom = idRoom;
            formprs_data.nameProcument = nameProcument;
            formprs_data.description_1 = description_1;
            formprs_data.supportBy = supportBy;
            formprs_data.checksupport = checksupport;
            formprs_data.quotationNum = quotationNum;
            formprs_data.scopeWork = scopeWork;
            formprs_data.docNum = docNum;
            formprs_data.budgetDoc = budgetDoc;
            formprs_data.otherSupport = otherSupport;
            formprs_data.otherSupport_num = otherSupport_num;
            
            return formprs_data;
        }
    }
}
