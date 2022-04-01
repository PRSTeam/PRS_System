using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Data
{
    public class UserDataModel
    {
        public string Keyword { get; set; }
        public string UserID { get; set; }
        public string Prefix_NameThai { get; set; }
        public string Full_NameThai { get; set; }
        public string Operate_Pos { get; set; }
        public string Manage_Pos { get; set; }
        public string Email { get; set; }
        public string ESignature { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

        public string Full_NameEng { get; set; }
        public string Prefix_NameEng { get; set; }
        public string User_Type_Operation { get; set; }
        public string User_Type_Magnement { get; set; }
        public string ImgName { get; set; }
    }
}
