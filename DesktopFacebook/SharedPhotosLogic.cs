using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal class SharedPhotosLogic
    {
        private readonly bool   r_WasFound = true;
        private readonly string r_Filter   = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";

        public User Friend         { get; set; }
        public bool FriendWasFound { get; set; }

        internal SharedPhotosLogic()
        {
            this.Friend         = null;
            this.FriendWasFound = !r_WasFound;
        }

        internal void FindFriend(string i_FirstName, string i_LastName, User i_LoggedInUser)
        {            
            foreach (User user in i_LoggedInUser.Friends)
            {
                if (i_FirstName.Equals(user.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    i_LastName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase))
                {
                    this.Friend = user;
                    this.FriendWasFound = this.r_WasFound;
                    break;
                }
            }
        }

        internal bool ImportSharedPhotos(User i_LoggedInUser,User i_Friend)
        {
            bool wereImported        = true;
            bool addExtension        = true;
            List<Photo> sharedPhotos = new List<Photo>();

            SaveFileDialog file = new SaveFileDialog
            {
                Filter       = this.r_Filter,
                AddExtension = addExtension
            };

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
            foreach (Photo photo in i_SharedPhotos)
            {
                exportPhoto(photo, i_File);
            }                     
        }

        private void exportPhoto(Photo i_Photo, SaveFileDialog i_File)
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

        private List<Photo> getSharedPhotos(User i_LoggedInUser, User i_Friend, Album i_Album)
        {
            List<Photo> photos = new List<Photo>();

            foreach (Photo photo in i_Album.Photos)
            {
                if (this.isSharedPhoto(i_LoggedInUser, i_Friend, photo))
                {
                    photos.Add(photo);
                }
            }

            return photos;
        }

        private bool isSharedPhoto(User i_LoggedInUser, User i_Friend, Photo i_Photo)
        {
            return this.isTag(i_LoggedInUser, i_Photo) && isTag(i_Friend, i_Photo);
        }

        private bool isTag(User i_User, Photo i_Photo)
        {
            bool isTag = false;
          
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
