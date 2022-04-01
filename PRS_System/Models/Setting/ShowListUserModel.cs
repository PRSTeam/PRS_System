using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Setting
{
    public class ShowListUserModel
    {
        public string Keyword { get; set; }
        public string UserID { get; set; }

        public string Full_NameThai { get; set; }

        public string Full_NameEng { get; set; }

        public string Prefix_NameThai { get; set; }

        public string Prefix_NameEng { get; set; }

        public string User_Type { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public string ESignature { get; set; }
#nullable enable
        public List<UserDataModel>? userdata { get; set; }

        //public UserDataModel ToShowuserdata(string ImgName)
        //{
        //    UserDataModel showuserdata = new UserDataModel();
        //    showuserdata.UserID = UserID;
        //    showuserdata.Full_NameThai = Full_NameThai;
        //    showuserdata.Full_NameEng = Full_NameEng;
        //    showuserdata.Prefix_NameThai = Prefix_NameThai;
        //    showuserdata.Prefix_NameEng = Prefix_NameEng;
        //    showuserdata.User_Type = User_Type;
        //    showuserdata.Category = Category;
        //    showuserdata.Status = Status;
        //    showuserdata.ImgName = ImgName;
        //    return showuserdata;
        //}
    }
}
