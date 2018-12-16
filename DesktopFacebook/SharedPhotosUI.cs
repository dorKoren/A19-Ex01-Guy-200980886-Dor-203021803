using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal static class SharedPhotosUI
    {
        #region Class Members
        private const string k_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
        #endregion Class Members

        #region Internal Static Methods


        internal static void FetchSharedPhotosToListBox(FlowLayoutPanel i_SharedPhotosflowLayoutPanel, List<Photo> i_sharedPhotos)
        {
            int top = 3;
            foreach (Photo sharedPhoto in i_sharedPhotos)
            {
                LazyPictureBox sharedPicture = new LazyPictureBox();
                sharedPicture.Size = new Size(75, 75);
                sharedPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                sharedPicture.Load(sharedPhoto.PictureThumbURL);
                sharedPicture.Top = top;
                sharedPicture.Left = 3;
                top = sharedPicture.Bottom + 2;

                i_SharedPhotosflowLayoutPanel.Controls.Add(sharedPicture);
            }
        }

        /**************************************************************/
        public class LazyPictureBox : PictureBox
        {
            public string URL { get; set; }
            public bool WasSelected { get; set; }

            public LazyPictureBox() {

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
        }
        /**************************************************************/






        internal static void DownLoadPhotos(List<Photo> i_SharedPhotos)   // DOR: need to handle with list of picture box
        {
            foreach (Photo photo in i_SharedPhotos)
            {
                downLoadPhoto(photo);
            }
        }





        #endregion Internal Static Methods

        #region  Private Static Methods   
        private static void downLoadPhoto(Photo i_Photo)
        {

            SaveFileDialog file = new SaveFileDialog
            {
                Filter = k_Filter,
                FileName = i_Photo.Id,
                AddExtension = true
            };

            using (FileStream stream = (FileStream)file.OpenFile())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(file.FileName).ToUpper())
                    {
                        case ".BMP":
                            i_Photo.ImageNormal.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case ".JPG":
                            i_Photo.ImageNormal.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case ".PNG":
                            i_Photo.ImageNormal.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }
        #endregion  Private Static Methods
    }
}
