using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class AppSettings
    {
        public User               LastUser                   { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool               RememberUser               { get; set; }
        public string             LastAccessToken            { get; set; }

        public AppSettings()
        {
            RememberUser               = false;
            LastAccessToken            = null;
            LastUserBirthdayDictionary = null;
        }

        public void SaveToFile()
        {
            using (Stream stream = new FileStream(@"C:\Users\dorko\Desktop\appSettings.xml", FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);           
            }
        }

        public static AppSettings LoadFromFile()
        {
            AppSettings obj = null;

            using (Stream stream = new FileStream(@"C:\Users\dorko\Desktop\appSettings.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                obj = serializer.Deserialize(stream) as AppSettings;
            }

            return obj;
        }
    }
}
