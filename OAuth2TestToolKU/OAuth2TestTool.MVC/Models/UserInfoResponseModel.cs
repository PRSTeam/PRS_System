using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2TestTool.MVC.Models
{
	public class UserInfoResponseModel
	{
		[JsonProperty("uid")]
		public string uid { get; set; }

        [JsonProperty("thaiprename")]
        public string thaiprename { get; set; }
        /*
        [JsonProperty("thainame")]
        public string thainame { get; set; }

        [JsonProperty("first-name")]
        public string first-name { get; set; }

        [JsonProperty("last-name")]
        public string last-name { get; set; }

        [JsonProperty("cn")]
        public string cn { get; set; }

        [JsonProperty("givenname")]
        public string givenname { get; set; }

        [JsonProperty("surname")]
        public string surname { get; set; }

        [JsonProperty("faculty")]
        public string faculty { get; set; }
        */
    }
}
