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
            this.buttonLogin = new System.Windows.Forms.Button();
            this.pictureBoxUser = new System.Windows.Forms.PictureBox();
            this.labelAccountName = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkedListBoxWishes = new System.Windows.Forms.CheckedListBox();
            this.textBoxWish = new System.Windows.Forms.TextBox();
            this.buttonSendBirthdayWish = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxRememberUser = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLogin.Font = new System.Drawing.Font("BN Loco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonLogin.Location = new System.Drawing.Point(25, 270);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(205, 42);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // pictureBoxUser
            // 
            this.pictureBoxUser.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxUser.Image = global::DesktopFacebook.Properties.Resources.initial_image_picture;
            this.pictureBoxUser.Location = new System.Drawing.Point(25, 45);
            this.pictureBoxUser.Name = "pictureBoxUser";
            this.pictureBoxUser.Size = new System.Drawing.Size(205, 207);
            this.pictureBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUser.TabIndex = 1;
            this.pictureBoxUser.TabStop = false;
            // 
            // labelAccountName
            // 
            this.labelAccountName.AutoSize = true;
            this.labelAccountName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelAccountName.Font = new System.Drawing.Font("BN Loco", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAccountName.Location = new System.Drawing.Point(21, 9);
            this.labelAccountName.Name = "labelAccountName";
            this.labelAccountName.Size = new System.Drawing.Size(171, 24);
            this.labelAccountName.TabIndex = 2;
            this.labelAccountName.Text = "Account Name";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(258, 45);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(530, 393);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.checkedListBoxWishes);
            this.tabPage1.Controls.Add(this.textBoxWish);
            this.tabPage1.Controls.Add(this.buttonSendBirthdayWish);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(522, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Birthday Wish";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // checkedListBoxWishes
            // 
            this.checkedListBoxWishes.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.checkedListBoxWishes.BackColor = System.Drawing.SystemColors.Info;
            this.checkedListBoxWishes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxWishes.CheckOnClick = true;
            this.checkedListBoxWishes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkedListBoxWishes.FormattingEnabled = true;
            this.checkedListBoxWishes.Location = new System.Drawing.Point(6, 3);
            this.checkedListBoxWishes.Name = "checkedListBoxWishes";
            this.checkedListBoxWishes.ScrollAlwaysVisible = true;
            this.checkedListBoxWishes.Size = new System.Drawing.Size(510, 289);
            this.checkedListBoxWishes.TabIndex = 2;
            this.checkedListBoxWishes.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxWishes_SelectedIndexChanged);
            // 
            // textBoxWish
            // 
            this.textBoxWish.Location = new System.Drawing.Point(6, 306);
            this.textBoxWish.Multiline = true;
            this.textBoxWish.Name = "textBoxWish";
            this.textBoxWish.Size = new System.Drawing.Size(346, 52);
            this.textBoxWish.TabIndex = 1;
            this.textBoxWish.Text = "Wish Happy Birthday to Your Friends!";
            this.textBoxWish.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonSendBirthdayWish
            // 
            this.buttonSendBirthdayWish.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonSendBirthdayWish.Font = new System.Drawing.Font("BN Loco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonSendBirthdayWish.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSendBirthdayWish.Location = new System.Drawing.Point(355, 306);
            this.buttonSendBirthdayWish.Name = "buttonSendBirthdayWish";
            this.buttonSendBirthdayWish.Size = new System.Drawing.Size(161, 52);
            this.buttonSendBirthdayWish.TabIndex = 0;
            this.buttonSendBirthdayWish.Text = "SEND ";
            this.buttonSendBirthdayWish.UseVisualStyleBackColor = false;
            this.buttonSendBirthdayWish.Click += new System.EventHandler(this.buttonSendBirthdayWish_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(522, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import Shared Friend Photos";
            // 
            // checkBoxRememberUser
            // 
            this.checkBoxRememberUser.AutoSize = true;
            this.checkBoxRememberUser.Font = new System.Drawing.Font("BN Loco", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxRememberUser.Location = new System.Drawing.Point(25, 318);
            this.checkBoxRememberUser.Name = "checkBoxRememberUser";
            this.checkBoxRememberUser.Size = new System.Drawing.Size(126, 17);
            this.checkBoxRememberUser.TabIndex = 3;
            this.checkBoxRememberUser.Text = "Remember Me";
            this.checkBoxRememberUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRememberUser.UseVisualStyleBackColor = true;
            this.checkBoxRememberUser.CheckedChanged += new System.EventHandler(this.checkBoxRememberUser_CheckedChanged);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxRememberUser);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelAccountName);
            this.Controls.Add(this.pictureBoxUser);
            this.Controls.Add(this.buttonLogin);
            this.Name = "mainForm";
            this.Text = "Desktop Facebook";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.PictureBox pictureBoxUser;
        private System.Windows.Forms.Label labelAccountName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxWish;
        private System.Windows.Forms.Button buttonSendBirthdayWish;
        private System.Windows.Forms.CheckedListBox checkedListBoxWishes;
        private System.Windows.Forms.CheckBox checkBoxRememberUser;
    }
}

