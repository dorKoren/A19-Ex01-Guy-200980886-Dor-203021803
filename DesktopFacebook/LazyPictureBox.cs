using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopFacebook
{
    public class LazyPictureBox : PictureBox
    {
        private static int s_Top = 3;

        public string URL         { get; set; }
        public bool   WasSelected { get; set; }

        public LazyPictureBox()
        {
            this.Size = new Size(60, 60);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Top = s_Top;
            this.Left = 3;
            
            s_Top = this.Bottom + 2;
            WasSelected = true;
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
    }
}
