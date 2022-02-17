using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2TestTool.MVC.Models
{
    public class UserInfoViewModel
    {

		[Required(ErrorMessage = "Access Token is required.")]
		public string AccessToken { get; set; }

        [Required(ErrorMessage = "UserInfoEndpoint is required.")]
        public string UserInfoEndpoint { get; set; }

    
    }
}
