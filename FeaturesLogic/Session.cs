using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Session
    {

        // DOR : we should think if this class should be static.

        #region Class Members
        public User        LoggedInUser     { get; set; }
        public LoginResult LoginResult      { get; set; }
        public bool        IsSessionSuccess { get; set; }
        #endregion Class Members

        #region Constructor
        public Session()
        {
            IsSessionSuccess = false;
        }
        #endregion Constructor

        #region Public Methods
        public void EndSession()
        {
            LoggedInUser = null;
            LoginResult = null;
        }
        #endregion Public Methods

        #region Private Methods
        public void StartSession()
        {
            LoginResult = FacebookService.Login(
                //"1450160541956417",
                "2121776861417547",
                //"friends_birthday",
                "user_friends",
                "user_photos",
                "public_profile",
                "groups_access_member_info",
                "user_age_range",
                "user_gender");

            if (!string.IsNullOrEmpty(LoginResult.AccessToken))
            {
                LoggedInUser = LoginResult.LoggedInUser;
                IsSessionSuccess = !IsSessionSuccess;
            }
        }
        #endregion Private Methods
    }
}
