using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class SharedPhotosAlbum : IEnumerable<Photo>
    {
        internal readonly List<Photo> r_ShardPhotos;

        public SharedPhotosAlbum(List<Photo> i_Photos)
        {
            r_ShardPhotos = i_Photos;
        }

        public IEnumerator<Photo> GetEnumerator()
        {
            return new PhotosIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PhotosIterator(this); 
        }
    }
}


