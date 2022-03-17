using PRS_System.Models.Information;
using System.Collections.Generic;

namespace PRS_System.IServices
{
    public interface IInformationService
    {
        public List<InfomationModel> ShowInformation();
        public void AddNewsDetailData(InfomationModel infdata);
    }
}
