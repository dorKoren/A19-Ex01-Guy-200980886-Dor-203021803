using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class SharedPhotos
    {
        private readonly bool r_WasFound = true;

        public User Friend         { get; set; }
        public bool FriendWasFound { get; set; }

        public SharedPhotos()
        {
            this.Friend = null;
            this.FriendWasFound = !r_WasFound;
        }


        public List<Photo> ImportSharedPhotos(User i_LoggedInUser, User i_Friend)
        {
            List<Photo> sharedPhotos = new List<Photo>();

            foreach (Album album in i_Friend.Albums)
            {
                sharedPhotos.AddRange(getSharedPhotos(i_LoggedInUser, i_Friend, album));
            }

            return sharedPhotos;
        }

        public void FindFriend(string i_FirstName, string i_LastName, User i_LoggedInUser)
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
