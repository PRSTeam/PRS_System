using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.IServices
{
    public interface IFormService
    {
        public void AddFormDetailData(FormPRSDataModel formdetaildata);
        public void AddSubjectData(ProductDataModel formdetaildata);
        public void AddProductData(SubjectDataModel formdetaildata);

        public int GetMaximumID_TOR();
    }
}
