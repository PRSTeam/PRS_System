using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
using PRS_System.Models.Setting;
namespace PRS_System.IServices
{
    public interface IAccountService
    {
        public void AddNewUser(UserDataModel datauser);
        public List<UserDataModel> GetDataUser(string user_id);
        public string GetSignature(string user_id);
        IEnumerable<UserDataModel> GetAllAccountWithKeyword(string keyword);
    }
}
