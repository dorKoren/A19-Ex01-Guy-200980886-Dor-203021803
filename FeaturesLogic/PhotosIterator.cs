using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class PhotosIterator : IEnumerator<Photo>
    {

        private SharedPhotosAlbum m_PhotosAlbum;
        private int m_CurrentIndex = -1;
        private int m_Count = -1;

        #region Constructor

        public PhotosIterator(SharedPhotosAlbum i_Album)
        {
            m_PhotosAlbum = i_Album;
            m_Count = m_PhotosAlbum.Count();
        }

        #endregion Constructor

        public Photo Current {
            get
            {
                if (m_Count > 0)
                {
                    return m_PhotosAlbum.m_ShardPhotos[m_CurrentIndex];
                }
                return null;
            }
        }

        object IEnumerator.Current {
            get
            {
                if (m_Count > 0)
                {
                    return m_PhotosAlbum.m_ShardPhotos[m_CurrentIndex];
                }
                return null;
            }        
        }

        public void Dispose()
        {
            // TODO: Impliment
        }

        public bool MoveNext()
        {
            m_CurrentIndex++;
        }

        public void Reset()
        {
            m_CurrentIndex = 0;
        }
    }
}
/*
 private Country m_Collection;
                private int m_CurrentIdx = -1;
                private int m_Count = -1;

                public BigCityNamesIterator(Country i_Collection)
                {
                    m_Collection = i_Collection;
                    m_Count = m_Collection.m_Cities.Count;
                }
 */
