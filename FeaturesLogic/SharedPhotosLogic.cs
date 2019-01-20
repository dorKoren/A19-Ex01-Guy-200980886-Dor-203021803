using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class SharedPhotosLogic
    {
        #region Class Members
        private readonly bool r_WasFound       = true;
        private readonly int  r_MaxNumOfImages = 3;

        public User           Friend                       { get; set; }
        public bool           FriendWasFound               { get; set; }
        private List<Photo>   m_SharedPhotosList             { get; set; }
        public int            TotalSelectedSharedPictures  { get; set; }
        #endregion Class Members

        #region constructor
        public SharedPhotosLogic()
        {
            Friend = null;
            FriendWasFound = !r_WasFound;
            TotalSelectedSharedPictures = 0;
            m_SharedPhotosList = new List<Photo>();
        }
        #endregion constructor

        #region Public Methods
        public void ImportSharedPhotos(User i_LoggedInUser, User i_Friend)
        {

            m_SharedPhotosList = new List<Photo>();

            foreach (Album album in i_Friend.Albums)
            {
                m_SharedPhotosList.AddRange(getSharedPhotos(i_LoggedInUser, i_Friend, album));

                // Pre-Set max number of images to load
                if (m_SharedPhotosList.Count > r_MaxNumOfImages)
                {
                    break;
                }
            }

            TotalSelectedSharedPictures = m_SharedPhotosList.Count;
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

            TotalSelectedSharedPictures = m_SharedPhotosList.Count;
        }

        public IEnumerator<Photo> GetPhotosIterator()
        {
            return new SharedPhotosAlbum(m_SharedPhotosList).GetEnumerator();
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
            return photos;
        }

        private bool isSharedPhoto(User i_LoggedInUser, User i_Friend, Photo i_Photo)
        {
            return this.isTag(i_LoggedInUser, i_Photo) && isTag(i_Friend, i_Photo);
        }

        private bool isTag(User i_User, Photo i_Photo)
        {

            return true;  // DOR !!!

            //bool isTag = false;

            //foreach (PhotoTag tag in i_Photo.Tags)
            //{
            //    // We assume that we have the right Permissions
            //    if (tag.User.Equals(i_User))
            //    {
            //        isTag = !isTag;
            //        break;
            //    }
            //}

            //return isTag;
        }
        #endregion Private Methods
    }
}
