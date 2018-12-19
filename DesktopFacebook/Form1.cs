using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;
using static FeaturesLogic.BirthdayDictionary;
using static FacebookWrapper.FacebookService;
using static DesktopFacebook.Properties.Resources;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using static  DesktopFacebook.BirthdayWishUI;
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
        private readonly string       r_PhotosNotFound      = "Photos Not Found!";
        private readonly string       r_MsgFriendNotFound   = "Friend Not Found!";
        private readonly string       r_CaptionFriendSearch = "Friend Search Error";
        private readonly string       r_FirstName           = "First Name:";
        private readonly string       r_LastName            = "Last Name:";

        private Session               m_Session;
        private Memory            m_Memory;
        private SharedPhotos          m_SharedPhotos;
        //internal BirthdayWish         m_BirthdayWish;
        private SharedPhotosUI        m_SharedPhotosUI;
        private BirthdayWishUI        m_BirthdayWishUI;
        private FetchMaker            m_FetchMaker;

        #endregion Class Members

        #region Constructor

        public mainForm()
        {
            InitializeComponent();

            /* There is a problems with the serializer loading */
            //m_Memory = Serializer.LoadFromFile();  

            if (m_Memory != null &&
                m_Memory.RememberUser &&
                !string.IsNullOrEmpty(m_Memory.LastAccessToken))
            {
                Connect(m_Memory.LastAccessToken);

                m_Session.LoggedInUser = m_Memory.LastUser;
                m_BirthdayWishUI.BirthdayWish.BirthdayDictionary = m_Memory.LastUserBirthdayDictionary;
                //m_BirthdayWish.BirthdayDictionary = m_Memory.LastUserBirthdayDictionary;

                fetchUserInfo();
                fetchFeaturesInfo();
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
                m_Memory.LastUserBirthdayDictionary = m_BirthdayWishUI.BirthdayWish.BirthdayDictionary;
                //m_Memory.LastUserBirthdayDictionary = m_BirthdayWish.BirthdayDictionary;

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


            pictureBoxUser.ImageLocation =  loggedInUser.PictureNormalURL;
            Text                         =  string.Format("Logged in as {0} {1}", firstName, lastName);
            labelAccountName.Text        =  string.Format("{0} {1}", firstName, lastName);
            checkBoxRememberUser.Visible =  isVisible;
            buttonLogout.Visible         =  isVisible;
            buttonLogin.Visible          = !isVisible;
        }

        private void fetchFeaturesInfo()
        {
            User loggedInUser = m_Session.LoggedInUser;


            List<Control> birthdayWishControls = new List<Control>();
            birthdayWishControls.Add(buttonSendBirthdayWish);
            birthdayWishControls.Add(textBoxWish);
            birthdayWishControls.Add(checkedListBoxWishes);
            ;

            List<Control> sharedPhotosControls = new List<Control>();
            sharedPhotosControls.Add(textBoxFirstName);
            sharedPhotosControls.Add(textBoxLastName);
            sharedPhotosControls.Add(buttonSearch);
            sharedPhotosControls.Add(buttonDownload);
            sharedPhotosControls.Add(friendPictureBox);

            m_FetchMaker.FetchBirthdayWishUI(birthdayWishControls);
            m_FetchMaker.FetchSharedPhotosUI(sharedPhotosControls);

            m_BirthdayWishUI.BirthdayWish.BirthdayDictionary.FillBirthdays(loggedInUser);
            m_BirthdayWishUI.UpdateCheckedListBoxWishes(checkedListBoxWishes, textBoxWish, buttonSendBirthdayWish);

            m_SharedPhotos = new SharedPhotos();
        }

        #endregion Fetch Methods

        #region Event Handler Click Methods
        private void buttonLogin_Click(object sender, EventArgs e)  

        {
            m_Session = new Session();

            m_Session.StartSession();

            if (m_Session.IsSessionSuccess)
            {
                m_Memory     = new Memory();
                m_BirthdayWishUI = new BirthdayWishUI();
                m_SharedPhotosUI = new SharedPhotosUI();           
                m_FetchMaker     = new FetchMaker(m_BirthdayWishUI, m_SharedPhotosUI);

                checkBoxRememberUser.Checked = m_Memory.RememberUser;
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

            m_SharedPhotos.FindFriend(textBoxFirstName.Text, textBoxLastName.Text, m_Session.LoggedInUser);

            if (m_SharedPhotos.FriendWasFound)
            {
                User loggedInUser = m_Session.LoggedInUser;
                User friend       = m_SharedPhotos.Friend;

                m_SharedPhotos.ImportSharedPhotos(loggedInUser, friend);

                loadSharedPhotosToFlowLayoutPanel();
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




        private void loadSharedPhotosToFlowLayoutPanel()
        {
            bool isDownloadEnabled = true;

            if (m_SharedPhotos.SharedPhotosList.Count <= 0)
            {
                string message = string.Format(@"No photos of You and {0} were found",
                                                 m_SharedPhotos.Friend.Name);

                MessageBox.Show(message, r_PhotosNotFound,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User friend = m_SharedPhotos.Friend;

                List<Photo> sharedPhotos = m_SharedPhotos.SharedPhotosList;

                m_SharedPhotosUI = new SharedPhotosUI();

                m_SharedPhotosUI.ConvertPhotosToLazyPicBox(sharedPhotos);

                sharedPhotosBindingSource.DataSource = m_SharedPhotos; // DOR !!!
            
                buttonDownload.Enabled = isDownloadEnabled;


                foreach (LazyPictureBox pic in m_SharedPhotosUI.SharedLazyPictureBox)
                {
                    sharedPhotosFlowLayoutPanel.Controls.Add(pic);
                }
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
            textBoxLastName.Visible  = !isVisible;
            buttonSearch.Visible     = !isVisible;
            friendPictureBox.Visible = !isVisible;
            buttonDownload.Visible   = !isVisible;
            pictureBoxUser.Image     = initial_image_picture;
        }

        private void resetBirthdayWishInfo()
        {
            bool isVisible = true;

            m_BirthdayWishUI.BirthdayWish = null;

            buttonSendBirthdayWish.Visible = !isVisible;
            textBoxWish.Visible = !isVisible;

            checkedListBoxWishes.Items.Clear();
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

        private void sharedPhotosflowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
