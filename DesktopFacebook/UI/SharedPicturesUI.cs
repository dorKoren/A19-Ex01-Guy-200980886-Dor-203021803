using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopFacebook.UI
{
    public static class SharedPicturesUI
    {
        private readonly string r_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";

        public static bool ImportSharedPhotos(User i_LoggedInUser, User i_Friend)
        {
            bool wereImported = true;
            bool addExtension = true;
            List<Photo> sharedPhotos = new List<Photo>();

            SaveFileDialog file = new SaveFileDialog
            {
                Filter = this.r_Filter,
                AddExtension = addExtension
            };

            foreach (Album album in i_Friend.Albums)
            {
                sharedPhotos.AddRange(SharedPhotoLogic.GetSharedPhotos(i_LoggedInUser, i_Friend, album));
            }

            if (sharedPhotos.Count == 0)
            {
                wereImported = !wereImported;
            }
            else
            {
                ExportPhotos(sharedPhotos, file);
            }

            return wereImported;
        }

        public static void ExportPhotos(List<Photo> i_SharedPhotos, SaveFileDialog i_File)
        {
            foreach (Photo photo in i_SharedPhotos)
            {
                ExportPhoto(photo, i_File);
            }
        }

        public static void ExportPhoto(Photo i_Photo, SaveFileDialog i_File)
        {
            i_File.FileName = i_Photo.Id;

            using (FileStream stream = (FileStream)i_File.OpenFile())
            {
                if (i_File.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(i_File.FileName).ToUpper())
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
