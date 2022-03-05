using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.IServices
{
    public interface IFormService
    {
        public void AddFormDetailData(FormPRSDataModel formdetaildata, int ID_TOR);
        public void AddSubjectData(List<SubjectDataModel>  formdetaildata,int ID_TOR);
        public void AddProductData(List<ProductDataModel> formdetaildata, int ID_TOR);

        public int GetMaximumID_TOR();
        public int GetMaximumID_PRODUCT_LIST();
        public int GetMaximumID_COM();

        public int GetMaximumID_ASSIST();

        public int GetMaximumID_SUBJECT_LIST();
    }
}
