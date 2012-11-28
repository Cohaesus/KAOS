using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookTestUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a application instance with you app id.
            dmportella.Facebook.Application application = new dmportella.Facebook.Application("APP ID");

            //Get an access code with you app secret
            if (application.GetAccessCode("APP SECRET"))
            {
                // Create a user client passing in the application instance.
                dmportella.Facebook.Testing.UserClient userClient = new dmportella.Facebook.Testing.UserClient(application);

                // create user 1 with the application already installed and giving the app read_stream permission.
                dmportella.Facebook.Testing.TestUser user1 = userClient.CreateUser(true, "read_stream");

                // create user 2 with the application already installed and giving the app read_stream permission.
                dmportella.Facebook.Testing.TestUser user2 = userClient.CreateUser(true, "read_stream");

                // befriend user1 and user2
                if (userClient.BefriendEachOther(user1, user2))
                {
                    // both users should be friends now.
                    /* The list below is the list of properties available to use on each user instance.
                    user1.LoginUrl;
                    user1.Email;
                    user1.Password;
                    user1.AccessToken;
                    user1.Id;
                     */
                }
            }
        }
    }
}
