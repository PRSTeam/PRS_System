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
        public void AddSubjectData(List<SubjectDataModel>  formdetaildata,int ID_TOR);
        public void AddProductData(List<ProductDataModel> formdetaildata, int ID_TOR);
        public int GetMaximumID_TOR();
        public int GetMaximumID_PRODUCT_LIST();
        public int GetMaximumID_COM();

        public int GetMaximumID_ASSIST();
        public List<FormPRSDataModel> GetnamePRS(string user_id);
        public int GetMaximumID_SUBJECT_LIST();
        public FormPRSModel GetValuesFormPRS(int id_tor);
        public List<ProductDataModel> GetValuesFormPRSProduct(int id_tor);
        public List<SubjectDataModel> GetValuesFormPRSSubject(int id_tor);

        public void EditFormDetailData(FormPRSDataModel formdetaildata);
        public void EditFormProductlData(List<ProductDataModel> formdetaildata, int ID_TOR);
        public void EditFormSubjectData(List<SubjectDataModel> formdetaildata, int ID_TOR);

        public void DeleteFormProductData(int ID_Product);

        public void DeleteFormSubjectData(int ID_Subject);
    }
}
