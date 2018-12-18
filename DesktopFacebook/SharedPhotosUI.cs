using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal class SharedPhotosUI : IFetch
    {
        #region Class Members
        private  const string         k_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
        internal List<LazyPictureBox> SharedLazyPictureBox { get; }
        #endregion Class Members

        #region Class members


        #region constructor
        internal SharedPhotosUI()
        {
            SharedLazyPictureBox = new List<LazyPictureBox>();
        }
        #endregion constructor


        internal void ConvertPhotosToLazyPicBox(List<Photo> i_Photos)
        {
            foreach (Photo photo in i_Photos)
            {
                LazyPictureBox lazyPicBox = new LazyPictureBox();

                lazyPicBox.Load(photo.PictureNormalURL);
                SharedLazyPictureBox.Add(lazyPicBox);
            }
        }


        internal static void DownLoadPhotos(List<LazyPictureBox> i_SharedPicBox)   
        {
            foreach (LazyPictureBox lazyPicBox in i_SharedPicBox)
            {
                if (lazyPicBox.WasSelected)
                {
                    downLoadPhoto(lazyPicBox);
                }
            }
        }


        #endregion internal Static Methods

        #region  private static methods   
        private static void downLoadPhoto(LazyPictureBox i_LazyPicBox)
        {

            SaveFileDialog file = new SaveFileDialog
            {
                Filter = k_Filter,
                FileName = i_LazyPicBox.Text,
                AddExtension = true
            };

            using (FileStream stream = (FileStream)file.OpenFile())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(file.FileName).ToUpper())
                    {
                        case ".BMP":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case ".JPG":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case ".PNG":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }

        public void Fetch(List<Control> i_Controls)
        {
            foreach (Control control in i_Controls)
            {
                control.Visible = true;
            }

        }
        #endregion  Private Static Methods
    }
}
