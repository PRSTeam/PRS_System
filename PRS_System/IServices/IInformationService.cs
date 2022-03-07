using PRS_System.Models.Information;
using System.Collections.Generic;

namespace PRS_System.IServices
{
    public interface IInformationService
    {
        public List<InformationModel> ShowInformation();
    }
}
