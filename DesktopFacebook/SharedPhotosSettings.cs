using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class SharedPhotosSettings
    {
        private readonly bool r_WasFound = true;

        public User Friend { get; set; }
        public bool FriendWasFound { get; set; }

        internal SharedPhotosSettings()
        {
            Friend = null;
            FriendWasFound = !r_WasFound;
        }

        public void FindFriend(string i_FirstName, string i_LastName, User i_LoggedInUser)
        {            
            foreach (User user in i_LoggedInUser.Friends)
            {
                if ((i_FirstName.Equals(user.FirstName, StringComparison.OrdinalIgnoreCase) &&
                     (i_LastName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase))))
                {
                    Friend = user;
                    FriendWasFound = r_WasFound;
                    break;
                }

                else
                {
                    FriendWasFound = false;
                    Friend = null;
                }
            }

        }

    }
}
