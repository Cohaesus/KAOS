using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Dynamic;
using System.Json;

namespace dmportella.Facebook.Testing
{
    public sealed class UserClient
    {
        /// <summary>
        /// APICAlls Constant values for the each API call
        /// </summary>
        internal static class APICalls
        {
            /// <summary>
            /// Facebook API call for creating a test user.
            /// </summary>
            public const string CreateUser = "https://graph.facebook.com/{0}/accounts/test-users?installed={1}&permissions={2}&method={3}&access_token={4}";

            /// <summary>
            /// Facebook API call for deleting a test user.
            /// </summary>
            public const string DeleteUser = "https://graph.facebook.com/{0}?method={1}&access_token={2}";

            /// <summary>
            /// Facebook API call for Befriending a test user.
            /// </summary>
            public const string BeFriendUser = "https://graph.facebook.com/{0}/friends/{1}?method={2}&access_token={3}";

            /// <summary>
            /// HTTP Methods Constants.
            /// <remarks>Done this way just to ensure consistency.</remarks>
            /// </summary>
            internal static class Methods
            {
                /// <summary>
                /// HTTP Delete method.
                /// </summary>
                public const string Delete = "delete";

                /// <summary>
                /// HTTP Post method.
                /// </summary>
                public const string Post = "post";
            }
        }

        private Application application;

        /// <summary>
        /// Instanciate a User client class.
        /// </summary>
        /// <param name="application">The facebook application settings.</param>
        public UserClient(Application application)
        {
            if (application == null)
            {
                throw new ArgumentNullException("application");
            }

            if (!(application != null && application.HasAccessCode))
            {
                throw new ArgumentException("Application must have a valid access key.", "application");
            }

            this.application = application;
        }

        /// <summary>
        /// Creates a new testing user using the Facebook application id and access key.
        /// </summary>
        /// <param name="installed">Indicates to facebook if the user should have the application already installed when they are created.</param>
        /// <param name="permissions">The list of facebook permissions the application will have on that user.</param>
        /// <returns>Returns a test user instance representing the newly created user.</returns>
        public TestUser CreateUser(bool installed, string permissions)
        {
            if (string.IsNullOrEmpty(permissions))
            {
                throw new ArgumentNullException("permissions", "You must have at least one permission to request.");
            }

            using (WebClient webClient = new WebClient())
            {
                string response = webClient.DownloadString(string.Format(APICalls.CreateUser, this.application.ApplicationId, installed, permissions, APICalls.Methods.Post, this.application.AccessToken));

                TestUser user = new TestUser(JsonValue.Parse(response));

                return user;
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The test user instance to be deleted.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public bool DeleteUser(TestUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (WebClient webClient = new WebClient())
            {
                string response = webClient.DownloadString(string.Format(APICalls.DeleteUser, user.Id, APICalls.Methods.Delete, user.AccessToken));

                return Boolean.Parse(response);
            }
        }

        /// <summary>
        /// Sends a friends request from user 1 to user 2.
        /// </summary>
        /// <param name="user1">User sending the request.</param>
        /// <param name="user2">User receiving the request.</param>
        /// <returns>Returns true if the request was successful.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public bool SendFriendRequest(TestUser user1, TestUser user2)
        {
            if (user1 == null)
            {
                throw new ArgumentNullException("user1");
            }

            if (user2 == null)
            {
                throw new ArgumentNullException("user2");
            }

            using (WebClient webClient = new WebClient())
            {
                string response1 = webClient.DownloadString(string.Format(APICalls.BeFriendUser, user1.Id, user2.Id, APICalls.Methods.Post, user1.AccessToken));

                return Boolean.Parse(response1);
            }
        }

        /// <summary>
        /// Automatically be friend two test user instances.
        /// </summary>
        /// <param name="user1">User A</param>
        /// <param name="user2">User B</param>
        /// <returns>Returns true if the request was successful.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public bool BefriendEachOther(TestUser user1, TestUser user2)
        {
            if (user1 == null)
            {
                throw new ArgumentNullException("user1");
            }

            if (user2 == null)
            {
                throw new ArgumentNullException("user2");
            }

            using (WebClient webClient = new WebClient())
            {
                string response1 = webClient.DownloadString(string.Format(APICalls.BeFriendUser, user1.Id, user2.Id, APICalls.Methods.Post, user1.AccessToken));

                string response2 = webClient.DownloadString(string.Format(APICalls.BeFriendUser, user2.Id, user1.Id, APICalls.Methods.Post, user2.AccessToken));

                bool call1 = Boolean.Parse(response1);
                bool call2 = Boolean.Parse(response2);

                return call1 && call2;
            }
        }
    }
}
