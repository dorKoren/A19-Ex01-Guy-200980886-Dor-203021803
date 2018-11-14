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
using static DesktopFacebook.BirthdayWish;
using static DesktopFacebook.AppSettings;

namespace DesktopFacebook
{

    public partial class mainForm : Form
    {
        public LoginResult  m_LoginResult;
        public User         m_LoggedInUser;
        public AppSettings  m_AppSettings;
        public BirthdayWish m_BirthdayWish;

        public bool        m_IsFirstLogin = true;
        
        public mainForm()
        {
            InitializeComponent();

            // This is the first time this (or ANY?) User login to the app
            if (m_IsFirstLogin)
            {
                m_AppSettings = new AppSettings();
                m_AppSettings.RememberUser = false;
                m_IsFirstLogin = false;
            }

            // This User is the last user to login
            else
            {
                m_AppSettings = LoadFromFile();
                checkBoxRememberUser.Checked = m_AppSettings.RememberUser;          
            }

            if (m_AppSettings.RememberUser &&
                    !string.IsNullOrEmpty(m_AppSettings.LastAccessToken))
            {
                Connect(m_AppSettings.LastAccessToken);
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
            m_LoginResult = Login(     // DOR maybe we should create session class for the login & connect... 
                "2121776861417547",
                //"friends_birthday",
                "user_friends",
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

            m_BirthdayWish = new BirthdayWish();
            m_BirthdayWish.FillBirthdays(m_LoggedInUser);

            upfatecheckedListBoxWishes(m_BirthdayWish);

            pictureBoxUser.ImageLocation = m_LoggedInUser.PictureNormalURL;

            Text = "Logged in as " + m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;

            labelAccountName.Text = m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;        

            buttonLogin.Text = "Log out"; 

            

            

        }

        private void upfatecheckedListBoxWishes(BirthdayWish i_BirthdayWish)
        {
            BirthdayNode curNode = i_BirthdayWish.BirthdayFriends[DateTime.Now.DayOfYear];

            for (int i = 0; i < 10; i++)
            {
                CheckBox checkk = new CheckBox();
                checkk.Text = "DOR KOREN";
                checkk.Checked = true;
                checkedListBoxWishes.Items.Add(checkk);
            }

            

            foreach (User friend in curNode.BirthdayFriends)
            {
                CheckBox check = new CheckBox();
                check.Text = friend.Name + friend.LastName;
                check.Checked = true;
                checkedListBoxWishes.Items.Add(check);

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
    }
}
