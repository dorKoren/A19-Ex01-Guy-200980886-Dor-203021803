﻿using System.Collections.Generic;
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
        internal static void DownLoadPhotos(List<Photo> i_SharedPhotos)
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
