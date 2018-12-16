using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class SharedPhotos
    {
        #region Class Members
        private readonly bool r_WasFound = true;
        private readonly int  r_MaxNumOfImages = 30;

        public User        Friend                      { get; set; }
        public bool        FriendWasFound              { get; set; }
        public List<Photo> SharedPhotosList            { get; set; } 
        public int         TotalSelectedSharedPictures { get; set; }

        #endregion Class Members

        #region constructor
        public SharedPhotos()
        {
            Friend = null;
            FriendWasFound = !r_WasFound;
            TotalSelectedSharedPictures = 0;
        }
        #endregion constructor

        #region Public Methods
        public void ImportSharedPhotos(User i_LoggedInUser, User i_Friend)  
        {
            SharedPhotosList = new List<Photo>();


            foreach (Album album in i_Friend.Albums)
            {
                SharedPhotosList.AddRange(getSharedPhotos(i_LoggedInUser, i_Friend, album));

                // Pre-Set max number of images to load
                if (SharedPhotosList.Count > r_MaxNumOfImages)
                {
                    break;
                }
            }

            TotalSelectedSharedPictures = SharedPhotosList.Count;
        }

        public void FindFriend(string i_FirstName, string i_LastName, User i_LoggedInUser)
        {
            TotalSelectedSharedPictures = 0;

            foreach (User user in i_LoggedInUser.Friends)
            {
                if (i_FirstName.Equals(user.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    i_LastName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase))
                {
                    Friend = user;
                    FriendWasFound = this.r_WasFound;
                    break;
                }
            }
        }
        #endregion Public Methods

        #region Private Methods
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

            TotalSelectedSharedPictures = photos.Count;

            return photos;
        }

        private bool isSharedPhoto(User i_LoggedInUser, User i_Friend, Photo i_Photo)
        {
            return this.isTag(i_LoggedInUser, i_Photo) && isTag(i_Friend, i_Photo);
        }

        private bool isTag(User i_User, Photo i_Photo)
        {

            return true;  // DOR !!!

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
        #endregion Private Methods
    }
}
