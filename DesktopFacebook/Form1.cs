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

namespace DesktopFacebook
{
    public partial class mainForm : Form
    {
        private LoginResult m_LoginResult { get; set; }
        private FacebookWrapper.ObjectModel.User m_LoggetInUser;


        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_LoginResult = FacebookService.Login(
                "2121776861417547",
                "user_birthday",
                "user_photos");

            m_LoggetInUser = m_LoginResult.LoggedInUser;
            this.Text = "Logged in as " + m_LoggetInUser.FirstName + " " + m_LoggetInUser.LastName;
            labelAccountName.Text = m_LoggetInUser.FirstName + " " + m_LoggetInUser.LastName;
            pictureBoxUser.ImageLocation = m_LoggetInUser.PictureNormalURL;
            buttonLogin.Text = "Log out";
        }
    }
}
