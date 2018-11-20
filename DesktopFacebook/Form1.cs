using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using static FacebookWrapper.FacebookService;
using static DesktopFacebook.BirthdayDictionary;
using static DesktopFacebook.AppSettings;
using Message = FacebookWrapper.ObjectModel.Message;
using System.Net.Mail;
using static DesktopFacebook.CheckBoxFriend;
using static DesktopFacebook.Session;
using static DesktopFacebook.BirthdayWishSettings;
using static DesktopFacebook.Properties.Resources;

namespace DesktopFacebook
{
    public partial class mainForm : Form
    {
        internal Session              m_Session;        
        internal AppSettings          m_AppSettings;
        internal SharedPhotosSettings m_SharedPhotos;
        internal BirthdayWishSettings m_BirthdayWish;
        private  bool                 m_RememberUser = true;

        public mainForm()
        {
            InitializeComponent();

            m_Session = new Session(this);
            //m_AppSettings = LoadFromFile();   // DOR  steel need to handle with this!!!!!

            if (m_AppSettings != null && m_AppSettings.RememberUser &&
                    !string.IsNullOrEmpty(m_AppSettings.LastAccessToken))
            {
                Connect(m_AppSettings.LastAccessToken);
                FetchUserInfo();
            }
            else
            {
                m_AppSettings                = new AppSettings();
                m_SharedPhotos               = new SharedPhotosSettings();
                m_BirthdayWish               = new BirthdayWishSettings();
                m_AppSettings.RememberUser   = !m_RememberUser;
                m_Session.Login();

                checkBoxRememberUser.Checked = m_AppSettings.RememberUser;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            m_AppSettings.RememberUser = checkBoxRememberUser.Checked;
            m_AppSettings.LastAccessToken = m_Session.LoginResult.AccessToken;
            m_AppSettings.LastUserBirthdayDictionary = m_BirthdayWish.BirthdayDictionary; 

            m_AppSettings.LastAccessToken = m_AppSettings.RememberUser ?
                                            m_Session.LoginResult.AccessToken : null;

            //m_AppSettings.SaveToFile();   // DOR  steel need to handle with this!!!!!
        }

        public void FetchUserInfo()
        {
            bool v_IsVisible = true;

            User loggedInUser = m_Session.LoggedInUser;

            m_BirthdayWish.BirthdayDictionary.FillBirthdays(loggedInUser);

            updateCheckedListBoxWishes();

            buttonSendBirthdayWish.Visible = v_IsVisible;

            pictureBoxUser.ImageLocation = loggedInUser.PictureNormalURL;

            Text = "Logged in as " + loggedInUser.FirstName + " " + loggedInUser.LastName;

            labelAccountName.Text = loggedInUser.FirstName + " " + loggedInUser.LastName;

            buttonLogin.Visible = !v_IsVisible;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_Session.Login();   
        }

        private void updateCheckedListBoxWishes()
        {
            int currentDay = m_BirthdayWish.CurrentDayOfYear;
            
            //BirthdayNode curNode = m_BirthdayWish.BirthdayDictionary.BirthdayFriends[currentDay];

            BirthdayNode curNode = m_BirthdayWish.BirthdayDictionary.BirthdayFriends[98];

            foreach (User friend in curNode.BirthdayFriends)
            {
                CheckBoxFriend check = new CheckBoxFriend(friend);

                checkedListBoxWishes.Items.Add(check.Name, true);
            }
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

        private void buttonSendBirthdayWish_Click(object sender, EventArgs e)
        {
            postWishToFriends();
        }

        private void postWishToFriends()   // we don't have authorization to post statuses
        {
            string congrats = m_BirthdayWish.GenerateCongratulations(
                                  checkedListBoxWishes.CheckedItems,
                                  textBoxWish.Text);

            m_Session.LoggedInUser.PostStatus(congrats);
        }

              
        private void textBox_TextChanged(object sender, EventArgs e)
        {
        
        }
     
        private void pictureBoxFriend_Click(object sender, EventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            User loggedInUser = m_Session.LoggedInUser;
            User friend       = m_SharedPhotos.Friend;

            bool imported = m_SharedPhotos.ImportSharedPhotos(loggedInUser, friend);
            string message;

            if (!imported)
            {
                message = string.Format(@"No photos of You and {0} were found",
                                          m_SharedPhotos.Friend.Name);

                MessageBox.Show(message, "Photos Not Found!", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                message = string.Format(@"photos of You and {0} were imported!",
                                          m_SharedPhotos.Friend.Name);

                MessageBox.Show(message, "Imported Successfuly!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Friend Not Found!", "Friend Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxFirstName.Text = "";
                textBoxLastName.Text = "";
                buttonImport.Enabled = !isImportEnabled;
                pictureBoxFriend.Image = initial_friend_image_picture;


            }
        }
    }
}
