using System;
using System.Collections.Generic;
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

        public List<LazyPictureBox>  SharedPicBox { get; set; }   

        
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

                /* There is a problem to save 'Memory' to XML file */
                m_Memory.SaveToFile();  
            }
        }
        #endregion Protected Override Methods

        #region Private Methods
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
            //DownLoadPhotos(m_SharedPhotosUI.SharedLazyPictureBox);
            DownLoadPhotos(SharedPicBox);
        }

     

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bool              isDownloadEnabled = true;
            SharedPhotosLogic sharedPhotosLogic = m_SharedPhotosUI.SharedPhotosLogic;

            sharedPhotosLogic.FindFriend(textBoxFirstName.Text, textBoxLastName.Text, m_Session.LoggedInUser);

            if (sharedPhotosLogic.FriendWasFound)
            {
                User loggedInUser = m_Session.LoggedInUser;
                User friend       = m_SharedPhotosUI.SharedPhotosLogic.Friend;

                sharedPhotosLogic.ImportSharedPhotos(loggedInUser, friend);

                SharedPicBox = m_SharedPhotosUI.ConvertPhotosToLazyPictureBoxes();


                foreach (LazyPictureBox pic in SharedPicBox)
                {
                    pic.LazyPicBoxClicked += lazyPictureBox_Clicked;

                }

                buttonDownload.Enabled = true;

                m_SharedPhotosUI.LoadSharedPhotosToFlowLayoutPanel(sharedPhotosFlowLayoutPanel, SharedPicBox);

                sharedPhotosLogicBindingSource.DataSource    = m_SharedPhotosUI.SharedPhotosLogic;

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



        internal void lazyPictureBox_Clicked(object sender, EventArgs e)
        {
            if (sender != null)
            {
                LazyPictureBox pic = sender as LazyPictureBox;
            
                if (pic.WasSelected)
                {
                    m_SharedPhotosUI.SharedPhotosLogic.TotalSelectedSharedPictures += 1;
                }
                else
                {
                    m_SharedPhotosUI.SharedPhotosLogic.TotalSelectedSharedPictures -= 1;
                }

                totalSelectedSharedPicturesLabel1.Text =
                    m_SharedPhotosUI.SharedPhotosLogic.TotalSelectedSharedPictures.ToString();
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
                m_FetchMaker.Reset();       // Resets the Birthday Logic and the Photos Logic
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
        #endregion Private Methods
    }
}
