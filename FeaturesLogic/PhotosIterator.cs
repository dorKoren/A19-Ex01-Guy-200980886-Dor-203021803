﻿using System;
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
            m_Count = m_PhotosAlbum.r_ShardPhotos.Count;
        }

        #endregion Constructor

        public Photo Current {
            get
            {
                if (m_Count > 0)
                {
                    return m_PhotosAlbum.r_ShardPhotos[m_CurrentIndex];
                }
                return null;
            }
        }

        object IEnumerator.Current {
            get
            {
                if (m_Count > 0)
                {
                    return m_PhotosAlbum.r_ShardPhotos[m_CurrentIndex];
                }
                return null;
            }        
        }

        public bool isEmpty()
        {
            return (m_Count <= 0);
        }

        public void Dispose()
        {
            // TODO: Impliment
        }

        public bool MoveNext()
        {
            bool wasMoved = false;

            if(m_CurrentIndex < m_Count - 1)
            {
                wasMoved = !wasMoved;
                m_CurrentIndex++;
            }
            return wasMoved;
            
        }

        public void Reset()
        {
            m_CurrentIndex = 0;
        }
    }
}
