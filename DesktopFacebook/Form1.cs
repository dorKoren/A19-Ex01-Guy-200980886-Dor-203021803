using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;
using static FeaturesLogic.BirthdayDictionary;
using static FacebookWrapper.FacebookService;
using static DesktopFacebook.Properties.Resources;
using System.Collections.Generic;
using System.Linq;
using static  DesktopFacebook.BirthdayWishUI;

namespace DesktopFacebook
{
    public partial class mainForm : Form
    {
        #region Class Members
        private readonly string       r_LogOutMsg           = "Are you sure that you want logout?";
        private readonly string       r_LabelAccountName    = "Account Name";
        private readonly string       r_MainFormName        = "Desktop Facebook";
        private readonly string       r_CaptionLogOut       = "Logout";
        private readonly string       r_PhotosNotFound      = "Photos Not Found!";
        private readonly string       r_MsgFriendNotFound   = "Friend Not Found!";
        private readonly string       r_CaptionFriendSearch = "Friend Search Error";
        private readonly string       r_FirstName           = "First Name:";
        private readonly string       r_LastName            = "Last Name:";
        internal Session              m_Session;        
        internal Serializer           m_Serializer;
        internal SharedPhotos         m_SharedPhotos;
        internal BirthdayWish         m_BirthdayWish;
        #endregion Class Members

        #region Constructor
        public mainForm()
        {
            InitializeComponent();

            /* There is a problems with the serializer loading */
            //m_Serializer = Serializer.LoadFromFile();  

            if (m_Serializer != null &&
                m_Serializer.RememberUser &&
                !string.IsNullOrEmpty(m_Serializer.LastAccessToken))
            {
                Connect(m_Serializer.LastAccessToken);
                m_Session.LoggedInUser = m_Serializer.LastUser;   
                m_BirthdayWish.BirthdayDictionary = m_Serializer.LastUserBirthdayDictionary;
                fetchUserInfo();
            }
        }
        #endregion Constructor

        #region Protected Override Methods
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!buttonLogin.Visible)
            {
                m_Session.EndSession();
                m_Serializer.RememberUser = checkBoxRememberUser.Checked;
                m_Serializer.LastUserBirthdayDictionary = m_BirthdayWish.BirthdayDictionary;

                m_Serializer.LastAccessToken = m_Serializer.RememberUser ?
                    m_Session.LoginResult.AccessToken : null;

                /* There is a problem to save 'Facebook data' to XML file */
                //m_Serializer.SaveToFile();  
            }
        }
        #endregion Protected Override Methods

        #region Private Methods

        #region Fetch Methods
        private void fetchUserInfo()
        {
            bool   isVisible    = true;
            User   loggedInUser = m_Session.LoggedInUser;
            string firstName    = loggedInUser.FirstName;
            string lastName     = loggedInUser.LastName;


            pictureBoxUser.ImageLocation =  loggedInUser.PictureNormalURL;
            this.Text                    =  string.Format("Logged in as {0} {1}", firstName, lastName);
            labelAccountName.Text        =  string.Format("{0} {1}", firstName, lastName);
            checkBoxRememberUser.Visible =  isVisible;
            buttonLogout.Visible         =  isVisible;
            buttonLogin.Visible          = !isVisible;

            fetchSharedPhotosInfo();
            fetchBirthdayWishInfo();
        }

        private void fetchBirthdayWishInfo()
        {
            User loggedInUser = m_Session.LoggedInUser;
            bool isVisible    = true;

            m_BirthdayWish = new BirthdayWish();
            m_BirthdayWish.BirthdayDictionary.FillBirthdays(loggedInUser);

            updateCheckedListBoxWishes();

            buttonSendBirthdayWish.Visible = isVisible;
            textBoxWish.Visible            = isVisible;
        }

        private void fetchSharedPhotosInfo()
        {
            bool isVisible = true;

            m_SharedPhotos = new SharedPhotos();

            textBoxFirstName.Visible = isVisible;
            textBoxLastName.Visible  = isVisible;
            buttonSearch.Visible     = isVisible;
            buttonImport.Visible     = isVisible;
            pictureBoxFriend.Visible = isVisible;
        }
        #endregion Fetch Methods

        #region Event Handler Click Methods
        private void buttonLogin_Click(object sender, EventArgs e)  

        {
            m_Session = new Session();

            if (m_Session.m_IsSessionSuccess)
            {
                m_Serializer = new Serializer();
                checkBoxRememberUser.Checked = m_Serializer.RememberUser;
                fetchUserInfo();
            }
            else
            {
                MessageBox.Show(m_Session.LoginResult.ErrorMessage);
            }
        }

        private void buttonSendBirthdayWish_Click(object sender, EventArgs e)
        {
            postWishToFriends();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            User loggedInUser = m_Session.LoggedInUser;
            User friend = m_SharedPhotos.Friend;
            m_SharedPhotos.ImportSharedPhotos(loggedInUser, friend);

            if (!m_SharedPhotos.SharedPhotosList.Any())  // DOR !!!
            {
                string message = string.Format(@"No photos of You and {0} were found",
                                               m_SharedPhotos.Friend.Name);

                MessageBox.Show(message, r_PhotosNotFound,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SharedPhotosUI.ExportPhotos(m_SharedPhotos.SharedPhotosList);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bool isImportEnabled = true;

            m_SharedPhotos.FindFriend(textBoxFirstName.Text, textBoxLastName.Text, m_Session.LoggedInUser);

            if (m_SharedPhotos.FriendWasFound)
            {
                User friend = m_SharedPhotos.Friend;
                buttonImport.Text = string.Format(@"import shared photos with {0}", friend.Name);
                pictureBoxFriend.Image = friend.ImageNormal;
                buttonImport.Enabled = isImportEnabled;
            }

            else
            {
                MessageBox.Show(r_MsgFriendNotFound, r_CaptionFriendSearch,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxFirstName.Text = r_FirstName;
                textBoxLastName.Text = r_LastName;
                buttonImport.Enabled = !isImportEnabled;
                pictureBoxFriend.Image = initial_friend_image_picture;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            string message = string.Format(r_LogOutMsg);

            DialogResult dialog = MessageBox.Show(message, r_CaptionLogOut, MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                resetDesktop();
            }
        }

        private void textBoxFirstName_Click(object sender, EventArgs e)
        {
            textBoxFirstName.Text = "";
        }

        private void textBoxLastName_Click(object sender, EventArgs e)
        {
            textBoxLastName.Text = "";
        }
        #endregion Event Handler Click Methods

        #region Reset Methods
        private void resetDesktop()
        {
            bool isVisible = true;

            m_Session.LoggedInUser = null;
            m_Session.LoginResult = null;
            labelAccountName.Text = r_LabelAccountName;
            this.Text = r_MainFormName;
            buttonLogin.Visible = isVisible;
            buttonLogout.Visible = !isVisible;
            checkBoxRememberUser.Visible = !isVisible;

            resetBirthdayWishInfo();
            resetSharedPhotosInfo();
        }

        private void resetSharedPhotosInfo()
        {
            bool isVisible = true;

            textBoxFirstName.Visible = !isVisible;
            textBoxLastName.Visible = !isVisible;
            buttonSearch.Visible = !isVisible;
            pictureBoxFriend.Visible = !isVisible;
            buttonImport.Visible = !isVisible;
            pictureBoxUser.Image = initial_image_picture;
        }

        private void resetBirthdayWishInfo()
        {
            bool isVisible = true;

            m_BirthdayWish = null;

            m_BirthdayWish = null;
            buttonSendBirthdayWish.Visible = !isVisible;
            textBoxWish.Visible = !isVisible;

            checkedListBoxWishes.Items.Clear();
        }
        #endregion Reset Methods


        private void postWishToFriends()   // we don't have authorization to post statuses
        {

            List<string> friends = GetFriendsListNamesFromCheckListBox(checkedListBoxWishes);

            string congrats = m_BirthdayWish.GenerateCongratulations(friends, textBoxWish.Text);

            m_Session.LoggedInUser.PostStatus(congrats);
        }

        private void updateCheckedListBoxWishes()
        {
            int  currentDay = m_BirthdayWish.CurrentDayOfYear;
            bool enabled    = true;
            
            BirthdayNode curNode = m_BirthdayWish.BirthdayDictionary.BirthdayFriends[currentDay];

            foreach (User friend in curNode.BirthdayFriends)
            {       
                checkedListBoxWishes.Items.Add(friend.Name, true);
            }

            if (checkedListBoxWishes.Items.Count != 0)
            {
                buttonSendBirthdayWish.Enabled = enabled;
                textBoxWish.Enabled  = enabled;
            }
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBoxWishes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRememberUser_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxFriend_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxUser_Click(object sender, EventArgs e)
        {

        }
        #endregion Private Methods

        private void panelUserDetails_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
