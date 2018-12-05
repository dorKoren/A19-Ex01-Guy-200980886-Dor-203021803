using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;


namespace DesktopFacebook
{

    internal class Session
    {
        internal User        LoggedInUser { get; set; }
        internal LoginResult LoginResult  { get; set; }

       
        internal Session()
        {
            StartSession();
        }

        internal void StartSession()
        {
            LoginResult = FacebookService.Login(
                "2121776861417547",
                "friends_birthday",
                "user_friends",
                "user_photos",
                "public_profile",
                "publish_to_groups",
                "groups_access_member_info",
                "user_age_range",
                "user_gender",
                "user_link",
                "user_friends",
                "user_posts");

            if (!string.IsNullOrEmpty(LoginResult.AccessToken))
            {
                LoggedInUser = LoginResult.LoggedInUser;
            }
            else
            {
                MessageBox.Show(LoginResult.ErrorMessage);
            }           
        }

        internal void EndSession()
        {
            LoggedInUser = null;
            LoginResult  = null;
        }
    }
}
