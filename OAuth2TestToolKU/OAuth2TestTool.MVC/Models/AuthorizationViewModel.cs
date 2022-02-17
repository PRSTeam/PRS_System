using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2TestTool.MVC.Models
{
    public class AuthorizationViewModel
    {
		[Required(ErrorMessage = "Authorization endpoint is required.")]
		public string AuthorizationEndpoint { get; set; }

		[Required(ErrorMessage = "Client ID is required.")]
		public string ClientId { get; set; }

        [Required(ErrorMessage = "Client Secret is required.")]
        public string ClientSecret { get; set; }

        [Required(ErrorMessage = "Redirect URI is required.")]
		public string RedirectURI { get; set; }

		[Required(ErrorMessage = "Scope is required.")]
		public string Scope { get; set; }

		[Required(ErrorMessage = "State is required.")]
		public string State { get; set; }

        [Required(ErrorMessage = "TokenEndpoint is required.")]
        public string TokenEndpoint { get; set; }

        [Required(ErrorMessage = "UserInfoEndpoint is required.")]
        public string UserInfoEndpoint { get; set; }
    }
}
