using FacebookWrapper.ObjectModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopFacebook
{

    public class LazyPictureBox : PictureBox
    {
        public event EventHandler LazyPicBoxClicked;

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

        public static LazyPictureBox ConvertPhotoToLazyPicBox(Photo i_Photo)
        {
            LazyPictureBox lazyPicBox = new LazyPictureBox(); 

            lazyPicBox.Load(i_Photo.PictureNormalURL);
            lazyPicBox.ImageLocation = i_Photo.PictureNormalURL;
       
            return lazyPicBox;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (base.ImageLocation == null)
            {
                base.ImageLocation = this.URL;
            }

            base.OnPaint(pe);
        }

        protected override void OnClick(EventArgs e)
        {
            // Select current image
            if (!this.WasSelected)
            {
                selectPic(this);
            }
            // Deselect current image
            else
            {
                deSelectPic(this);
            }

            notifyClickedObservers(e);

            base.OnClick(e);
        }

        private void notifyClickedObservers(EventArgs e)
        {
            LazyPicBoxClicked?.Invoke(this, e);
        }

        private void deSelectPic(LazyPictureBox i_Picture)
        {
            i_Picture.Height -= 20;
            i_Picture.Width  -= 20;
            i_Picture.WasSelected = false;
        }

        private void selectPic(LazyPictureBox i_Picture)
        {
            i_Picture.Height += 20;
            i_Picture.Width  += 20;
            i_Picture.WasSelected = true;
        }
    }
}
