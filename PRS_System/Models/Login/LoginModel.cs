using Newtonsoft.Json;
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

        public class ReceiveSession
        {
            [JsonProperty("ACCESS_TOKEN")]
            public string AccessToken { get; set; }

            [JsonProperty("thainame")]
            public string thainame { get; set; }

            [JsonProperty("first-name")]
            public string first_name { get; set; }

            [JsonProperty("last-name")]
            public string last_name { get; set; }

            [JsonProperty("cn")]
            public string cn { get; set; }

            [JsonProperty("givenname")]
            public string givenname { get; set; }

            [JsonProperty("surname")]
            public string surname { get; set; }

            [JsonProperty("thaiprename")]
            public string thaiprename { get; set; }

            [JsonProperty("jobtype")]
            public string jobtype { get; set; }

            [JsonProperty("type-person")]
            public string type_person { get; set; }

            [JsonProperty("position")]
            public string position { get; set; }

            [JsonProperty("position-id")]
            public string position_id { get; set; }

            [JsonProperty("campus")]
            public string campus { get; set; }

            [JsonProperty("faculty")]
            public string faculty { get; set; }

            [JsonProperty("faculty-id")]
            public string faculty_id { get; set; }

            [JsonProperty("department")]
            public string department { get; set; }

            [JsonProperty("department-id")]
            public string department_id { get; set; }

            [JsonProperty("advisor-id")]
            public string advisor_id { get; set; }

            [JsonProperty("mail")]
            public string mail { get; set; }

            [JsonProperty("google-mail")]
            public string google_mail { get; set; }

            [JsonProperty("office365-mail")]
            public string office365_mail { get; set; }

            [JsonProperty("userprincipalname")]
            public string userprincipalname { get; set; }

            [JsonProperty("uid")]
            public string uid { get; set; }

            [JsonProperty("zoneid")]
            public string zoneid { get; set; }

            [JsonProperty("zone")]
            public string zone { get; set; }
        }
    }
}
