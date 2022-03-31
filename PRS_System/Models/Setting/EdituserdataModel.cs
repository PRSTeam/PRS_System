using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Setting
{
    public class EdituserdataModel
    {
        
        public string UserID { get; set; }
        
        public string Full_NameThai { get; set; }
        
        public string Full_NameEng { get; set; }
        
        public string Prefix_NameThai { get; set; }
        
        public string Prefix_NameEng { get; set; }
        public string Email { get; set; }

        public string User_Type_Operation { get; set; }
        public string User_Type_Magnement { get; set; }

        public string Category { get; set; }
        
        public string Status { get; set; }
        
        public string ESignature { get; set; }
        public List<UserDataModel> userdata { get; set; }

        //public UserDataModel ToEdituserdata(string ImgName)
        //{
        //    UserDataModel edituserdata = new UserDataModel();
        //    edituserdata.UserID = UserID;
        //    edituserdata.Full_NameThai = Full_NameThai;
        //    edituserdata.Full_NameEng = Full_NameEng;
        //    edituserdata.Prefix_NameThai = Prefix_NameThai;
        //    edituserdata.Prefix_NameEng = Prefix_NameEng;
        //    edituserdata.User_Type = User_Type;
        //    edituserdata.Category = Category;
        //    edituserdata.Status = Status;
        //    edituserdata.ImgName = ImgName;
        //    return edituserdata;
        //}
    }
}
