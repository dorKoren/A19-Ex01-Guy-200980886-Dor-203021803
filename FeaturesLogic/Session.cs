using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Session
    {
        public User        LoggedInUser { get; set; }
        public LoginResult LoginResult  { get; set; }
        public bool        m_IsSessionSuccess;

        public Session()
        {
            m_IsSessionSuccess = false;
            startSession();
        }

        private void startSession()
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
                m_IsSessionSuccess = !m_IsSessionSuccess;
            }
        }

        public void EndSession()
        {
            LoggedInUser = null;
            LoginResult = null;
        }
    }
}
