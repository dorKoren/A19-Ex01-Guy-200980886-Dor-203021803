using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal static class SharedPhotosUI
    {
        private const string k_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";

        internal static void ExportPhotos(List<Photo> i_SharedPhotos)
        {
            foreach (Photo photo in i_SharedPhotos)
            {
                exportPhoto(photo);
            }
        }

        private static void exportPhoto(Photo i_Photo)
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

                        default:
                            break;
                    }
                }
            }
        }
    }
}
