using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace dmportella.Facebook
{
    /// <summary>
    /// Facebook Application settings class.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// APICAlls Constant values for the each API call
        /// </summary>
        internal static class APICalls
        {
            // hardcoded at the moment
            /// <summary>
            /// The url format for fetching an application accesskey from the facebook api.
            /// </summary>
            public const string GetApplicationAccessKey = "https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&grant_type={2}";

            /// <summary>
            /// The list of constants of grant types used on the facebook api.
            /// </summary>
            internal static class GrantTypes
            {
                public const string client_credentials = "client_credentials";
            }
        }

        private string accessToken;

        /// <summary>
        /// The facebook application Id.
        /// </summary>
        public string ApplicationId { get; private set; }

        /// <summary>
        /// The application API access token.
        /// </summary>
        public string AccessToken
        {
            get { return this.accessToken; }
        }

        /// <summary>
        /// Indicates if this application instance has an access token.
        /// </summary>
        public bool HasAccessCode
        {
            get { return !(string.IsNullOrEmpty(this.accessToken)); }
        }

        /// <summary>
        /// Creates an instance of Application.
        /// </summary>
        /// <param name="applicationId">The facebook application id.</param>
        public Application(string applicationId)
        {
            this.ApplicationId = applicationId;
        }

        /// <summary>
        /// Calles the facebook api to request for a access token to access the facebook application api.
        /// </summary>
        /// <param name="applicationSecret">The application secret key.<remarks>You can find this value on the facebook application detail page.</remarks></param>
        /// <returns>Returns true if the call was successful.</returns>
        public bool GetAccessCode(string applicationSecret)
        {
            string response = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                response = webClient.DownloadString(string.Format(APICalls.GetApplicationAccessKey, this.ApplicationId, applicationSecret, APICalls.GrantTypes.client_credentials));
            }
            // pain string they sent us is not wrapped as a json object so i just split on the '=' sign to get jus the access token
            // must do this better
            this.accessToken = response.Replace("access_token=", "");

            return this.HasAccessCode;
        }
    }
}
