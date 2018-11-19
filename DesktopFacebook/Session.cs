using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{

    internal class Session
    {
        internal User        LoggedInUser { get; set; }
        internal LoginResult LoginResult  { get; set; }
        internal mainForm    Form         { get;      }

        internal Session(mainForm i_Form)
        {
            Form = i_Form;
            //Login();
        }

        public void Login()
        {
            LoginResult = FacebookService.Login(
                "2121776861417547",
                //"1450160541956417",
                "friends_birthday",
                "user_friends",
                "user_photos",
                "public_profile",
                "email",
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
                Form.FetchUserInfo();
            }
            else
            {
                MessageBox.Show(LoginResult.ErrorMessage);
            }           
        }

        

    }
}
