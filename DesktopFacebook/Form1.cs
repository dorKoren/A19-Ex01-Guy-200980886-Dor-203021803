using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;
using static FacebookWrapper.FacebookService;
using static DesktopFacebook.Properties.Resources;

using static DesktopFacebook.SharedPhotosUI;

namespace DesktopFacebook
{
    public partial class mainForm : Form
    {
        #region Class Members

        private readonly string       r_LogOutMsg           = "Are you sure that you want logout?";
        private readonly string       r_LabelAccountName    = "Account Name";
        private readonly string       r_MainFormName        = "Desktop Facebook";
        private readonly string       r_CaptionLogOut       = "Logout";
        private readonly string       r_MsgFriendNotFound   = "Friend Not Found!";
        private readonly string       r_CaptionFriendSearch = "Friend Search Error";
        private readonly string       r_FirstName           = "First Name:";
        private readonly string       r_LastName            = "Last Name:";

        private Session               m_Session;
        private Memory                m_Memory;
        private SharedPhotosUI        m_SharedPhotosUI;
        private BirthdayWishUI        m_BirthdayWishUI;
        private FetchMaker            m_FetchMaker;

        #endregion Class Members

        #region Constructor

        public mainForm()
        {
            InitializeComponent();

            m_Memory = Memory.LoadFromFile();  

            if (m_Memory != null &&
                m_Memory.RememberUser &&
                !string.IsNullOrEmpty(m_Memory.LastAccessToken))
            {
                Connect(m_Memory.LastAccessToken);

                m_Session.LoggedInUser = m_Memory.LastUser;
                m_BirthdayWishUI.BirthdayWishLogic.BirthdayDictionary = m_Memory.LastUserBirthdayDictionary;

                fetchUserInfo();
                fetchFeaturesInfo();
            }
            else
            {
                m_Memory = Memory.CreateNewMemory();
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
                m_Memory.RememberUser = checkBoxRememberUser.Checked;
                m_Memory.LastUserBirthdayDictionary = m_BirthdayWishUI.BirthdayWishLogic.BirthdayDictionary;

                m_Memory.LastAccessToken = m_Memory.RememberUser ?
                    m_Session.LoginResult.AccessToken : null;

                /* There is a problem to save 'Facebook data' to XML file */
                //m_Memory.SaveToFile();  
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

            checkBoxRememberUser.Checked =  m_Memory.RememberUser;
            pictureBoxUser.ImageLocation =  loggedInUser.PictureNormalURL;
            Text                         =  string.Format("Logged in as {0} {1}", firstName, lastName);
            labelAccountName.Text        =  string.Format("{0} {1}", firstName, lastName);
            checkBoxRememberUser.Visible =  isVisible;
            buttonLogout.Visible         =  isVisible;
            buttonLogin.Visible          = !isVisible;
        }

        private void fetchFeaturesInfo()
        {
            m_FetchMaker.FetchInitBirthdayWishUI(tabPageBirthday);
            m_FetchMaker.FetchInitSharedPhotosUI(tabPageSharedPhotos);

            m_BirthdayWishUI.BirthdayWishLogic.BirthdayDictionary.FillBirthdays(m_Session.LoggedInUser);
            m_BirthdayWishUI.UpdateCheckedListBoxWishes(checkedListBoxWishes, textBoxWish, buttonSendBirthdayWish);
        }

        #endregion Fetch Methods

        #region Event Handler Click Methods
        private void buttonLogin_Click(object sender, EventArgs e)  

        {
            m_Session = new Session();

            m_Session.StartSession();

            if (m_Session.IsSessionSuccess)
            {
                m_BirthdayWishUI = new BirthdayWishUI();
                m_SharedPhotosUI = new SharedPhotosUI();           
                m_FetchMaker     = new FetchMaker(m_BirthdayWishUI, m_SharedPhotosUI);

                fetchUserInfo();
                fetchFeaturesInfo();
            }
            else
            {
                MessageBox.Show(m_Session.LoginResult.ErrorMessage);
            }
        }

        private void buttonSendBirthdayWish_Click(object sender, EventArgs e)
        {
            m_BirthdayWishUI.PostWishToFriends(checkedListBoxWishes, textBoxWish, m_Session.LoggedInUser);
        }

        private void DownLoad_Click(object sender, EventArgs e)
        {
           DownLoadPhotos(m_SharedPhotosUI.SharedLazyPictureBox);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bool isDownloadEnabled = true;
            SharedPhotosLogic sharedPhotosLogic = m_SharedPhotosUI.SharedPhotosLogic;

            sharedPhotosLogic.FindFriend(textBoxFirstName.Text, textBoxLastName.Text, m_Session.LoggedInUser);

            if (sharedPhotosLogic.FriendWasFound)
            {
                User loggedInUser = m_Session.LoggedInUser;
                User friend       = m_SharedPhotosUI.SharedPhotosLogic.Friend;

                sharedPhotosLogic.ImportSharedPhotos(loggedInUser, friend);

                m_SharedPhotosUI.LoadSharedPhotosToFlowLayoutPanel(sharedPhotosFlowLayoutPanel, buttonDownload);

                sharedPhotosLogicBindingSource.DataSource    = m_SharedPhotosUI.SharedPhotosLogic;
                //sharedLazyPictureBoxBindingSource.DataSource = m_SharedPhotosUI.SharedLazyPictureBox;

            }
            else
            {
                MessageBox.Show(r_MsgFriendNotFound, r_CaptionFriendSearch,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxFirstName.Text  = r_FirstName;
                textBoxLastName.Text   = r_LastName;
                buttonDownload.Enabled = !isDownloadEnabled;

                friendPictureBox.Image = initial_friend_image_picture;
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
                m_FetchMaker.FetchResetBirthdayWishUI(tabPageBirthday);
                m_FetchMaker.FetchResetSharedPhotosUI(tabPageSharedPhotos);
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
            pictureBoxUser.Image = initial_image_picture;
        }


        #endregion Reset Method

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

        private void pictureBoxUser_Click(object sender, EventArgs e)
        {

        }
     

        #endregion Private Methods


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void totalSelectedSharedPicturesLabel3_Click(object sender, EventArgs e)
        {

        }
        
        private void sharedPhotosListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
                
        }

        private void friendPictureBox_Click_1(object sender, EventArgs e)
        {

        }

        private void totalSelectedSharedPicturesLabel4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
