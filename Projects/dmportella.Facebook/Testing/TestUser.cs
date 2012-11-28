using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;

namespace dmportella.Facebook.Testing
{
    /// <summary>
    /// Instance of a Test User for facebook
    /// </summary>
    public sealed class TestUser
    {
        /// <summary>
        /// The json object representing the user.
        /// </summary>
        private dynamic jsonUser;

        /// <summary>
        /// The user Facebook ID.
        /// </summary>
        public string Id
        {
            get { return this.jsonUser.id; }
            set { this.jsonUser.id = value; }
        }

        /// <summary>
        /// The user access token.
        /// </summary>
        public string AccessToken
        {
            get { return this.jsonUser.access_token; }
            set { this.jsonUser.access_token = value; }
        }

        /// <summary>
        /// The user facebook URL.
        /// <remarks>Use this url to login to the user account on the browser.</remarks>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        public string LoginUrl
        {
            get { return this.jsonUser.login_url; }
            set { this.jsonUser.login_url = value; }
        }

        /// <summary>
        /// The user email account.
        /// <remarks>Use this email to login to the user account on the browser.</remarks>
        /// </summary>
        public string Email
        {
            get { return this.jsonUser.email; }
            set { this.jsonUser.email = value; }
        }

        /// <summary>
        /// The user password.
        /// <remarks>Use this password to login to the user account on the browser.</remarks>
        /// </summary>
        public string Password
        {
            get { return this.jsonUser.password; }
            set { this.jsonUser.password = value; }
        }

        /// <summary>
        /// Creates a instance of a TestUser object.
        /// </summary>
        /// <param name="jsonUser">The Json Value instance to use for creating the user object.</param>
        public TestUser(JsonValue jsonUser)
        {
            this.jsonUser = jsonUser;
        }

        /// <summary>
        /// Creates a instance of a TestUser object.
        /// </summary>
        /// <param name="jsonUser">The Json string to use for creating the user object.</param>
        public TestUser(string jsonUser)
        {
            this.jsonUser = JsonValue.Parse(jsonUser);
        }

        /// <summary>
        /// Creates a instance of a TestUser object.
        /// </summary>
        public TestUser()
        {
            this.jsonUser = new JsonObject();
        }

        public override string ToString()
        {
            return (this.jsonUser != null) ? this.jsonUser.ToString() : base.ToString();
        }
    }
}
