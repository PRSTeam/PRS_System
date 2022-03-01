using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.Models.Data;
namespace PRS_System.IServices
{
    public interface IAccountService
    {
        public void AddNewUser(UserDataModel datauser);
        public void EditUser();
    }
}
