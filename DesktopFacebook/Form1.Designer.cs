using System.Windows.Forms;

namespace DesktopFacebook
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label totalSelectedSharedPicturesLabel;
            this.sharedPhotosLogicBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelAccountName = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBirthday = new System.Windows.Forms.TabPage();
            this.checkedListBoxWishes = new System.Windows.Forms.CheckedListBox();
            this.textBoxWish = new System.Windows.Forms.TextBox();
            this.buttonSendBirthdayWish = new System.Windows.Forms.Button();
            this.tabPageSharedPhotos = new System.Windows.Forms.TabPage();
            this.friendPictureBox = new System.Windows.Forms.PictureBox();
            this.totalSelectedSharedPicturesLabel1 = new System.Windows.Forms.Label();
            this.sharedPhotosFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.checkBoxRememberUser = new System.Windows.Forms.CheckBox();
            this.pictureBoxUser = new System.Windows.Forms.PictureBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            totalSelectedSharedPicturesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sharedPhotosLogicBindingSource)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageBirthday.SuspendLayout();
            this.tabPageSharedPhotos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.friendPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).BeginInit();
            this.SuspendLayout();
            // 
            // totalSelectedSharedPicturesLabel
            // 
            totalSelectedSharedPicturesLabel.AutoSize = true;
            totalSelectedSharedPicturesLabel.Location = new System.Drawing.Point(6, 256);
            totalSelectedSharedPicturesLabel.Name = "totalSelectedSharedPicturesLabel";
            totalSelectedSharedPicturesLabel.Size = new System.Drawing.Size(208, 17);
            totalSelectedSharedPicturesLabel.TabIndex = 18;
            totalSelectedSharedPicturesLabel.Text = "Total Selected Shared Pictures:";
            // 
            // sharedPhotosLogicBindingSource
            // 
            this.sharedPhotosLogicBindingSource.DataSource = typeof(FeaturesLogic.SharedPhotosLogic);
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonLogin.Location = new System.Drawing.Point(25, 323);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(205, 42);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = " Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelAccountName
            // 
            this.labelAccountName.AutoSize = true;
            this.labelAccountName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAccountName.Location = new System.Drawing.Point(27, 9);
            this.labelAccountName.Name = "labelAccountName";
            this.labelAccountName.Size = new System.Drawing.Size(141, 25);
            this.labelAccountName.TabIndex = 2;
            this.labelAccountName.Text = "Account Name";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageBirthday);
            this.tabControl.Controls.Add(this.tabPageSharedPhotos);
            this.tabControl.Location = new System.Drawing.Point(258, 45);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(646, 398);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageBirthday
            // 
            this.tabPageBirthday.BackColor = System.Drawing.Color.LightGray;
            this.tabPageBirthday.Controls.Add(this.checkedListBoxWishes);
            this.tabPageBirthday.Controls.Add(this.textBoxWish);
            this.tabPageBirthday.Controls.Add(this.buttonSendBirthdayWish);
            this.tabPageBirthday.Location = new System.Drawing.Point(4, 25);
            this.tabPageBirthday.Name = "tabPageBirthday";
            this.tabPageBirthday.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBirthday.Size = new System.Drawing.Size(638, 369);
            this.tabPageBirthday.TabIndex = 0;
            this.tabPageBirthday.Text = "Birthday Wish";
            // 
            // checkedListBoxWishes
            // 
            this.checkedListBoxWishes.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.checkedListBoxWishes.BackColor = System.Drawing.SystemColors.Info;
            this.checkedListBoxWishes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxWishes.CheckOnClick = true;
            this.checkedListBoxWishes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkedListBoxWishes.FormattingEnabled = true;
            this.checkedListBoxWishes.Location = new System.Drawing.Point(3, 6);
            this.checkedListBoxWishes.Name = "checkedListBoxWishes";
            this.checkedListBoxWishes.ScrollAlwaysVisible = true;
            this.checkedListBoxWishes.Size = new System.Drawing.Size(649, 289);
            this.checkedListBoxWishes.TabIndex = 2;
            // 
            // textBoxWish
            // 
            this.textBoxWish.Enabled = false;
            this.textBoxWish.Location = new System.Drawing.Point(6, 306);
            this.textBoxWish.Multiline = true;
            this.textBoxWish.Name = "textBoxWish";
            this.textBoxWish.Size = new System.Drawing.Size(346, 52);
            this.textBoxWish.TabIndex = 1;
            this.textBoxWish.Text = "Wish Happy Birthday to Your Friends!";
            this.textBoxWish.Visible = false;
            // 
            // buttonSendBirthdayWish
            // 
            this.buttonSendBirthdayWish.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonSendBirthdayWish.Enabled = false;
            this.buttonSendBirthdayWish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonSendBirthdayWish.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSendBirthdayWish.Location = new System.Drawing.Point(355, 306);
            this.buttonSendBirthdayWish.Name = "buttonSendBirthdayWish";
            this.buttonSendBirthdayWish.Size = new System.Drawing.Size(300, 52);
            this.buttonSendBirthdayWish.TabIndex = 0;
            this.buttonSendBirthdayWish.Text = "SEND ";
            this.buttonSendBirthdayWish.UseVisualStyleBackColor = false;
            this.buttonSendBirthdayWish.Visible = false;
            this.buttonSendBirthdayWish.Click += new System.EventHandler(this.buttonSendBirthdayWish_Click);
            // 
            // tabPageSharedPhotos
            // 
            this.tabPageSharedPhotos.AutoScroll = true;
            this.tabPageSharedPhotos.BackColor = System.Drawing.Color.LightGray;
            this.tabPageSharedPhotos.Controls.Add(this.friendPictureBox);
            this.tabPageSharedPhotos.Controls.Add(totalSelectedSharedPicturesLabel);
            this.tabPageSharedPhotos.Controls.Add(this.totalSelectedSharedPicturesLabel1);
            this.tabPageSharedPhotos.Controls.Add(this.sharedPhotosFlowLayoutPanel);
            this.tabPageSharedPhotos.Controls.Add(this.buttonSearch);
            this.tabPageSharedPhotos.Controls.Add(this.textBoxLastName);
            this.tabPageSharedPhotos.Controls.Add(this.buttonDownload);
            this.tabPageSharedPhotos.Controls.Add(this.textBoxFirstName);
            this.tabPageSharedPhotos.Location = new System.Drawing.Point(4, 25);
            this.tabPageSharedPhotos.Name = "tabPageSharedPhotos";
            this.tabPageSharedPhotos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSharedPhotos.Size = new System.Drawing.Size(638, 369);
            this.tabPageSharedPhotos.TabIndex = 1;
            this.tabPageSharedPhotos.Text = "Import Shared Friend Photos";
            // 
            // friendPictureBox
            // 
            this.friendPictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.sharedPhotosLogicBindingSource, "Friend", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "picture not found!"));
            this.friendPictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.sharedPhotosLogicBindingSource, "Friend", true));
            this.friendPictureBox.Image = global::DesktopFacebook.Properties.Resources.initial_friend_image_picture;
            this.friendPictureBox.Location = new System.Drawing.Point(11, 47);
            this.friendPictureBox.Name = "friendPictureBox";
            this.friendPictureBox.Size = new System.Drawing.Size(231, 191);
            this.friendPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.friendPictureBox.TabIndex = 20;
            this.friendPictureBox.TabStop = false;
            // 
            // totalSelectedSharedPicturesLabel1
            // 
            this.totalSelectedSharedPicturesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sharedPhotosLogicBindingSource, "TotalSelectedSharedPictures", true));
            this.totalSelectedSharedPicturesLabel1.Location = new System.Drawing.Point(220, 256);
            this.totalSelectedSharedPicturesLabel1.Name = "totalSelectedSharedPicturesLabel1";
            this.totalSelectedSharedPicturesLabel1.Size = new System.Drawing.Size(58, 23);
            this.totalSelectedSharedPicturesLabel1.TabIndex = 19;
            this.totalSelectedSharedPicturesLabel1.Text = "0";
            // 
            // sharedPhotosFlowLayoutPanel
            // 
            this.sharedPhotosFlowLayoutPanel.AutoScroll = true;
            this.sharedPhotosFlowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.sharedPhotosFlowLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sharedPhotosFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sharedPhotosFlowLayoutPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.sharedPhotosFlowLayoutPanel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sharedPhotosFlowLayoutPanel.Location = new System.Drawing.Point(311, 47);
            this.sharedPhotosFlowLayoutPanel.Name = "sharedPhotosFlowLayoutPanel";
            this.sharedPhotosFlowLayoutPanel.Size = new System.Drawing.Size(293, 288);
            this.sharedPhotosFlowLayoutPanel.TabIndex = 16;
            this.sharedPhotosFlowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sharedPhotosFlowLayoutPanel_Paint);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(314, 7);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(293, 34);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxLastName.Location = new System.Drawing.Point(149, 7);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(108, 22);
            this.textBoxLastName.TabIndex = 4;
            this.textBoxLastName.Text = "Last Name:";
            this.textBoxLastName.Click += new System.EventHandler(this.textBoxLastName_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Enabled = false;
            this.buttonDownload.Location = new System.Drawing.Point(10, 292);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(247, 43);
            this.buttonDownload.TabIndex = 2;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.DownLoad_Click);
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxFirstName.Location = new System.Drawing.Point(11, 6);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(112, 22);
            this.textBoxFirstName.TabIndex = 0;
            this.textBoxFirstName.Text = "First Name:";
            this.textBoxFirstName.Click += new System.EventHandler(this.textBoxFirstName_Click);
            // 
            // checkBoxRememberUser
            // 
            this.checkBoxRememberUser.AutoSize = true;
            this.checkBoxRememberUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxRememberUser.Location = new System.Drawing.Point(25, 267);
            this.checkBoxRememberUser.Name = "checkBoxRememberUser";
            this.checkBoxRememberUser.Size = new System.Drawing.Size(97, 17);
            this.checkBoxRememberUser.TabIndex = 3;
            this.checkBoxRememberUser.Text = "Remember Me";
            this.checkBoxRememberUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRememberUser.UseVisualStyleBackColor = true;
            this.checkBoxRememberUser.Visible = false;
            // 
            // pictureBoxUser
            // 
            this.pictureBoxUser.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxUser.Image = global::DesktopFacebook.Properties.Resources.initial_image_picture;
            this.pictureBoxUser.InitialImage = null;
            this.pictureBoxUser.Location = new System.Drawing.Point(25, 45);
            this.pictureBoxUser.Name = "pictureBoxUser";
            this.pictureBoxUser.Size = new System.Drawing.Size(205, 207);
            this.pictureBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUser.TabIndex = 1;
            this.pictureBoxUser.TabStop = false;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(26, 386);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonLogout.Size = new System.Drawing.Size(204, 42);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Visible = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(936, 467);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.checkBoxRememberUser);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelAccountName);
            this.Controls.Add(this.pictureBoxUser);
            this.Controls.Add(this.buttonLogin);
            this.Name = "mainForm";
            this.Text = "Desktop Facebook";
            ((System.ComponentModel.ISupportInitialize)(this.sharedPhotosLogicBindingSource)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageBirthday.ResumeLayout(false);
            this.tabPageBirthday.PerformLayout();
            this.tabPageSharedPhotos.ResumeLayout(false);
            this.tabPageSharedPhotos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.friendPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.PictureBox pictureBoxUser;
        private System.Windows.Forms.Label labelAccountName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBirthday;
        private System.Windows.Forms.TextBox textBoxWish;
        private System.Windows.Forms.Button buttonSendBirthdayWish;
        private System.Windows.Forms.CheckedListBox checkedListBoxWishes;
        private System.Windows.Forms.CheckBox checkBoxRememberUser;
        private System.Windows.Forms.Button buttonLogout;
        private TabPage tabPageSharedPhotos;
        private BindingSource sharedPhotosLogicBindingSource;
        private FlowLayoutPanel sharedPhotosFlowLayoutPanel;
        private Button buttonSearch;
        private TextBox textBoxLastName;
        private Button buttonDownload;
        private TextBox textBoxFirstName;
        private Label totalSelectedSharedPicturesLabel1;
        private PictureBox friendPictureBox;
    }
}

