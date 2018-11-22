using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Serializer
    {
        private const string k_Path = "C:\\Users\\dorko\\Desktop\\appSettings.xml";


        public User LastUser                                 { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool RememberUser                             { get; set; }
        public string LastAccessToken                        { get; set; }

        public Serializer()
        {
            this.RememberUser = false;
            this.LastAccessToken = null;
            this.LastUserBirthdayDictionary = null;
        }

        public static Serializer LoadFromFile()
        {
            Serializer obj = null;

            using (Stream stream = new FileStream(k_Path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Serializer));
                obj = serializer.Deserialize(stream) as Serializer;
            }

            return obj;
        }

        public void SaveToFile()
        {
            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }
    }
}
