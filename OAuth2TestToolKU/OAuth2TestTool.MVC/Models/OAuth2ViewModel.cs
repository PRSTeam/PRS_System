using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth2TestTool.MVC.Models
{
    public class OAuth2ViewModel
    {
		public string AccessToken { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string UserInfo { get; set; }
        public string AuthorizationCode { get; set; }
		public string AuthorizationEndpoint { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string ErrorMessage { get; set; }
		public string Focus { get; set; }
		public string RawResponse { get; set; }
		public string RedirectURI { get; set; }
		public string RefreshToken { get; set; }
		public string RefreshTokenEndpoint { get; set; }
		public string Scope { get; set; }
		public string State { get; set; }
		public string TokenEndpoint { get; set; }
        public string UserInfo_uid { get; set; }
        public string UserInfo_all { get; set; }
    }
}
