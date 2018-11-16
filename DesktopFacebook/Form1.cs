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
using Message = FacebookWrapper.ObjectModel.Message;
using System.Net.Mail;
using static DesktopFacebook.CheckBoxFriend;

namespace DesktopFacebook
{

    public partial class mainForm : Form
    {
        public LoginResult  m_LoginResult;
        public User         m_LoggedInUser;
        public AppSettings  m_AppSettings;
        public BirthdayWish m_BirthdayWish;

        public bool         m_IsFirstLogin = true;

        public readonly int r_CurrentDayOfYear = DateTime.Now.DayOfYear - 1;



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
                //"1450160541956417",
                "friends_birthday",
                "user_friends",
                "user_photos",
                "public_profile",
                "user_posts");

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

            updatecheckedListBoxWishes(m_BirthdayWish);

            pictureBoxUser.ImageLocation = m_LoggedInUser.PictureNormalURL;

            Text = "Logged in as " + m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;

            labelAccountName.Text = m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName;        

            buttonLogin.Text = "Log out"; 

            

            

        }

        private void updatecheckedListBoxWishes(BirthdayWish i_BirthdayWish)
        {

            //BirthdayNode curNode = i_BirthdayWish.BirthdayFriends[r_CurrentDayOfYear];

            BirthdayNode curNode = i_BirthdayWish.BirthdayFriends[98];

            /*
            for (int i = 0; i < 50; i++)
            {
                CheckBox checkk = new CheckBox();
                checkk.Text = "DOR KOREN";
                checkk.Checked = true;
                checkedListBoxWishes.Items.Add(checkk.Text, true);
            } */

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
            sendWishToFriends();
        }

        private void sendWishToFriends()
        {
            foreach (string friendName in checkedListBoxWishes.CheckedItems)
            {
                User user = m_BirthdayWish.FindUserInCurrentDay(friendName, r_CurrentDayOfYear);
                sendWishToFriend(user);              
            }
        }

        private void sendWishToFriend(User i_Friend)
        {

            m_LoggedInUser.PostStatus("MAZALTOV@@@!!!");


            /*
               User currentFriend = check.Friend;
               try
               {
                   MailMessage mail = new MailMessage();
                   SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                   mail.From = new MailAddress("dorkoren10@gmail.com"); // need to fix
                   mail.To.Add(currentFriend.Email);
                   mail.Subject = "Happy Birthday!";
                   mail.Body = "MAZAL TOV YA BEN SHARMUTA!";

                   SmtpServer.Port = 587;
                   SmtpServer.Credentials = new System.Net.NetworkCredential("Dor Koren", "Q2e4T6u8O0"); // ask balmas!
                   SmtpServer.EnableSsl = true;

                   SmtpServer.Send(mail);
                   MessageBox.Show("mail Send!");
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
               */
        }
    }
}
