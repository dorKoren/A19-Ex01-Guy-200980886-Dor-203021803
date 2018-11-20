using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class SharedPhotosSettings
    {
        private readonly bool r_WasFound = true;

        public User Friend { get; set; }
        public bool FriendWasFound { get; set; }

        internal SharedPhotosSettings()
        {
            Friend = null;
            FriendWasFound = !r_WasFound;
        }

        public void FindFriend(string i_FirstName, string i_LastName, User i_LoggedInUser)
        {            
            foreach (User user in i_LoggedInUser.Friends)
            {
                if ((i_FirstName.Equals(user.FirstName, StringComparison.OrdinalIgnoreCase) &&
                     (i_LastName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase))))
                {
                    Friend = user;
                    FriendWasFound = r_WasFound;
                    break;
                }
                else
                {
                    FriendWasFound = false;
                    Friend = null;
                }
            }
        }

        public bool ImportSharedPhotos(User i_LoggedInUser,User i_Friend)
        {
            bool wereImported        = true;
            List<Photo> sharedPhotos = new List<Photo>();


            SaveFileDialog file        = new SaveFileDialog();
            file.FileName              = "SharedPic";
            file.Filter                = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
            file.AddExtension          = true;

            foreach (Album album in i_Friend.Albums)
            {
                sharedPhotos.AddRange(getSharedPhotos(i_LoggedInUser, i_Friend, album));            
            }

            if (sharedPhotos.Count == 0)
            {
                wereImported = !wereImported;
            }
            else
            {
                exportPhotos(sharedPhotos, file);
            }

            return wereImported;
        }

        private void exportPhotos(List<Photo> i_SharedPhotos, SaveFileDialog i_File)
        {
            if (i_File.ShowDialog() == DialogResult.OK)
            {
                foreach (Photo photo in i_SharedPhotos)
                {
                    exportPhoto(photo, i_File);
                }
            }
        }

        private void exportPhoto(Photo i_Photo, SaveFileDialog i_File)
        {
            switch (Path.GetExtension(i_File.FileName).ToUpper())
            {
                case ".BMP":
                    i_Photo.ImageNormal.Save(i_Photo.Name, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;

                case ".JPG":
                    i_Photo.ImageNormal.Save(i_Photo.Name, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;

                case ".PNG":
                    i_Photo.ImageNormal.Save(i_Photo.Name, System.Drawing.Imaging.ImageFormat.Png);
                    break;

                default:
                    break;
            }
            
        }

        private List<Photo> getSharedPhotos(User i_LoggedInUser, User i_Friend, Album i_Album)
        {
            List<Photo> photos = new List<Photo>();

            foreach (Photo photo in i_Album.Photos)
            {
                if (isSharedPhoto(i_LoggedInUser, i_Friend, photo))
                {
                    photos.Add(photo);
                }
            }

            return photos;
        }

        private bool isSharedPhoto(User i_LoggedInUser, User i_Friend, Photo i_Photo)
        {
            return isTag(i_LoggedInUser, i_Photo) && isTag(i_Friend, i_Photo);
        }


        private bool isTag(User i_User, Photo i_Photo)
        {
            bool isTag = false;

            return true;

           
            foreach (PhotoTag tag in i_Photo.Tags)
            {
                // We assume that we have the right Permissions
                if (tag.User.Equals(i_User))
                {
                    isTag = !isTag;
                    break;
                }
            }

            return isTag;
        }
    }
}
