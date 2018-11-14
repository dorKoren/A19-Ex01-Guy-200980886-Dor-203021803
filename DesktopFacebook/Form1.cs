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
using static DesktopFacebook.BirthdayWish;
using static DesktopFacebook.AppSettings;

namespace DesktopFacebook
{

    public partial class mainForm : Form
    {
        public LoginResult m_LoginResult;
        public User        m_LoggedInUser;
        public AppSettings m_AppSettings;
        private bool       m_IsFirstLogin = true;
        
        public mainForm()
        {
            InitializeComponent();

            m_AppSettings = AppSettings.LoadFromFile();

            checkBoxRememberUser.Checked = m_AppSettings.RememberUser;

            if (m_AppSettings.RememberUser &&
                    !string.IsNullOrEmpty(m_AppSettings.LastAccessToken))
            {
                FacebookService.Connect(m_AppSettings.LastAccessToken);
                fetchUserInfo();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            m_AppSettings.RememberUser    = checkBoxRememberUser.Checked;
            m_AppSettings.LastAccessToken = m_LoginResult.AccessToken;
            //AppSettings.LastUserBirthdayDictionary = checkedListBoxWishes.Items;  // DOR - ask balmas.

            if (m_AppSettings.RememberUser)
            {
                m_AppSettings.LastAccessToken = m_LoginResult.AccessToken;
            }
            else
            {
                m_AppSettings.LastAccessToken = null;
            }

            m_AppSettings.SaveToFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loginAndInit();      
        }

        private void loginAndInit()
        {
            m_LoginResult = FacebookService.Login(     // DOR maybe we should create session class for the login & connect... 
                "2121776861417547",
                "user_birthday",
                "user_photos");

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                fetchUserInfo();
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage);
            }
        }

        private void fetchUserInfo()
        {
            m_LoggedInUser = m_LoginResult.LoggedInUser;
            Text = "Logged in as " + m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;
            labelAccountName.Text = m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;
            pictureBoxUser.ImageLocation = m_LoggedInUser.PictureNormalURL;
            buttonLogin.Text = "Log out";          
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
    }
}
