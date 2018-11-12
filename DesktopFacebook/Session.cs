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
    //public delegate void LoginEventHandler(object sender, LoginEventHandler e);

    internal class Session
    {
        //public event LoginEventHandler LoggedIn;

        public User        LoggedInUser { get; set; }
        public LoginResult LoginResult  { get; set; }

        private Session()
        {

        }

        public void Login()
        {
            LoginResult = FacebookService.Login(     
                "2121776861417547",
                "user_birthday",
                "user_photos");

            LoggedInUser = LoginResult.LoggedInUser;



            if (!string.IsNullOrEmpty(LoginResult.AccessToken))
            {
                LoggedInUser = LoginResult.LoggedInUser;
                //fetchUserInfo();
            }
            else
            {
                MessageBox.Show(LoginResult.ErrorMessage);
            }           
        }
    }
}
