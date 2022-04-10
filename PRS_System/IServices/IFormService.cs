using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
using PRS_System.Models.FormModel;
namespace PRS_System.IServices
{
    public interface IFormService
    {
        public void AddFormDetailData(FormPRSDataModel formdetaildata);
        public void AddSubjectData(List<SubjectDataModel> formdetaildata, int ID_TOR);
        public void AddProductData(List<ProductDataModel> formdetaildata, int ID_TOR);
        public int GetMaximumID_TOR();
        public int GetMaximumID_PRODUCT_LIST();
        public int GetMaximumID_COM();

        public int GetMaximumID_ASSIST();
        public List<FormPRSDataModel> GetnamePRS(string user_id);
        public List<FormPRSDataModel> GetListSuppies();
        public List<FormPRSDataModel> GetListApproval(string id_com);
        public List<FormPRSDataModel> GetListApprovalCerrent(string cerrent);
        public int GetMaximumID_SUBJECT_LIST();
        public FormPRSModel GetValuesFormPRS(int id_tor);
        public List<ProductDataModel> GetValuesFormPRSProduct(int id_tor);
        public List<SubjectDataModel> GetValuesFormPRSSubject(int id_tor);

        public void EditFormDetailData(FormPRSDataModel formdetaildata, int id_tor);
        public void EditFormProductlData(ProductDataModel formdetaildata);
        public void EditFormSubjectData(SubjectDataModel formdetaildata);
        public void UpdateAddProductData(ProductDataModel formdetaildata, int id_tor);

        public void UpdateAddSubjectData(SubjectDataModel formdetaildata, int id_tor);

        public void DeleteFormProductData(int ID_Product);

        public void DeleteFormSubjectData(int ID_Subject);

        public void AddDataSupplies(FormPRSModel datasupplies, int id_tor);
        public int GetMaximumID_ORDER_DIRACT();

        public FormPRSModel Get_PRS_ORDER_DIRACT(int id_tor);

        public void updatestatusform(string status, int id_tor, string cerrent_flow);

        public void UpdateDataOrder_Suppies(FormPRSModel data, int id_tor);
       

        public FormPRSModel.CommentDataModel GetCommentApproval(int id_tor, string nameapproval);
        public FormPRSModel GetCommentApproval2(int id_tor, string id_com);
        public void AddCommentApproval(FormPRSModel data);
        public void EditCommentApproval(FormPRSModel data);
        public void AddAssist_TOR(FormPRSModel data);
        public void EditAssist_TOR(FormPRSModel data);
        public FormPRSModel GetAssist(int id_tor);

        public List<FormPRSDataModel> SearchPRS_Proqument(string user_id,string keyword);
        public List<FormPRSDataModel> SearchPRS_Suppies(string keyword);
        public List<FormPRSDataModel> SearchPRS_Approval(string keyword, string id_com,string cerrent_flow);

        public void DeleteAllFormTOR(int id_tor);


    }
}
