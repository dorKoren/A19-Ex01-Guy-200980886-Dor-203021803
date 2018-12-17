using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopFacebook
{
    public class LazyPictureBox : PictureBox
    {
        private static int s_Top = 3;

        public string URL { get; set; }
        public bool WasSelected { get; set; }

        public LazyPictureBox()
        {
            this.Size = new Size(60, 60);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Top = s_Top;
            this.Left = 3;
            this.Click += lazyPicBox_Click;

            s_Top = this.Bottom + 2;
            WasSelected = false;
        }

        public new void Load(string i_UrlToLoad)
        {
            URL = i_UrlToLoad;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (base.ImageLocation == null)
            {
                base.ImageLocation = this.URL;
            }

            base.OnPaint(pe);
        }

        private static void lazyPicBox_Click(object sender, EventArgs e) // DOR ???
        {
            LazyPictureBox pic = sender as LazyPictureBox;
           
            // Select current image
            if (!pic.WasSelected)
            {
                selectPic(pic);
            }
            // Deselect current image
            else
            {
                deSelectPic(pic);
            }

        }

        private static void deSelectPic(LazyPictureBox i_Picture)
        {
            //pic.BackColor = Color.Transparent;
            i_Picture.Show();
            i_Picture.WasSelected = false;
            //m_SharedPhotos.TotalSelectedSharedPictures--;

        }

        private static void selectPic(LazyPictureBox i_Picture)
        {
            //pic.BackColor = Color.Beige;
            i_Picture.Hide();
            i_Picture.WasSelected = true;
            //m_SharedPhotos.TotalSelectedSharedPictures++;
        }
    }
}
