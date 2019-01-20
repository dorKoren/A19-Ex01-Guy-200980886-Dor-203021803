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
        internal readonly List<Photo> m_ShardPhotos;

        public SharedPhotosAlbum(List<Photo> i_Photos)
        {
            m_ShardPhotos = i_Photos;
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



/*

public class Country : IEnumerable, IEnumerable<string>
{
    private readonly List<City> m_Cities;

    public Country()
    {
        m_Cities = new List<City>()
        {
            new City() {Name = "Tel Aviv", Prefix = "03", Area = 122.7f, Population = 1250000},
            new City() {Name = "Herzelia", Prefix = "09", Area = 35.17f, Population = 65200},
            new City() {Name = "Haifa", Prefix = "04", Area = 105.5f, Population = 1080000},
            new City() {Name = "Hadera", Prefix = "08", Area = 68.25f, Population = 225000}
        };
    }
    */
