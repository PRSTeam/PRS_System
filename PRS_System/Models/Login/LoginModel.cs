using System.ComponentModel.DataAnnotations;

namespace PRS_System.Models.Login
{
    public class LoginModel
    {
        public class Sendlogin
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class Receivelogin
        {
            public string user_id { get; set; }
            public string name_th { get; set; }
            public string name_eng { get; set; }
            public string fullname_th { get; set; }
            public string fullname_eng { get; set; }
            public string position { get; set; }
            public string user_type { get; set; }
        }
    }
}
