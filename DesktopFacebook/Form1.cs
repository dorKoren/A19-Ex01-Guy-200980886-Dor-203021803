﻿using System;
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
        private Serializer            m_Serializer;
        private SharedPhotosUI        m_SharedPhotosUI;
        private BirthdayWishUI        m_BirthdayWishUI;
        private FetchMaker            m_FetchMaker;

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
                m_BirthdayWishUI.BirthdayWishLogic.BirthdayDictionary = m_Serializer.LastUserBirthdayDictionary;

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
                m_Serializer.RememberUser = checkBoxRememberUser.Checked;
                m_Serializer.LastUserBirthdayDictionary = m_BirthdayWishUI.BirthdayWishLogic.BirthdayDictionary;

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

            checkBoxRememberUser.Checked =  m_Serializer.RememberUser;
            pictureBoxUser.ImageLocation =  loggedInUser.PictureNormalURL;
            Text                         =  string.Format("Logged in as {0} {1}", firstName, lastName);
            labelAccountName.Text        =  string.Format("{0} {1}", firstName, lastName);
            checkBoxRememberUser.Visible =  isVisible;
            buttonLogout.Visible         =  isVisible;
            buttonLogin.Visible          = !isVisible;
        }

        private void fetchFeaturesInfo()
        {
            m_FetchMaker.FetchInitBirthdayWishUI(this);
            m_FetchMaker.FetchInitSharedPhotosUI(this);

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
                m_Serializer     = new Serializer();
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

                sharedPhotosLogicBindingSource.DataSource = m_SharedPhotosUI.SharedPhotosLogic;

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
                m_FetchMaker.FetchResetBirthdayWishUI(this);
                m_FetchMaker.FetchResetSharedPhotosUI(this);
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
