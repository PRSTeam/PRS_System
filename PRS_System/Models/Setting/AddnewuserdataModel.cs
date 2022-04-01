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
        
        public string Full_NameEng { get; set; }
        [Required]
        public string Prefix_NameThai { get; set; }
        
        public string Prefix_NameEng { get; set; }
        [Required]
        public string User_Type_Operation { get; set; }
        public string User_Type_Magnement{ get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Status { get; set; }
        
        public string ESignature { get; set; }

        public UserDataModel ToAddnewuserdata(string ImgName)
        {
            UserDataModel addnewuserdata = new UserDataModel();
            addnewuserdata.UserID = UserID;
            addnewuserdata.Full_NameThai = Full_NameThai;
            addnewuserdata.Full_NameEng = Full_NameEng;
            addnewuserdata.Prefix_NameThai = Prefix_NameThai;
            addnewuserdata.Prefix_NameEng = Prefix_NameEng;
            addnewuserdata.User_Type_Operation = User_Type_Operation;
            addnewuserdata.User_Type_Magnement = User_Type_Magnement;
            addnewuserdata.Category = Category;
            addnewuserdata.Status = Status;
            addnewuserdata.ImgName = ImgName;
            return addnewuserdata;
        }
    }
}
