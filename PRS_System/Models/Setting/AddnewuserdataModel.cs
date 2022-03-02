using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Setting
{
    public class AddnewuserdataModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Full_NameThai { get; set; }
        [Required]
        public string Full_NameEng { get; set; }
        [Required]
        public string Prefix_NameThai { get; set; }
        [Required]
        public string Prefix_NameEng { get; set; }
        [Required]
        public string User_Type { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string ESignature { get; set; }

        public UserDataModel ToAddnewuserdata(string ImgName)
        {
            UserDataModel addnewuserdata = new UserDataModel();
            addnewuserdata.UserID = UserID;
            addnewuserdata.Full_NameThai = Full_NameThai;
            addnewuserdata.Full_NameEng = Full_NameEng;
            addnewuserdata.Prefix_NameThai = Prefix_NameThai;
            addnewuserdata.Prefix_NameEng = Prefix_NameEng;
            addnewuserdata.User_Type = User_Type;
            addnewuserdata.Category = Category;
            addnewuserdata.Status = Status;
            addnewuserdata.ImgName = ImgName;
            return addnewuserdata;
        }
    }
}
