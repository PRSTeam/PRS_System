using PRS_System.Models.Information;
using System.Collections.Generic;

namespace PRS_System.IServices
{
    public interface IInformationService
    {
        public List<InfomationModel> ShowInformation();
        public void AddNewsDetailData(InfomationModel infdata);
        public void Del_data(string filename);
        public void Add_data(string filename);
    }
}
