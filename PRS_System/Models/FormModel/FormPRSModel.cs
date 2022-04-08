using Microsoft.AspNetCore.Http;
using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.FormModel
{
    public class FormPRSModel
    {
        public double vaxproduct { get; set; }
        public double sumproduct { get; set; }
        public int id_tor { get; set; }
        public string idRoom { get; set; }
        public string nameProcument { get; set; }
        public string description_1 { get; set; }
        public string supportBy { get; set; }
        public string checksupport { get; set; }
        public int prsnum { get; set; }
        public int quotationNum { get; set; }
        public int scopeWork { get; set; }
        public int docNum { get; set; }
        public int budgetDoc { get; set; }
        public string otherSupport { get; set; }
        public int otherSupport_num { get; set; }
        public string supportType { get; set; }
        public string status { get; set; }
        public DateTime? TOR_DATE { get; set; }
#nullable enable
        public List<ProductDataModel>? Productdata { get; set; }
        public List<SubjectDataModel>? Subjectdata { get; set; }
        public IFormFile FilePDF { get; set; }
        public string FilePath { get; set; }
        public string diractor_1 { get; set; }
        public string diractor_2 { get; set; }
        public string diractor_3 { get; set; }
        public string IndexProDelete { get; set; }
        public string IndexSubdelete { get; set; }
        public string checkboxquotation { get; set; }
        public string buttonstatus { get; set; }
        public string buttonstatus_2 { get; set; }
        public string buttonstatus_3 { get; set; }
        public string cerrent_flow { get; set; }
        public int id_order { get; set; }
        public UserDataModel user { get; set; }

        public string login_userid { get; set; }
        public string category_user { get; set; }

        public string type_user_magnement { get; set; }
        public string assist_input0 { get; set; }
        public string assist_input1 { get; set; }
        public string assist_input2 { get; set; }

        public string type_assitst { get; set; }
        public string detail_assist { get; set; }
        public string desc_assist1 { get; set; }
        public string desc_assist2 { get; set; }
        public string desc_assist3 { get; set; }
        //---------------------------------------
        // Suppiess
        public string definition { get; set; }
        public string name_select1 { get; set; }
        public string name_select2 { get; set; }
        public string User_ID { get; set; }

        //----------------------------------------
        //------Approval------------------------
        public class CommentDataModel
        {
            public string comment { get; set; }
            public DateTime? com_date { get; set; }
        }

        public string des_approval0 { get; set; }

        public string des_approval { get; set; }
        public DateTime? date_approval { get; set; }
        public string name_approval { get; set; }
        public string esign_approval { get; set; }

        public string des_approval2 { get; set; }
        public DateTime? date_approval2 { get; set; }
        public string name_approval2 { get; set; }
        public string esign_approval2 { get; set; }

        public string des_approval3 { get; set; }
        public DateTime? date_approval3 { get; set; }
        public string name_approval3 { get; set; }
        public string esign_approval3 { get; set; }

        public string des_approval4 { get; set; }
        public DateTime? date_approval4 { get; set; }
        public string name_approval4 { get; set; }
        public string esign_approval4 { get; set; }

        public string des_approval5 { get; set; }
        public DateTime? date_approval5 { get; set; }
        public string name_approval5 { get; set; }
        public string esign_approval5 { get; set; }

        public string last_approval { get; set; }

        public string Email_Proquement { get; set; }
        public string Email_approval { get; set; }
        public string Email_approval2 { get; set; }
        public string Email_approval3 { get; set; }
        public string Email_approval4 { get; set; }
        public string Email_approval5 { get; set; }

        public List<ApprovalListDataModel> ListEmailAdmin { get; set; }
        public string stringlistemail_admin { get; set; }
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
            formprs_data.diractor_1 = diractor_1;
            formprs_data.diractor_2 = diractor_2;
            formprs_data.diractor_3 = diractor_3;
            formprs_data.FilePath = FilePath;
            formprs_data.User_ID = User_ID;
            formprs_data.buttonstatus = buttonstatus;
            return formprs_data;
        }
    }
}
