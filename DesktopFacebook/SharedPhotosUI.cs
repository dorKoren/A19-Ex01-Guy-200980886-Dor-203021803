using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal class SharedPhotosUI
    {
        #region Class Members
        private const string         k_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
        internal List<LazyPictureBox> SharedLazyPictureBox { get; }
        #endregion Class Members

        #region Class members


        #region constructor
        internal SharedPhotosUI(List<Photo> i_SharedPhotos)
        {
            SharedLazyPictureBox = new List<LazyPictureBox>();

            convertSharedPhotosToLazyPicBox(i_SharedPhotos);
        }
        #endregion constructor



        private void convertSharedPhotosToLazyPicBox(List<Photo> i_SharedPhotos)
        {

            foreach (Photo sharedPhoto in i_SharedPhotos)
            {
                LazyPictureBox lazyPicBox = new LazyPictureBox();

                lazyPicBox.Load(sharedPhoto.PictureNormalURL);
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
        #endregion  Private Static Methods
    }
}
